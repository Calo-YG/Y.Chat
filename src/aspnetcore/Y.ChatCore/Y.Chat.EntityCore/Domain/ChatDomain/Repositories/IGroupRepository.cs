namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IGroupRepository
    {
        Task<bool> InGroup(Guid groupId, Guid userId);
    }
}
