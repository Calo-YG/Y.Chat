using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;

namespace Y.Chat.Host.Services
{
    [Authorize]
    public class NoticeService:BaseService<NoticeService>, INoticeApplicationService
    {
        public NoticeService() { }

        [RoutePattern(HttpMethod ="Post")]
        public async Task SendNotice(SendNoticeInput input)
        {
            var cmd = new SendApplyCommand(input.IsGroup,
                input.UserId,
                input.ApplyUserId,
                input.ApplyGroupId,
                input.Remark);

            await _eventBus.PublishAsync(cmd);
        }
    }
}
