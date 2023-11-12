using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class ReadMessage: AuditEntity<Guid,Guid>
    {
        public Guid GroupId { get; set; }

        public Guid MessageId { get; set; }

        public bool IsRead { get; set; }

        public ReadMessage() { }    

        public ReadMessage(Guid groupId, Guid messageId)
        {
            GroupId = groupId;
            MessageId = messageId;
            IsRead = false;
        }

        public void SetRead() 
        {
            IsRead = true;
        }
    }
}
