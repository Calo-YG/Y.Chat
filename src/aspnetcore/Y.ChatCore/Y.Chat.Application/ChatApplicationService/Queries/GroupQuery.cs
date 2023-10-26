using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class GroupQuery(string Name,string No) : Query<List<GroupDto>>
    {
        public override List<GroupDto> Result { get; set ; }
    }
}
