using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class GuoupUser: AuditAggregateRoot<Guid,Guid>
    {
        public Guid GroupId { get;private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public ChatGroup ChatGroup { get; private set; }

        public GuoupUser(Guid groupId,Guid userId)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            GroupId = groupId;
            UserId = userId;
            CreationTime = DateTime.Now;
        }
    }
}
