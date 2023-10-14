using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.UserDomain.Repositories
{
    public interface IUserRepository: IRepository<User, Guid>
    {
        IQueryable<User> GetFriends(Guid userId);
    }
}
