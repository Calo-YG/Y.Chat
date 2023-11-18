using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class DeleteItemCommand(Guid Id,Guid ConversationId):Command;
}
