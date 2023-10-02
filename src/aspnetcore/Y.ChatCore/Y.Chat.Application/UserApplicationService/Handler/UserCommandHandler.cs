using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.UserApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserCommandHandler
    {
        private readonly YChatContext _chatContext;
        private readonly IRepository<User, Guid> _userRepository;
        public UserCommandHandler(IRepository<User,Guid> userRepositroy,YChatContext context)
        {
            _userRepository = userRepositroy;
            _chatContext = context; 
        }

        [EventHandler(Order =1)]
        public void Code(CreateUserCommand createUserCommand)
        {
        }
        [EventHandler(Order = 2)]
        public void CreateUser(CreateUserCommand createUserCommand)
        {

        }
    }
}
