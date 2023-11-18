using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Masuit.Tools.Systems;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class Conversation: AuditEntity<Guid,Guid>
    {
        public string Name { get; set; }

        public string? Description { get; set; } 

        public string Avatar { get; set; }
        /// <summary>
        /// 群号
        /// </summary>
        public string GroupNumber { get;  set; }   

        public Guid? UserId { get;  set; }

        public Conversation() 
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
        }  
        public Conversation(string name, string? description, string avatar="")
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;    
            Description = description;
            Avatar = avatar;
            CreationTime = DateTime.Now;
            SetGroupNumber();
        }

        public void SetUser(Guid? userId)
        {
            UserId = userId;
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
