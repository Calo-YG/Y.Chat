using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class UserNoticeQuery(Guid userId,NoticeType Type) : Query<List<NoticeDto>>
    {
        public override List<NoticeDto> Result { get; set; }
    }
}
