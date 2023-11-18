using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IChatListRepositroy:IRepository<ChatList,Guid>
    {
    }
}
