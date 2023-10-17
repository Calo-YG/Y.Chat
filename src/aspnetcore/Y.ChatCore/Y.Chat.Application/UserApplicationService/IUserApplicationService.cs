using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService
{
    public interface IUserApplicationService
    {
        Task Create(CreateUserInput input);

        Task SendCode(string email);

        Task<AuthenticationDto> Login(LoginInput input);

        Task SetSign(SignInput input);
    }
}
