using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class ChatListQuery(Guid UserId) : Query<List<ChatListDto>>
    {
        public override List<ChatListDto> Result { get ; set ; }
    }
}
