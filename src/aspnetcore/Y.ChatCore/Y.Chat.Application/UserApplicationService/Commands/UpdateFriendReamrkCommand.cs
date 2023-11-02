using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.UserApplicationService.Commands
{
    public record class UpdateFriendReamrkCommand(Guid UserId,Guid FriendId,string Content):Command;
}
