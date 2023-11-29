using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class NoticeDto
    {
        public Guid Id { get; set; }

        public Guid RequestUserId { get; set; }

        public string RequestUserName { get; set; }

        public string? RequestAvatar { get; set; }

        public string Content { get; set; }

        public Guid RecivedId { get; set; }

        public NoticeType NoticeType { get; set; }

        public bool Agree { get; set; }

        public bool Read { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
