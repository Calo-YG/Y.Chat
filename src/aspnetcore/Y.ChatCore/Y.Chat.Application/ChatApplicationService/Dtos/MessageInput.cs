using Y.Chat.Application.Base;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class MessageInput:PageInput
    {
        public Guid ChatId { get; set; }

        public string? Content { get; set; } 

        public Guid? UserId { get; set; }

        public MessageType? MessageType { get; set; }

        public DateTime? CreationTime { get; set; }
    }
}
