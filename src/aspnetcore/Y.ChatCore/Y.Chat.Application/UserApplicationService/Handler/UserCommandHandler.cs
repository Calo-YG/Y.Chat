using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.UserApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserCommandHandler
    {
        private readonly YChatContext _chatContext;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IUserDomainService _userDomainService;
        public UserCommandHandler(IRepository<User,Guid> userRepositroy
            ,YChatContext context
            ,IUserDomainService userDomainService)
        {
            _userRepository = userRepositroy;
            _chatContext = context; 
            _userDomainService = userDomainService;
        }

        [EventHandler]
        public async Task CreateUser(CreateUserCommand createUserCommand)
        {
            await _userDomainService.CheckEmailCode(createUserCommand.Input.Email
                      , createUserCommand.Input.Code);

            var existsEmail =await _chatContext.Users
                .AnyAsync(p=>p.Email==createUserCommand.Input.Email);

            if (existsEmail)
            {
                throw new UserFriendlyException("该邮箱已被注册");
            }

            var password = createUserCommand.Input.Password.SHA256();

            var user = new User(createUserCommand.Input.UserName
                ,password
                ,createUserCommand.Input.Email);

            await _chatContext.Users.AddAsync(user);   

            await _chatContext.SaveChangesAsync();
        }

        [EventHandler]
        public async Task SendCode(SendEmailCommand sendEmailCommand)
        {
          await _userDomainService.SendEmailCode(sendEmailCommand.email);
        }
    }
}
