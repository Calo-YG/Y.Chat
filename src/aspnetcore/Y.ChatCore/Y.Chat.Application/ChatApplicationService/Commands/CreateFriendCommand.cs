using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class CreateFriendCommand(Guid UserId,Guid FriendId,string? Comment):Command;
}
