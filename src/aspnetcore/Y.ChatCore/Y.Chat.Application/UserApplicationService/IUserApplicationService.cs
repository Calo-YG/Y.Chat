using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService
{
    public interface IUserApplicationService
    {
        Task Create(CreateUserInput input);
    }
}
