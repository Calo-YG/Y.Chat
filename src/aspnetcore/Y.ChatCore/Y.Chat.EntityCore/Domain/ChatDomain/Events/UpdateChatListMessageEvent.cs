using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Ddd.Domain.Events;
using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Events
{
    public record class UpdateChatListMessageEvent(Guid ConversationId, Guid LastMessageId) : Event, IDomainEvent
    {
        public IUnitOfWork? UnitOfWork { get; set; }
    }
}
