using Calo.Blog.Common.Authorization;
using Masa.BuildingBlocks.Ddd.Domain.Services;
using Masuit.Tools;
using Masuit.Tools.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Shared;

namespace Y.Chat.EntityCore.Domain.UserDomain
{
    public class UserDomainService : DomainService, IUserDomainService,IScopedDependency
    {
        private readonly YChatContext _context;

        private readonly IDistributedCache _cache;

        private readonly ITokenProvider _tokenProvider;
        public UserDomainService(YChatContext context
            ,IDistributedCache cache
            ,ITokenProvider tokenProvider) 
        {
            _context = context;
            _cache = cache;
            _tokenProvider= tokenProvider;
        }  

        public async Task SendEmailCode(string email)
        {
            var key = YChatConst.EmailPrefix + email;
            if(await RedisHelper.ExistsAsync(key))       
            {
                throw new UserFriendlyException("验证码已发送请勿重复发送");
            }

            Random rnd = new Random();
            int num = rnd.StrictNext();//产生真随机数

            var _email = new Email()
            {
                SmtpServer = "smtp.163.com",// SMTP服务器
                SmtpPort = 25, // SMTP服务器端口
                EnableSsl = true,//使用SSL
                Username = "wyg154511sjq@163.com",// 邮箱用户名
                Password = "YBVNLAROJICNZDUU",// 邮箱密码
                Tos = email, //收件人
                Subject = "YChat注册通知",//邮件标题
                Body = $"你的邮箱注册码为{num}",//邮件内容 
            };

            _email.Send();

            var emailrecords = new EmailRecords(_email.Body
                , _email.Tos
                , DateTime.Now
                , DateTime.Now.AddMinutes(10)
                , EmailRecordType.Register);

            await _context.EmailRecords.AddAsync(emailrecords);
            await _context.SaveChangesAsync();  

            var emailcache = new EmailCache(email, num.ToString());

            await RedisHelper.SetAsync($"{YChatConst.EmailPrefix}{email}", emailcache, 600);
        }

        public async Task CheckEmailCode(string email,string code)
        {
            var key =$"{YChatConst.EmailPrefix}{email}";

            var exists = await RedisHelper.ExistsAsync(key);

            bool success = exists;

            var cache = await RedisHelper.GetAsync<EmailCache>(key);

            success = cache?.Code is null ?false : cache.Code == code;

            if(!success)
            {
                throw new UserFriendlyException("邮箱验证码不正确或者过期");
            }
        }

        public string GenerateToken(string username,Guid userId)
        {
            var tokenModle=new UserTokenModel()
            {
                UserId = userId.ToString(),
                UserName = username,
            };

            var token=_tokenProvider.GenerateToken(tokenModle); 

            return token;
        }

        public async Task SetAvatar(Guid userId, string avatar)
        {
            var user = _context.Users.FirstOrDefault(p=>p.Id == userId);

            user.SetAvatar(avatar);

            _context.Users.Update(user);

            await _context.SaveChangesAsync();  
        }
    }
}
