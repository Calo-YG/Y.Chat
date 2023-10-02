using Y.Chat.Application.UserApplicationService;
using Y.Chat.Application.UserApplicationService.Commands;
using Y.Chat.Application.UserApplicationService.Dtos;

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
    }
}
