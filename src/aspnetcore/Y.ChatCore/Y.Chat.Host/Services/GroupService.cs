using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.Application.FileApplicationService.Dtos;

namespace Y.Chat.Host.Services
{
    [Authorize]
    public class GroupService : BaseService<GroupService>, IGroupApplicationService
    {
        static string[] suffixs = new string[] { "jpg", "png", "jpeg" };
        private IHttpContextAccessor httpContextAccessor => GetRequiredService<IHttpContextAccessor>();
        public GroupService()
        {
        }
        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [RoutePattern(HttpMethod ="Post")]
        public async Task Create(CreateGroupInput input)
        {
            var command = new CreateGroupCommand(input.UserId
                ,input.Name
                ,input.Description);

            await _eventBus.PublishAsync(command);  
        }
        /// <summary>
        /// 上传群聊头像
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [RoutePattern(HttpMethod = "Post")]
        public async Task UploadAvatar([FromForm] IFormFile file)
        {
            if (file is null)
            {
                throw new UserFriendlyException("请选择文件");
            }

            var suffix = file?.FileName.Split(".")[1];

            if (!suffixs.Contains(suffix))
            {
                throw new UserFriendlyException("头像仅支持 jpg png jepg");
            }
            Guid groupId;
            var parse = Guid.TryParse(httpContextAccessor.HttpContext.Request.Form["groupId"].ToString(),out groupId);
            if (!parse)
            {
                return;
            }

            var cmd = new UploadGroupAvatarCommand(file.OpenReadStream()
               ,file.FileName
               ,groupId
               ,file.ContentType);

            await _eventBus.PublishAsync(cmd);
        }
        /// <summary>
        /// 搜索群聊
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [RoutePattern(HttpMethod = "Get")]
        public async Task<List<GroupDto>> QueryGroup(GroupQueryInput input)
        {
            var query = new GroupQuery(input.GroupName
                ,input.GroupNumber);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }
        /// <summary>
        /// 用户群聊列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [RoutePattern(HttpMethod = "Get")]
        public async Task<List<GroupDto>> UserGroup(Guid userId)
        {
            var query =new UserGroupQuery(userId);
            await _eventBus.PublishAsync(query);
            return query.Result;
        }
    }
}
