using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService.Commands
{
    public record CreateUserCommand(CreateUserInput Input) : Command;
}
