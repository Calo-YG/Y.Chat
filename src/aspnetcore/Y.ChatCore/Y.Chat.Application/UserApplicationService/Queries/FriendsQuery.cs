using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.UserApplicationService.Dtos;

namespace Y.Chat.Application.UserApplicationService.Queries
{
    public record class FriendsQuery(Guid UserId) : Query<List<FriendDto>>
    {
        public override List<FriendDto> Result { get ; set ; }
    }
}
