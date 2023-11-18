using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class ChatListDto
    {
        public Guid UserId { get; set; }

        public Guid ConversationId { get;  set; }

        public int UnReadCount { get;  set; }

        public Guid? LastMessageId { get;  set; }

        public string Avatart { get;  set; }

        public string Name { get;  set; }

        public string Content { get;  set; }

        public MessageType MessageType { get;  set; }

        public DateTime LastMessageTime { get; set; }
    }
}
