using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;
        private readonly IFriendRepository _friendRepository;

        public ChatHub(ILoggerFactory loggerFactory, IEventBus eventBus, IFriendRepository friendRepository)
        {
            _logger = loggerFactory.CreateLogger<ChatHub>();
            _eventBus = eventBus;
            _friendRepository = friendRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();

            await RedisHelper.SetAsync($"{ChatConst.Online}_{userId}",GetConnectionId());
            await RedisHelper.LPushAsync($"{ChatConst.UserOnlineList}_{userId}",userId);

            var usergroup = new UserGroupQuery((Guid)userId);

            var groupids = new List<Guid>();

            var friends = await _friendRepository.GetUserFriends(userId).ToListAsync();

            await _eventBus.PublishAsync(usergroup);

            groupids.AddRange(friends.Select(p => p.ChatId));
            groupids.AddRange(usergroup.Result.Select(p => p.Id));

            foreach (var item in groupids.Distinct())
            {
                var key = $"{ChatConst.Group}_{item}";
                // 加入群组
                await Groups.AddToGroupAsync(Context.ConnectionId, item.ToString("N"));

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

            var currentusers =await RedisHelper.LRangeAsync($"{ChatConst.UserOnlineList}_{userId}", 0, 2);
            if (currentusers.Length > 1)
            {
                return;
            }

            var usergroup = new UserGroupQuery((Guid)userId);

            var groupids = new List<Guid>();

            var friends = await _friendRepository.GetUserFriends(userId).ToListAsync();

            await _eventBus.PublishAsync(usergroup);

            groupids.AddRange(friends.Select(p => p.ChatId));
            groupids.AddRange(usergroup.Result.Select(p => p.Id));

            var connectionid = GetConnectionId();

            foreach (var item in groupids)
            {
                var key = $"{ChatConst.Group}_{item}";

                await Groups.RemoveFromGroupAsync(connectionid, item.ToString("N"));

                await RedisHelper.LRemAsync(key, -1, userId);
            }

            var systemMsg = new CreateNotifyCommand(userId, "用户下线", NotfiyType.Online);

            await _eventBus.PublishAsync(systemMsg);

            await RedisHelper.DelAsync($"{ChatConst.Online}_{userId}");

            await RedisHelper.LRemAsync($"{ChatConst.UserOnlineList}_{userId}", count: 1,userId);
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
            _logger.LogInformation($"ChatId---{groupid}");
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    return;
                }
                var userId = GetUserId();

                string key = $"user:{userId}_{groupid}:count";

                var exists = await RedisHelper.ExistsAsync(key);
                if (exists)
                {
                    var count = await RedisHelper.GetAsync<int>(key);

                    if (count >= 25)
                    {
                        //向发送者发送回调消息
                        await Clients.Caller.SendAsync("MessageLimit", "消息发送过于频繁");
                        return;
                    }
                }
                var messagetype = ChangeEnum.Change(type);
                var messagecmd = new CreateMessageCommand(userId, groupid, content, messagetype);

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

                await Clients.Groups(groupid.ToString("N")).SendAsync(ChatConst.Recive, groupid,userId, content,type,messagecmd.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("消息发送失败");
                _logger.LogError(ex.Message);
                throw;
            }

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
