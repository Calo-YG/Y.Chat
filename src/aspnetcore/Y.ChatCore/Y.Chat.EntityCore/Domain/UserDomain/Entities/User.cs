using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Masuit.Tools.Security;
using Masuit.Tools.Systems;
using Y.Chat.EntityCore.Domain.UserDomain.Shared;

namespace Y.Chat.EntityCore.Domain.UserDomain.Entities
{
    public class User : Entity<Guid>
    {
        public string Name { get;  set; }

        public string? Email { get;  set; }

        public string Account { get;  set; }

        public string Password { get;  set; }

        public string Avatar { get;  set; }

        public LoginType LoginType { get; private set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string? Autograph { get; private set; }

        public User() 
        {
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
        }
        public User(string name,string password, string? email)
        {
            var sfn = SnowFlakeNew.GetInstance(); // 改良版雪花id，对时间回拨不敏感
            Id = IdGeneratorFactory.SequentialGuidGenerator.NewId();
            Name = name;
            Email = email;
            Account = sfn.GetUniqueId();
            Password = password;
            Avatar = "";
        }

        public void SetName(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new UserFriendlyException("名称不能为空");
            }
            Name = name;
        }

        public void SetAccount(string? account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new UserFriendlyException("账号不能为空");
            }
            Account = account;
        }
        public void SetAvatar(string avatar)
        {
            Avatar = avatar;
        }

        public void SetPassword(string? password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new UserFriendlyException("密码不能为空");
            }
            Password = password.SHA256();
        }

        public void SetLoginType(LoginType loginType)
        {
            LoginType = loginType;
        }

        public void SetAutograph(string? autograph)
        {
            if (string.IsNullOrEmpty(autograph))
            {
                throw new UserFriendlyException("签名不能为空");
            }
            Autograph = autograph;  
        }
    }
}
