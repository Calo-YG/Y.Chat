using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class NoticeAgreeCommand(Guid Id,Guid ReceviedUserId):Command;
}
