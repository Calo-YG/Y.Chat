using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class DeleteFriendAllMessageEvent(Guid GroupId):Event;
}
