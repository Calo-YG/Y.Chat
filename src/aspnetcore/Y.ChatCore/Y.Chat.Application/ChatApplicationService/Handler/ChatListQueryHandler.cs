using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatListQueryHandler
    {
        private readonly YChatContext Context;
        public ChatListQueryHandler(YChatContext context) 
        {
            Context = context;
        }
        [EventHandler]
        public async Task ListQuery(ChatListQuery query)
        {
            var result = from c in Context.ChatLists
                         where c.UserId == query.UserId
                         join m in Context.ChatMessages on c.LastMessageId equals m.Id
                         select new ChatListDto()
                         {
                             UserId=c.UserId,
                             ConversationId=m.GroupId,
                             MessageType=m.MessageType,
                             LastMessageId=m.Id,
                             LastMessageTime=m.CreationTime,
                             Avatart=c.Avatart,
                             Name=c.Name,
                             Content=m.Content,
                             UnReadCount=c.UnReadCount
                         };

            var data =await result.ToListAsync();

            query.Result = data;    
        }
    }
}
