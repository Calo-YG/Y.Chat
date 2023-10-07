using Y.Chat.Application.UserApplicationService;
using Y.Chat.Application.UserApplicationService.Commands;
using Y.Chat.Application.UserApplicationService.Dtos;
using Y.Chat.Application.UserApplicationService.Queries;

namespace Y.Chat.Host.Services
{
    public class UserService : BaseService<UserService>,IUserApplicationService
    {
        public UserService()
        {
            
        }

        [RoutePattern(HttpMethod ="Post")]
        public async Task Create(CreateUserInput input)
        {
            var cmd = new CreateUserCommand(input);

            await _eventBus.PublishAsync(cmd);
        }

        [RoutePattern(HttpMethod = "Get")]
        public async Task SendCode(string email)
        {
           var emailCommand=new SendEmailCommand(email);

           await _eventBus.PublishAsync(emailCommand);
        }

        [RoutePattern(HttpMethod ="Post")]
        public async Task<AuthenticationDto> Login(LoginInput input)
        {
            var query = new LoginQuery(input);

            await _eventBus.PublishAsync(query);

            return query.Result;
        }
    }
}
