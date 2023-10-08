﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Y.Chat.Application.FileApplicationService;
using Y.Chat.Application.FileApplicationService.Commands;
using Y.Chat.Application.FileApplicationService.Queries;

namespace Y.Chat.Host.Services
{
    public class FileService:BaseService<FileService>,IFileApplicationService
    {
        static string[] suffixs = new string[] { "jpg","png","jepg" };
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
        public async Task UploadAvatar([FromForm]IFormFileCollection files)
        {
            if(files?.Any() ?? false)
            {
                throw new UserFriendlyException("请选择文件");
            }
            
            var file = files?.FirstOrDefault();

            var suffix = file?.Name.Split(".")[1];

            if (!suffixs.Contains(suffix))
            {
                throw new UserFriendlyException("头像仅支持 jpg png jepg");
            }
            //var fileProvider =new FileExtensionContentTypeProvider();
            var userId = Guid.Parse(httpContextAccessor.HttpContext.Request.Form["userId"].ToString());

            UploadAvatarCommand cmd = new UploadAvatarCommand(file.OpenReadStream()
                ,file.Name
                ,file.ContentType
                ,userId);

            await _eventBus.PublishAsync(cmd);  
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFile(string filename)
        {
            var query = new GetFileQuery(filename);

            await _eventBus.PublishAsync(query);

            return new FileStreamResult(query.Result.FileStream
                ,query.Result.ContentType);
        }
    }
}
