﻿using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.UserApplicationService.Dtos;
using Y.Chat.Application.UserApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserQueryHandler
    {
        private readonly YChatContext _context;
        private readonly IUserDomainService _userDomainService;
        private readonly IFriendRepository _friendRepository;
        public UserQueryHandler(YChatContext context
            , IUserDomainService userDomainService
            , IFriendRepository friendRepository)
        {
            _context = context; 
            _userDomainService = userDomainService;
            _friendRepository = friendRepository;
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
            var select = await _friendRepository.GetUserFriends(query.UserId)
                .Select(p=>new FriendDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Avatar = p.Avatar,
                    Sign=p.Autograph
                })
                .ToListAsync();
            query.Result = select;
        }
    }
}
