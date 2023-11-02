using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService
{
    public interface INoticeApplicationService
    {
        Task SendNotice(SendNoticeInput input);

        Task NoticeAggred(Guid id, Guid receviedId);

        Task NoticeRead(Guid id, Guid receviedId);
    }
}
