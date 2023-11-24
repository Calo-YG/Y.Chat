using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.ChatApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService.Queries
{
    public record class ChatListFindQuery(Guid ChatId,Guid UserId) : Query<ChatListDto?>
    {
        public override ChatListDto? Result { get; set; }
    }
}
