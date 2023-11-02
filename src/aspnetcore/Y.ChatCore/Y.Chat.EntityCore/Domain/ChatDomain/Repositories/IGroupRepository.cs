using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IGroupRepository:IRepository<ChatGroup,Guid>
    {
        Task<bool> InGroup(Guid groupId, Guid userId);

        IQueryable<ChatGroup> UserGroups(Guid userId);

        Task<bool> IsGroupOnwer(Guid groupId, Guid userId);
    }
}
