using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.UserApplicationService.Dtos;
using Y.Chat.Application.UserApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Chat.EntityCore.Domain.UserDomain.Repositories;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserQueryHandler
    {
        private readonly YChatContext _context;
        private readonly IUserDomainService _userDomainService;
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        public UserQueryHandler(YChatContext context
            , IUserDomainService userDomainService
            , IFriendRepository friendRepository
            , IUserRepository userRepository)
        {
            _context = context; 
            _userDomainService = userDomainService;
            _friendRepository = friendRepository;
            _userRepository = userRepository;
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
            var list = await _userRepository.GetFriends(query.UserId)
                .ToListAsync();
            
            query.Result = list.Map<List<FriendDto>>();

            foreach (var item in query.Result)
            {
                var id = item.Id.ToString();

                var exists = await RedisHelper.ExistsAsync(id);

                item.Online = exists;
            }
        }
    }
}
