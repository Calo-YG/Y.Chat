namespace Y.Chat.EntityCore.Domain.ChatDomain
{
    public interface IChatDomainService
    {
        Task UpdateChatListMessage(Guid conversationId, Guid lastmessageid);
    }
}
