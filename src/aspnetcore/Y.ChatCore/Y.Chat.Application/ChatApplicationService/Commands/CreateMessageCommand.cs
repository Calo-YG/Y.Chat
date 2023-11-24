using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class CreateMessageCommand(Guid UserId, Guid GroupId, string Content, MessageType Type) : Command
    {
        public Guid MessageId { get; set; }
    }
}
