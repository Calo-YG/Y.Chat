using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Hubs;


namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class FriendCommadHandler
    {
        private readonly YChatContext Context;
        private readonly IHubContext<ChatHub> _chatContext;
        private readonly IEventBus _eventbus;
        public  FriendCommadHandler(YChatContext context
            , IHubContext<ChatHub> hubContext
            , IEventBus eventBus)
        {
            Context = context;
            _chatContext = hubContext;
            _eventbus = eventBus;
        }
        [EventHandler]
        public async Task CreateFriend(CreateFriendCommand cmd)
        {
            var frienduer = await Context.Users.FirstOrDefaultAsync(p => p.Id == cmd.FriendId);
            if (frienduer is null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            var hasfriend = await Context.Friends.AnyAsync(p=>p.FriendId == cmd.UserId&&p.UserId==cmd.FriendId);  
            
            if (hasfriend)
            {
                throw new UserFriendlyException("已添加好友");
            }

            var comment = cmd.Comment ?? frienduer.Name;

            var chatId = IdGeneratorFactory.SequentialGuidGenerator.NewId();

            var chatkey = $"{ChatConst.Group}_{chatId}";

            var friend = new Friends(cmd.FriendId,
                cmd.UserId,
                comment,
                chatId);
            var userfriend = new Friends(cmd.UserId,
                cmd.FriendId,
                "",
                chatId);

            string? connectionid = null;
            var exists = await RedisHelper.ExistsAsync($"{ChatConst.Online}_{cmd.UserId}");

            var currentconnectionId = await RedisHelper.GetAsync($"{ChatConst.Online}_{cmd.FriendId}");

            await _chatContext.Groups.AddToGroupAsync(currentconnectionId,chatId.ToString());

            await RedisHelper.LPushAsync(chatkey, cmd.FriendId);

            if (exists)
            {
                connectionid = await RedisHelper.GetAsync($"{ChatConst.Online}_{cmd.UserId}");

                await _chatContext.Groups.AddToGroupAsync(connectionid, chatId.ToString());

                await RedisHelper.LPushAsync(chatkey, cmd.FriendId);
            }

            var message = new CreateMessageCommand(cmd.FriendId, chatId, "我通过你的好友申请", Y.Chat.EntityCore.Domain.ChatDomain.Shared.MessageType.Text);

            await _chatContext.Clients.Groups(chatId.ToString()).SendAsync(ChatConst.Recive,chatId,message.Content);

            await Context.Friends.AddRangeAsync(friend, userfriend);

            await _eventbus.PublishAsync(message);
        }
    }
}
