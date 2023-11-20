using Masa.BuildingBlocks.Ddd.Domain.Events;
using Masa.BuildingBlocks.Ddd.Domain.Services;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;

namespace Y.Chat.EntityCore.Domain.ChatDomain
{
    public class ChatDomainService:DomainService,IChatDomainService
    {
        public ChatDomainService(IDomainEventBus eventBus):base(eventBus) 
        { 
        }

        public async Task UpdateChatListMessage(Guid conversationId,Guid lastmessageid)
        {
            var evnet =new UpdateChatListMessageEvent(conversationId, lastmessageid);

            await EventBus.Enqueue(evnet);
        }
    }
}
