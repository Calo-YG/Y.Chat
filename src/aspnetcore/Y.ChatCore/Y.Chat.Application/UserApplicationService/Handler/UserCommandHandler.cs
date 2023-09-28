using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserCommandHandler
    {
        private readonly IRepository<User, Guid> _userRepository;
        public UserCommandHandler(IRepository<User,Guid> userRepositroy)
        {
            _userRepository = userRepositroy;
        }

        [EventHandler]
        public void Createuser()
        {

        }
    }
}
