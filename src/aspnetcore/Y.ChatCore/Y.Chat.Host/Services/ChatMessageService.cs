using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.Base;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Host.Services
{
    public class ChatMessageService:BaseService<ChatMessageService>,IChatMessageApplicationService
    {
        public ChatMessageService()
        {

        }

        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task<PageDto<MessageDto>> QueryMessage(Guid chatId,string? content,Guid? userId,MessageType? messageType,DateTime? creationTime,int page,int pageSize)
        {
            var query = new SerachMessageQeury(chatId,
                userId,
                content,
                messageType,
                creationTime,
                page,
                pageSize);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }
    }
}
