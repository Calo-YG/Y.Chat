using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;

namespace Y.Chat.EntityCore.Domain.UserDomain.Entities
{
    public class Friends:AuditEntity<Guid,Guid>
    {
        public Guid UserId { get;private set; }

        public User User { get;private set; }

        public Guid FriendId { get;private set; }
        /// <summary>
        /// 好友备注
        /// </summary>
        public string Comment { get;private set; }  

        private Friends() { }   
        public Friends(Guid userId,Guid friendId,string comment) 
        { 
            UserId = userId;
            FriendId = friendId;
            Comment = comment;
        }
    }
}
