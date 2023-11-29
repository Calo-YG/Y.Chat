using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Host.Services
{
    [Authorize]
    public class NoticeService:BaseService<NoticeService>, INoticeApplicationService
    {
        public NoticeService() { }
        [Authorize]
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
        /// <summary>
        /// 通知已读
        /// </summary>
        /// <param name="id"></param>
        /// <param name="receviedId"></param>
        /// <returns></returns>
        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task NoticeRead(Guid id, Guid receviedId)
        {
            var cmd = new NoticeReadCommand(id, receviedId);

            await _eventBus.PublishAsync(cmd);
        }
        /// <summary>
        /// 获取用户通知
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task<List<NoticeDto>> UserNotices(Guid userId, NoticeType type)
        {
            var query = new UserNoticeQuery(userId,type);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }

        [Authorize]
        [RoutePattern(HttpMethod = "Delete")]
        public async Task Delete(Guid id)
        {
            var cmd = new DeleteNoticeCommand(id);

            await _eventBus.PublishAsync(cmd);
        }
    }
}
