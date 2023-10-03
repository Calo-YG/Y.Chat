using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService.Queries
{
    public record class LoginQuery(LoginInput Input) : Query<string>
    {
        public override string Result { get ; set ; }
    }
}
