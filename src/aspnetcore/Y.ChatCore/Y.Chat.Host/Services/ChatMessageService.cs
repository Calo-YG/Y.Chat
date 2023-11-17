using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.Base;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;

namespace Y.Chat.Host.Services
{
    public class ChatMessageService:BaseService<ChatMessageService>,IChatMessageApplicationService
    {
        public ChatMessageService()
        {

        }

        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task<PageDto<MessageDto>> QueryMessage(MessageInput input)
        {
            var query = new SerachMessageQeury(input.ChatId,
                input.UserId, 
                input.Content, 
                input.MessageType, 
                input.CreationTime, 
                input.Page, 
                input.Page);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }
    }
}
