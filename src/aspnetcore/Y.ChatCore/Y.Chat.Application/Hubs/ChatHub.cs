using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Y.Chat.EntityCore.Hubs
{
    [Authorize]
    public class ChatHub:Hub
    {
        private readonly ILogger _logger;
        private readonly IEventBus _eventBus;
        public ChatHub(ILoggerFactory loggerFactory
            ,IEventBus eventBus)
        {
            _logger = loggerFactory.CreateLogger<ChatHub>();
            _eventBus = eventBus;
        }

        public override async Task OnConnectedAsync()
        {
           
            var userId=GetUserId();
            var status = new UserStatus(userId);
            if(await RedisHelper.ExistsAsync(userId.ToString()))
            {
                await RedisHelper.SetAsync(userId.ToString(), status, exists: CSRedis.RedisExistence.Xx);
            }
            else
            {
               await RedisHelper.SetAsync(userId.ToString(), status, exists: CSRedis.RedisExistence.Nx);
            }
            
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogWarning(exception?.Message ?? "断开连接信息异常");
            var userId = GetUserId();
            var userStaus =await RedisHelper.GetAsync<UserStatus>(userId.ToString());
            userStaus.SetLeave();
            await RedisHelper.SetAsync(userId.ToString(), userStaus, exists: CSRedis.RedisExistence.Xx);

        }

        public Task SendMessage(SendMessageModel message)
        {
            
            return Task.CompletedTask;
        }

        private Guid GetUserId()
        {
            var id = Context.User.Claims.FirstOrDefault(p=>p.Type=="Id")?.Value;

            Guid userId;

            var isPrase= Guid.TryParse(id, out userId);
            if(!isPrase)
            {
                _logger.LogWarning($"Guid 转换失败--{id}");
            }
            return userId;
        }
    }
}
