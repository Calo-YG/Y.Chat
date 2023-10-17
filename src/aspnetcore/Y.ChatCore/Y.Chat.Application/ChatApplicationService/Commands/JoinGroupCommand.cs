using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class JoinGroupCommand(Guid GroupId,Guid UserId):Command;
}
