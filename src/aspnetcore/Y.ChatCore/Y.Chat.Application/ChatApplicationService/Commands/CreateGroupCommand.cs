using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class CreateGroupCommand(Guid UserId,string Name,string? Decription):Command;
}
