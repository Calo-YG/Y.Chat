using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class GroupUserQuery(Guid GroupId,ChatType Type) : Query<List<GroupUserDto>>
    {
        public override List<GroupUserDto> Result {get ; set; }
    }
}
