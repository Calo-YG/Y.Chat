using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class UserGroupQuery(Guid userId) : Query<List<GroupDto>>
    {
        public override List<GroupDto> Result { get; set; }
    }
}
