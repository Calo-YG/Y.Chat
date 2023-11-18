namespace Y.Chat.EntityCore.Domain.UserDomain
{
    public interface IUserDomainService
    {
        Task SendEmailCode(string email);

        Task CheckEmailCode(string email, string code);

        string GenerateToken(string username, Guid userId);

        Task SetAvatar(Guid userId,string avatar);
    }
}
