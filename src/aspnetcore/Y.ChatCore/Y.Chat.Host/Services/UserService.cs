using Calo.Blog.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Y.Chat.Application.ChatApplicationService.Commands;
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
        [Authorize]
        [RoutePattern(HttpMethod ="Patch")]
        public async Task SetSign(SignInput input)
        {
            var cmd = new SetUserSignCommand(input.Sign, input.UserId);
            await _eventBus.PublishAsync(cmd);
        }
        [Authorize]
        [RoutePattern(HttpMethod ="Get")]
        public async Task<List<FriendDto>> Friends(Guid userId)
        {
            var query = new FriendsQuery(userId);   
            await _eventBus.PublishAsync(query);
            return query.Result;
        }
        [Authorize]
        [RoutePattern(HttpMethod = "Post")]
        public async Task UpdateUserRemark(UpdateFriendRemarkInput input)
        {
            var cmd = new UpdateFriendReamrkCommand(input.UserId,
                input.FriendId,
                input.Content);

            await _eventBus.PublishAsync(cmd);
        }
        [Authorize]
        [RoutePattern(HttpMethod = "Get")]
        public async Task DeleteFriend(Guid chatId)
        {
            var cmd = new DeleteFriendCommand(chatId);

            await _eventBus.PublishAsync(cmd);
        }
    }
}
