using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class FriendMessage: AuditAggregateRoot<Guid,Guid>
    {
        public Guid SendUserId { get;private set; }

        public Guid RecivedUserId { get; private set; }

        public string Content { get; private set; }

        public MessageType MessageType { get; private set; }    

        public bool IsRead {  get; private set; }   


        public FriendMessage() { }
        public FriendMessage(Guid sendUserId,Guid recivedUserId,string content,MessageType messageType)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            SendUserId = sendUserId;
            RecivedUserId = recivedUserId;
            Content = content;
            MessageType = messageType;
            IsRead = false;
            CreationTime = DateTime.Now;
        }

        public void SetRead()
        {
            IsRead = true;
        }
    }
}
