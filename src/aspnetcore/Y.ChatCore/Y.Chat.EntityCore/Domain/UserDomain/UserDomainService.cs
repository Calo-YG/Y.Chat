using Calo.Blog.Common.Authorization;
using Masa.BuildingBlocks.Ddd.Domain.Services;
using Masuit.Tools;
using Masuit.Tools.Models;
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
        }  

        public void SendEmailCode(string email)
        {
            Random rnd = new Random();
            int num = rnd.StrictNext();//产生真随机数

            var _email = new Email()
            {
                SmtpServer = "smtp.163.com",// SMTP服务器
                SmtpPort = 25, // SMTP服务器端口
                EnableSsl = true,//使用SSL
                Username = "wyg154511sjq.com",// 邮箱用户名
                Password = "123456",// 邮箱密码
                Tos = email, //收件人
                Subject = "YChat注册通知",//邮件标题
                Body = $"你的邮箱注册码为{num}",//邮件内容
            };

             _email.SendAsync(async p =>
            {
                var emailrecords = new EmailRecords(_email.Body
                    ,_email.Tos
                    ,DateTime.Now
                    ,DateTime.Now.AddMinutes(10)
                    ,EmailRecordType.Register);

                await  _context.EmailRecords.AddAsync(emailrecords);

                var emailcache = new EmailCache(email,num.ToString());

                await RedisHelper.SetAsync($"{YChatConst.EmailPrefix}{email}",emailcache,600);
            });
        }

        public async Task<bool> CheckEmailCode(string email,string code)
        {
            var key = YChatConst.EmailPrefix + email;

            var exists = await RedisHelper.ExistsAsync(key);

            if (!exists)
            {
                return false;
            }

            var cache = await RedisHelper.GetAsync<EmailCache>(key);

            return cache.Code == code;  
        }

        public string GenerateToken(string username,Guid userId)
        {
            var tokenModle=new TokenModle()
            {
                UserId = userId,
                UserName = username,
            };

            var token=_tokenProvider.GenerateToken(tokenModle); 

            return token;
        }
    }
}
