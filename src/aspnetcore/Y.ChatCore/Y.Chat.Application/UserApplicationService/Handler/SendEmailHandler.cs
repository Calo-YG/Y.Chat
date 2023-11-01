using Masuit.Tools.Models;
using Y.EventBus;
using Y.Chat.EntityCore.Domain.UserDomain.Events;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class SendEmailHandler : IEventHandler<SendEmailEvent>
    {
        public Task HandelrAsync(SendEmailEvent eto)
        {

            var _email = new Email()
            {
                SmtpServer = "smtp.163.com",// SMTP服务器
                SmtpPort = 25, // SMTP服务器端口
                EnableSsl = true,//使用SSL
                Username = "wyg154511sjq@163.com",// 邮箱用户名
                Password = "YBVNLAROJICNZDUU",// 邮箱密码
                Tos = eto.Email, //收件人
                Subject = "YChat注册通知",//邮件标题
                Body = $"你的邮箱注册码为{eto.Numbers}",//邮件内容 
            };

            _email.Send();
            return Task.CompletedTask;
        }
    }
}
