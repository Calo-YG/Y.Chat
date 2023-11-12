using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Y.Chat.Application.FileApplicationService;
using Y.Chat.Application.FileApplicationService.Commands;
using Y.Chat.Application.FileApplicationService.Queries;
using Y.Chat.EntityCore.Domain.FileDomain.Shared;

namespace Y.Chat.Host.Services
{
    [Authorize]
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
        [Authorize]
        [RoutePattern(HttpMethod ="Post")]
        public async Task<string> UploadAvatar([FromForm]IFormFile file)
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
            Guid userId;
            var isParse = Guid.TryParse(httpContextAccessor.HttpContext.Request.Form["userId"].ToString(),out userId);
            if (!isParse)
            {
                throw new UserFriendlyException("Guid转换失败");
            }

            UploadAvatarCommand cmd = new UploadAvatarCommand(file.OpenReadStream()
                ,file.FileName
                ,file.ContentType
                ,userId);

            await _eventBus.PublishAsync(cmd);

            return cmd.FilePath;
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
        /// <summary>
        /// 上传群聊文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [Authorize]
        public async Task UploadGroupFile([FromBody]IFormFile file)
        {
            if (file is null)
            {
                throw new UserFriendlyException("请选择文件");
            }
            var jsonStr = httpContextAccessor.HttpContext.Request.Form["userId"].ToString();

            var model = JsonConvert.DeserializeObject<UploadGroupFileModel>(jsonStr);

            var upload = new UploadGroupFileCommand(file.OpenReadStream(),
                model.GroupId,
                model.ParentId, 
                file.ContentType, 
                file.FileName);

            await _eventBus.PublishAsync(upload);
        }
    }
}
