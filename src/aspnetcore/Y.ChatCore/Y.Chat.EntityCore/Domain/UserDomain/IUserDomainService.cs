namespace Y.Chat.EntityCore.Domain.UserDomain
{
    public interface IUserDomainService
    {
        void SendEmailCode(string email);

        Task<bool> CheckEmailCode(string email, string code);

        string GenerateToken(string username, Guid userId);
    }
}
