using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public class NoticeRepository : Repository<YChatContext, Notice, Guid>, INoticeRepository
    {
        public NoticeRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
        /// <summary>
        /// 用户通知
        /// </summary>
        /// <returns></returns>
        public  Task<List<NoticeModel>> UserNotice(Guid userId,NoticeType type)
        {
            var query = from n in Context.Notices
                        join u in Context.Users on n.InviteUserId equals u.Id
                        where n.RecivedUserId==userId && n.NoticeType==type && n.Agred==false
                        select new NoticeModel
                        {
                            Id=n.Id,
                            RecivedId=n.RecivedUserId,
                            NoticeType=n.NoticeType,
                            RequestAvatar=u.Avatar,
                            RequestUserId=u.Id,
                            RequestUserName=u.Name,
                            Content=n.Content,
                            Agree=n.Agred,
                            Read=n.Read,
                            CreationTime=n.CreationTime
                        };

            return  query.ToListAsync();
        }
        /// <summary>
        /// 设置群聊同意
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="grouid"></param>
        /// <returns></returns>
        public async Task GroupRequestAggree(Guid userId,Guid grouid)
        {
            var notices = Context.Notices.Where(p=>p.GroupId==grouid && p.InviteUserId==userId&&p.NoticeType==NoticeType.GroupRequest);

            foreach (var notice in notices)
            {
                notice.SetAggreed();
                notice.SetRead();
            }
           await base.UpdateRangeAsync(notices);
        }
    }
}
