using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.Base;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageQueryHandler
    {
        private readonly YChatContext Context;
        private readonly IMessageRepositroy _messageRepository;
        public ChatMessageQueryHandler(YChatContext context,
            IMessageRepositroy messageRepositroy) 
        {
            Context = context;  
            _messageRepository = messageRepositroy;
        }

        [EventHandler]
        public async Task QueryMessage(SerachMessageQeury query)
        {
            var result = await _messageRepository.QueryMessaeList(query.ChatId,
                query.UserId,
                query.Content,
                query.MessageType,
                query.CreationTime,
                query.Page,
             query.PageSize);

            query.Result = new PageDto<MessageDto>
            {
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = result.Total,
                Result=result.Message.Map<List<MessageDto>>()
            };
        }
    }
}
