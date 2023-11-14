using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.Hubs;
using Y.Chat.Application.UserApplicationService.Dtos;
using Y.Chat.Application.UserApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Repositories;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserQueryHandler
    {
        private readonly YChatContext _context;
        private readonly IUserDomainService _userDomainService;
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserQueryHandler(YChatContext context
            , IUserDomainService userDomainService
            , IFriendRepository friendRepository
            , IUserRepository userRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _context = context; 
            _userDomainService = userDomainService;
            _friendRepository = friendRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [EventHandler]
        public async Task Login(LoginQuery query)
        {
            var password = query.Input.Password.SHA256();

            var user =await _context.Users
                .FirstOrDefaultAsync(p => p.Name == query.Input.UserName && p.Password == password);

            if (user == null)
            {
                throw new UserFriendlyException(errorCode: "500","用户名或者密码错误");
            }

            var token = _userDomainService.GenerateToken(user.Name, user.Id);

            await RedisHelper.SetAsync(user.Id.ToString(), token);

            _httpContextAccessor.HttpContext.Response.Cookies.Append("x-access-token", token);

            query.Result = new Dtos.AuthenticationDto
            {
                Token = token,
                UserId = user.Id,
                UserName = user.Name,
                Avatar = user.Avatar,
                Email = user.Email,
                Sign=user.Autograph
            };
        }

        [EventHandler]

        public async Task Friends(FriendsQuery query)
        {
            var list = await _friendRepository.GetUserFriends(query.UserId)
                .ToListAsync();
            
            query.Result = list.Map<List<FriendDto>>();

            foreach (var item in query.Result)
            {
                var id = $"{ChatConst.Online}_{item.Id}";

                var exists = await RedisHelper.ExistsAsync(id);

                item.Online = exists;
            }
        }
    }
}
