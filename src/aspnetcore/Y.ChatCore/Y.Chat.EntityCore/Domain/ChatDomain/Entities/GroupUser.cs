using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class GroupUser: AuditAggregateRoot<Guid,Guid>
    {
        public Guid GroupId { get;private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public ChatGroup ChatGroup { get; private set; }

        public bool IsAdmin { get; private set; }
        /// <summary>
        /// 群主
        /// </summary>
        public bool Grouper { get; private set; }
        /// <summary>
        /// 群聊昵称
        /// </summary>
        public string? NickName { get; private set; }

        public GroupUser() { }  
        public GroupUser(Guid groupId,Guid userId)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            GroupId = groupId;
            UserId = userId;
            CreationTime = DateTime.Now;
        }

        public void Owner()
        {
            IsAdmin = true;
            Grouper = true;
        }

        public void Admin()
        {
            IsAdmin = false;
        }

        public void CancelAdmin()
        {
            IsAdmin = false;
        }
    }
}
