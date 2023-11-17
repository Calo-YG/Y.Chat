using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.Base;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class SerachMessageQeury(Guid ChatId,Guid? UserId, string? Content, MessageType? MessageType, DateTime? CreationTime, int Page, int PageSize) : Query<PageDto<MessageDto>>
    {
        public override PageDto<MessageDto> Result { get; set; }
    }
}
