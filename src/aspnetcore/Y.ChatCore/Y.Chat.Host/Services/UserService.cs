using Calo.Blog.Common.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
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

            var tokenModle = new UserTokenModel()
            {
                UserId = query.Result.UserId.ToString(),
                UserName = query.Result.UserName,
            };
            return query.Result;
        }
        [RoutePattern(HttpMethod ="Patch")]
        public async Task SetSign(SignInput input)
        {
            var cmd = new SetUserSignCommand(input.Sign, input.UserId);
            await _eventBus.PublishAsync(cmd);
        }
    }
}
