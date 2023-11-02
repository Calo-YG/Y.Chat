using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class UserNoticeQuery(Guid userId) : Query<List<NoticeDto>>
    {
        public override List<NoticeDto> Result { get; set; }
    }
}
