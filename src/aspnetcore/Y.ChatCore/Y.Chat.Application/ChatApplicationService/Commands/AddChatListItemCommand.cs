using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class AddChatListItemCommand(Guid UserId,Guid ConversationId,string name,string avatar,Guid? FriendId,ChatType ChatType):Command;
}
