using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Shared;

namespace Y.Chat.EntityCore.Domain.UserDomain.Repositories
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        IQueryable<UserFriendModel> GetFriends(Guid userId);
    }
}
