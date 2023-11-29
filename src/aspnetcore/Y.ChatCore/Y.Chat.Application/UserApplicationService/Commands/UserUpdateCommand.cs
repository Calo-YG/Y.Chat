using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.UserApplicationService.Commands
{
    public record class UserUpdateCommand(Guid id,string? Sign,string?Name,string?Password):Command;
}
