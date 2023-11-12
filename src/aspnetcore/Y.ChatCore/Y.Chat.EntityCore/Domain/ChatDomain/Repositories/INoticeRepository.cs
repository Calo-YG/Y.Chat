using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface INoticeRepository:IRepository<Notice,Guid>
    {
        Task<List<NoticeModel>> UserNotice(Guid userId, NoticeType type);
    }
}
