using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService.Queries
{
    public record class LoginQuery(LoginInput Input) : Query<AuthenticationDto>
    {
        public override AuthenticationDto Result { get ; set ; }
    }
}
