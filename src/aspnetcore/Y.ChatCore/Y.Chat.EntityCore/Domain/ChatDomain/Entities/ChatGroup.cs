using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Masuit.Tools.Systems;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class ChatGroup: AuditEntity<Guid,Guid>
    {
        public string Name { get;private set; }

        public string? Description { get;private set; } 

        public string Avatar { get;private set; }
        /// <summary>
        /// 群主
        /// </summary>
        public Guid? Bachelors {  get; private set; }
        /// <summary>
        /// 群号
        /// </summary>
        public string GroupNumber { get; private set; }   

        public ChatGroup() { }  
        public ChatGroup(string name,Guid? bachelors, string? description, string avatar="")
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Bachelors = bachelors;
            Name = name;    
            Description = description;
            Avatar = avatar;
            CreationTime = DateTime.Now;
        }

        public void SetGroupNumber()
        {
            var sfn = SnowFlakeNew.GetInstance();
            GroupNumber = sfn.GetUniqueId();
        }

        public void SetGroupAvatar(string avatar)
        {
            Avatar = avatar;
        }

    }
}
