using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatListCommandHandler
    {
        private readonly YChatContext Context;
        private readonly IChatListRepositroy _chatListRepository;
        private readonly IMessageRepositroy _messageRepository;
        public ChatListCommandHandler(YChatContext context, IChatListRepositroy chatListRepositroy, IMessageRepositroy messageRepositroy) 
        { 
            Context = context; 
            _chatListRepository = chatListRepositroy;
            _messageRepository = messageRepositroy;
        }
        /// <summary>
        /// 添加用户聊天列表
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task AddItem(AddChatListItemCommand cmd)
        {
            var item = new ChatList(cmd.UserId,
                cmd.ConversationId,
                cmd.name,
                cmd.avatar,
                cmd.ChatType);

            if (cmd.ChatType == ChatType.Default&&cmd.FriendId is not null)
            {
                item.SetFriend((Guid)cmd.FriendId);
            }

            await Context.AddAsync(item);

            await Context.SaveChangesAsync();   
        }
        /// <summary>
        /// 删除聊天列表
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task Delete(DeleteItemCommand cmd) 
        {
            await _chatListRepository.RemoveAsync(cmd.Id);

            //删除历史消息
            var ids = Context.ChatMessages
                .Where(p=> p.GroupId == cmd.ConversationId)
                .Select(p=>p.Id)
                .ToList();

            await _messageRepository.RemoveRangeAsync(ids);
            
            await Context.SaveChangesAsync();   
        }
    }
}
