using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public class NoticeRepository : Repository<YChatContext, Notice, Guid>, INoticeRepository
    {
        public NoticeRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
