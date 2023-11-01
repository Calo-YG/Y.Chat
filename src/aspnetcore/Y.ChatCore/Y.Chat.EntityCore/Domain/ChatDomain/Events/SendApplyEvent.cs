using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Events
{
    public record SendApplyEvent(bool IsGroup, Guid UserId, Guid? ApplyUserId,Guid? ApplyGroupId) :Event;
}
