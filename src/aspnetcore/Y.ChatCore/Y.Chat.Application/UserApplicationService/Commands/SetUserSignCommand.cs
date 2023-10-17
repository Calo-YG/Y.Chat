using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.UserApplicationService.Commands
{
    public record class SetUserSignCommand(string Sign, Guid UserId) : Command;
}
