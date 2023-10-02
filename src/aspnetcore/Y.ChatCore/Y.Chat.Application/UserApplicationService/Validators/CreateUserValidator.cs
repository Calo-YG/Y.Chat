using FluentValidation;
using Y.Chat.Application.UserApplicationService.Commands;

namespace Y.Chat.Application.UserApplicationService.Validators
{
    public class CreateUserValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() 
        {
            RuleFor(p => p.Input.UserName)
                .Must(p => !string.IsNullOrEmpty(p)).WithMessage("用户名不能为空")
                .Must(p => p.Length > 2 && p.Length <= 10).WithMessage("用户名长度不能超过10");

            RuleFor(p => p.Input.Password)
                .NotNull().NotEmpty().WithMessage("")
                .Must(p => p.Length >= 10 && p.Length <= 15)
                .WithMessage("密码长度10~15之间");

            RuleFor(p => p.Input.Code).NotNull().NotEmpty().WithMessage("验证码不能为空");
        }
    }
}
