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
                        where n.RecivedUserId==userId && n.NoticeType==type
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
                            Read=n.Read
                        };

            return  query.ToListAsync();
        }
    }
}
