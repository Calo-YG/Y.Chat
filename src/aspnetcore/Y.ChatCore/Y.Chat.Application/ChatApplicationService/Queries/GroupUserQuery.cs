using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class GroupUserQuery(Guid GroupId) : Query<List<GroupUserDto>>
    {
        public override List<GroupUserDto> Result {get ; set; }
    }
}
