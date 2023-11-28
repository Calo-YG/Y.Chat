using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class Message: AuditAggregateRoot<Guid,Guid>
    {
        public string Content {  get; set; }

        public MessageType MessageType { get; set; }

        public Guid UserId { get; set; }

        public Guid GroupId { get; set; }

        public bool Withdraw { get;private set; }

        public Message() { }    
        public Message(Guid userId,Guid groupId,string content,MessageType messageType) 
        {
            Id=IdGeneratorFactory.SequentialGuidGenerator.NewId();
            UserId=userId;
            GroupId=groupId;
            Content=content;
            MessageType = messageType;
            CreationTime = DateTime.Now;
            Creator = userId;
        }

        public void WithdrawMessage() => Withdraw = true;
    }
}
