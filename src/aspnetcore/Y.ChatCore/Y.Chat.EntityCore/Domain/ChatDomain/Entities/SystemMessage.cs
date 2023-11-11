using Masa.BuildingBlocks.Ddd.Domain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class SystemMessage:Entity<Guid>
    {
        public Guid UserId { get;private set; }

        public string Content { get; private set; }

        public NotfiyType NotfiyType { get; private set;}

        public Guid? RecivedUserId { get; private set; }

        public SystemMessage() { }

        public SystemMessage(Guid userId, string content, NotfiyType notfiyType, Guid? recivedUserId)
        {
            UserId = userId;
            Content = content;
            NotfiyType = notfiyType;
            RecivedUserId = recivedUserId;
        }   
    }
}
