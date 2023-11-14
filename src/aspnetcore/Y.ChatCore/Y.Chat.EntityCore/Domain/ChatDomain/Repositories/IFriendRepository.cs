using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IFriendRepository:IRepository<Friends,Guid>
    {
        IQueryable<FriendsModel> GetUserFriends(Guid userId);

        Task<List<Friends>> HasFriend(Guid chatId);
    }
}
