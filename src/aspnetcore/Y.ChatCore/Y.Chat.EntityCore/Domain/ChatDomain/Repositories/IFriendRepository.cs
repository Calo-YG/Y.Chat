using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IFriendRepository
    {
        IQueryable<User> GetUserFriends(Guid userId);
    }
}
