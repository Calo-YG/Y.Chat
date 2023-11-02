using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Commands;
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
        /// <summary>
        /// 通知同意
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receviedId"></param>
        /// <returns></returns>
        [Authorize]
        [RoutePattern(HttpMethod ="Get")]
        public async Task NoticeAggred(Guid id,Guid receviedId)
        {
            var cmd = new NoticeAgreeCommand(id, receviedId);   

            await _eventBus.PublishAsync(cmd);
        }
        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task NoticeRead(Guid id, Guid receviedId)
        {
            var cmd = new NoticeReadCommand(id, receviedId);

            await _eventBus.PublishAsync(cmd);
        }
    }
}
