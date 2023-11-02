using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities.Auditing;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class Notice: AuditAggregateRoot<Guid,Guid>
    {
        public Guid InviteUserId { get;private set; }

        public Guid RecivedUserId { get;private set; }

        public Guid? GroupId { get;private set; }

        public string Content {  get;private set; }  
        
        public bool Agred {  get;private set; }

        public NoticeType NoticeType { get; private set; }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string? Remark { get;private set; }  

        private Notice() { }

        public Notice(Guid inviteduserId,Guid recivedUserId,string content,NoticeType noticeType,string? reamrk,Guid? groupId=null) 
        {
            Id=IdGeneratorFactory.SequentialGuidGenerator.NewId();
            InviteUserId=inviteduserId;
            RecivedUserId=recivedUserId;
            Content=content;
            NoticeType=noticeType;
            Remark = reamrk;
            SetGroupId(groupId);
        }

        private void SetGroupId(Guid? groupId)
        {
            if (NoticeType == NoticeType.GroupRequest&&groupId.HasValue)
            {
                GroupId=groupId;
                NoticeType = NoticeType.GroupRequest;
            }
        }
        public void SetAggreed()
        {
            Agred = true;
        }
    }
}
