using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IGroupRepository:IRepository<Conversation,Guid>
    {
        Task<bool> InGroup(Guid groupId, Guid userId);

        IQueryable<Conversation> UserGroups(Guid userId);

        Task<bool> IsGroupOnwer(Guid groupId, Guid userId);

        IQueryable<User> GroupUsers(Guid groupId);
    }
}
