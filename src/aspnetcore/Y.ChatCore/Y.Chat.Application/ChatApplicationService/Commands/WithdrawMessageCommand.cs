using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class WithdrawMessageCommand(Guid MessageId,Guid UserId,Guid ChatId) : Command;
}
