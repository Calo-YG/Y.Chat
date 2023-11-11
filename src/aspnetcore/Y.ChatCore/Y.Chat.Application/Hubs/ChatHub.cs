using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;

        public ChatHub(ILoggerFactory loggerFactory, IEventBus eventBus)
        {
            _logger = loggerFactory.CreateLogger<ChatHub>();
            _eventBus = eventBus;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            var status = new UserStatus(userId, GetConnectionId());
            if (await RedisHelper.ExistsAsync(userId.ToString()))
            {
                await RedisHelper.SetAsync(
                    userId.ToString(),
                    status,
                    exists: CSRedis.RedisExistence.Xx
                );
            }
            else
            {
                await RedisHelper.SetAsync(
                    userId.ToString(),
                    status,
                    exists: CSRedis.RedisExistence.Nx
                );
            }

            var usergroup = new UserGroupQuery((Guid)userId);

            await _eventBus.PublishAsync(usergroup);

            foreach (var item in usergroup.Result)
            {
                var key = item.Id.ToString("N");
                // 加入群组
                await Groups.AddToGroupAsync(Context.ConnectionId, item.Id.ToString("N"));

                // 如果用户不存在当前群聊在线人数中，则添加。
                await RedisHelper.LRemAsync(key, -1, userId);
                await RedisHelper.LPushAsync(key, userId);
            }

            var systemMsg = new CreateNotifyCommand(userId, "用户上线", NotfiyType.Online);

            await _eventBus.PublishAsync(systemMsg);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogWarning(exception?.Message ?? "断开连接信息异常");
            var userId = GetUserId();
            var userStaus = await RedisHelper.GetAsync<UserStatus>(userId.ToString());
            userStaus.SetLeave();

            var usergroup = new UserGroupQuery(userId);

            await _eventBus.PublishAsync(usergroup);

            var connectionid = GetConnectionId();

            foreach (var item in usergroup.Result)
            {
                var key = item.Id.ToString("N");

                await Groups.RemoveFromGroupAsync(connectionid, item.Id.ToString("N"));

                await RedisHelper.LRemAsync(key, -1, connectionid);
            }

            var systemMsg = new CreateNotifyCommand(userId, "用户下线", NotfiyType.Online);
            await _eventBus.PublishAsync(systemMsg);

            await RedisHelper.DelAsync(userId.ToString());
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="content"></param>
        /// <param name="groupid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task SendMessage(string content,Guid groupid,string type)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            
            var userId=GetUserId();

            string key = $"user:{userId}:count";

            var exists =await RedisHelper.ExistsAsync(key);
            if (exists)
            {
                var count = await RedisHelper.GetAsync<int>(key);

                if (count>=25)
                {
                    return;
                }
            }
            var messagetype = ChangeEnum.Change(type);
            var messagecmd = new CreateMessageCommand(userId,groupid, content, messagetype);

            if (exists)
            {
                await RedisHelper.IncrByAsync(key, 1);
            }
            else
            {
                await RedisHelper.SetNxAsync(key, 1);
                await RedisHelper.ExpireAsync(key, 60);
            }

            await _eventBus.PublishAsync(messagecmd);

            await Clients.Groups(groupid.ToString("N")).SendAsync("ReciveMessage", groupid, content);
        }

        private Guid GetUserId()
        {
            var id = Context.User.Claims.FirstOrDefault(p => p.Type == "Id")?.Value;

            if (id is null)
            {
                throw new UnauthorizedAccessException(); ;
            }

            Guid userId;

            var isPrase = Guid.TryParse(id, out userId);
            if (!isPrase)
            {
                _logger.LogWarning($"Guid 转换失败--{id}");
            }
            return userId;
        }

        private string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
