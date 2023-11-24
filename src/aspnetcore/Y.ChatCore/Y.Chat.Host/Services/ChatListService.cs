using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;

namespace Y.Chat.Host.Services
{
    public class ChatListService:BaseService<ChatListService>,IChatListApplicationService
    {
        public ChatListService() { }

        [Authorize]
        [RoutePattern(HttpMethod ="Delete")]
        public async Task Delete(Guid id,Guid conversationId)
        {
            var cmd = new DeleteItemCommand(id,conversationId);

            await _eventBus.PublishAsync(cmd);
        }

        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task<List<ChatListDto>> Query(Guid userId)
        {
            var query = new ChatListQuery(userId);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }

        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task<ChatListDto?> Find(Guid chatId,Guid userId)
        {
            var query = new ChatListFindQuery(chatId, userId);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }
    }
}
