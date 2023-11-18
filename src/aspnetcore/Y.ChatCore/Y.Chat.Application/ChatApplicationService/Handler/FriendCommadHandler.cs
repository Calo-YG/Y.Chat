using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Hubs;


namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class FriendCommadHandler
    {
        private readonly YChatContext Context;
        private readonly IHubContext<ChatHub> _chatContext;
        private readonly IEventBus _eventbus;
        private readonly IFriendRepository _friendRepository;
        private readonly IChatListRepositroy _chatListRepositroy;
        public  FriendCommadHandler(YChatContext context
            , IHubContext<ChatHub> hubContext
            , IEventBus eventBus
            , IFriendRepository friendRepository
            , IChatListRepositroy chatListRepositroy)
        {
            Context = context;
            _chatContext = hubContext;
            _eventbus = eventBus;
            _friendRepository = friendRepository;
            _chatListRepositroy = chatListRepositroy;
        }
        [EventHandler]
        public async Task CreateFriend(CreateFriendCommand cmd)
        {
            var ids=new List<Guid>() { cmd.FriendId,cmd.UserId };

            var users = await Context.Users.Where(p=>ids.Contains(p.Id)).ToListAsync(); 

            var frienduer = users.FirstOrDefault(p => p.Id == cmd.FriendId);

            var user =users.FirstOrDefault(p=>p.Id == cmd.UserId);  

            if (users.Count < 2)
            {
                throw new UserFriendlyException("用户不存在");
            }

            var hasfriend = await Context.Friends.AnyAsync(p=>p.FriendId == cmd.UserId&&p.UserId==cmd.FriendId);  
            
            if (hasfriend)
            {
                throw new UserFriendlyException("已添加好友");
            }

            var comment = cmd.Comment ?? frienduer.Name;

            var conversation = new Conversation($"friend{frienduer.Name}", "", "");

            var chatId = conversation.Id;

            var chatkey = $"{ChatConst.Group}_{chatId}";

            var friend = new Friends(cmd.FriendId,
                cmd.UserId,
                comment,
                chatId);
            var userfriend = new Friends(cmd.UserId,
                cmd.FriendId,
                "",
                chatId);

            var userchatlist = new ChatList(user.Id,chatId,user.Name,user.Avatar,EntityCore.Domain.ChatDomain.Shared.ChatType.Default);

            var friendchatlist = new ChatList(frienduer.Id, chatId, frienduer.Name, user.Avatar, EntityCore.Domain.ChatDomain.Shared.ChatType.Default);

            List<ChatList> chatLists = new List<ChatList>() { userchatlist,friendchatlist};

            string? connectionid = null;
            var exists = await RedisHelper.ExistsAsync($"{ChatConst.Online}_{cmd.UserId}");

            var currentconnectionId = await RedisHelper.GetAsync($"{ChatConst.Online}_{cmd.FriendId}");

            await _chatContext.Groups.AddToGroupAsync(currentconnectionId,chatId.ToString());

            await _chatListRepositroy.AddRangeAsync(chatLists);

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

        [EventHandler]
        public async Task DeleteUser(DeleteFriendCommand cmd)
        {
            var friends =await _friendRepository.HasFriend(cmd.ChatId);

            if(friends.Count < 2)
            {
                throw new UserFriendlyException("好友关系不存在");
            }
            var idstr=cmd.ChatId.ToString();

            var key = $"{ChatConst.Group}_{idstr}";

            var userConnectionId = await RedisHelper.GetAsync($"{ChatConst.Online}_{friends[0].UserId}");

            var connectionId = await RedisHelper.GetAsync($"{ChatConst.Online}_{friends[1].UserId}");

            await _friendRepository.RemoveRangeAsync(friends);

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _chatContext.Groups.RemoveFromGroupAsync(idstr, userConnectionId);
            }

            if(!string.IsNullOrEmpty(connectionId))
            {
                await _chatContext.Groups.RemoveFromGroupAsync(idstr, userConnectionId);
            }

            await RedisHelper.DelAsync(key);

            var deleteevent = new DeleteFriendAllMessageEvent(cmd.ChatId);
           
            await _eventbus.PublishAsync(deleteevent);

            await Context.SaveChangesAsync();
        }
    }
}
