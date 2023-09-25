using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class ChatGroup: AuditEntity<Guid,Guid>
    {
        public string Name { get;private set; }

        public string? Description { get;private set; } 

        public string Avatar { get;private set; }

        public ChatGroup(string name, string? description, string avatar)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;    
            Description = description;
            Avatar = avatar;
            CreationTime = DateTime.Now;
        }

        public void SetGroupAvatar(string avatar)
        {
            Avatar = avatar;
        }

    }
}
