using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService.Queries;

public record class UserInfoQuery(Guid UserId) : Query<UserInfoDto>
{
    public override UserInfoDto Result { get; set; }
}
