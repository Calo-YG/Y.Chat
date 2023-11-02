using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class NoticeReadCommand(Guid Id,Guid ReceviedId):Command;
}
