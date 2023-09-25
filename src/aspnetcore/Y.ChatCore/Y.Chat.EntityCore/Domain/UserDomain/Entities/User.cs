using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Shared;

namespace Y.Chat.EntityCore.Domain.UserDomain.Entities
{
    public class User : Entity<Guid>
    {
        public string Name { get; private set; }

        public string? Email { get; private set; }

        public string Account { get; private set; }

        public string Password { get; private set; }

        public string Avatar { get; private set; }

        public LoginType LoginType { get; private set; }

        public User(string name, string account, string password, string? email)
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;
            Email = email;
            Account = account;
            Password = password;
        }

        public void SetAvatar(string avatar)
        {
            Avatar = avatar;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetLoginType(LoginType loginType)
        {
            LoginType = loginType;
        }
    }
}
