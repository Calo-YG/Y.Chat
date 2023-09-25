using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class ChatMessage: AuditAggregateRoot<Guid,Guid>
    {
        public string Content {  get; set; }

        public MessageType MessageType { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid GroupId { get; set; }

        public ChatGroup ChatGroup { get; set; }

        //public bool IsRead { get; set; }

        public ChatMessage(Guid userId,Guid groupId,string content,MessageType messageType) 
        {
            Id=IdGeneratorFactory.SequentialGuidGenerator.NewId();
            UserId=userId;
            UserId=groupId;
            Content=content;
            MessageType = messageType;
            CreationTime = DateTime.Now;
        }
    }
}
