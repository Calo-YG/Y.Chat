using Masa.BuildingBlocks.Ddd.Domain.Events;
using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.EntityCore.Domain.UserDomain.Events
{
    public record class UpdateAvatarEvent(Guid userId,string avatar):DomainEvent;
}
