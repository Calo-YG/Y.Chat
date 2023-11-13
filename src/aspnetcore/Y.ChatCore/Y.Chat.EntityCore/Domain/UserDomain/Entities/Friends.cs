using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace Y.Chat.EntityCore.Domain.UserDomain.Entities
{
    public class Friends:AuditEntity<Guid,Guid>
    {
        public Guid UserId { get;private set; }

        public Guid FriendId { get;private set; }

        public Guid ChatId { get;private set; }
        /// <summary>
        /// 好友备注
        /// </summary>
        public string Comment { get;private set; }  

        private Friends() { }   
        public Friends(Guid userId,Guid friendId,string comment,Guid chatId) 
        { 
            Id= IdGeneratorFactory.SequentialGuidGenerator.NewId();
            UserId = userId;
            FriendId = friendId;
            Comment = comment;
            ChatId =chatId;
        }

        public void SetComment(string comment)
        {
            Comment = comment;
        }
    }
}
