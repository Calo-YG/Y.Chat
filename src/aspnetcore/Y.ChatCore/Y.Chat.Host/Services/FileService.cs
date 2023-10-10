using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Y.Chat.Application.FileApplicationService;
using Y.Chat.Application.FileApplicationService.Commands;
using Y.Chat.Application.FileApplicationService.Queries;

namespace Y.Chat.Host.Services
{
    public class FileService:BaseService<FileService>,IFileApplicationService
    {
        static string[] suffixs = new string[] { "jpg","png","jpeg" };
        private IHttpContextAccessor httpContextAccessor=>GetRequiredService<IHttpContextAccessor>();
        public FileService()
        {

        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="files"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [RoutePattern(HttpMethod ="Post")]
        public async Task UploadAvatar([FromForm]IFormFile file)
        {
            if(file is null)
            {
                throw new UserFriendlyException("请选择文件");
            }
            
            var suffix = file?.FileName.Split(".")[1];

            if (!suffixs.Contains(suffix))
            {
                throw new UserFriendlyException("头像仅支持 jpg png jepg");
            }
            //var fileProvider =new FileExtensionContentTypeProvider();
            var userId = Guid.Parse(httpContextAccessor.HttpContext.Request.Form["userId"].ToString());

            UploadAvatarCommand cmd = new UploadAvatarCommand(file.OpenReadStream()
                ,file.FileName
                ,file.ContentType
                ,userId);

            await _eventBus.PublishAsync(cmd);  
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<IResult> GetFile(string filename)
        {
            var query = new GetFileQuery(filename);

            await _eventBus.PublishAsync(query);

            return Results.Stream(query.Result.FileStream,query.Result.ContentType);
        }
    }
}
