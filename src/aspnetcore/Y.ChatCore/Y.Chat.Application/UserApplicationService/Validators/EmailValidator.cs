using FluentValidation;
using Y.Chat.Application.UserApplicationService.Commands;

namespace Y.Chat.Application.UserApplicationService.Validators
{
    public class EmailValidator: AbstractValidator<SendEmailCommand>
    {
        public EmailValidator() 
        {
            RuleFor(p => p.email).EmailAddress().WithMessage("请输入邮箱的邮箱地址");
        }
    }
}
