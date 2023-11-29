namespace Y.Chat.EntityCore.Domain.ChatDomain.Shared
{
    public class NoticeModel
    {
        public Guid Id { get; set; }    

        public Guid RequestUserId { get; set; }

        public string RequestUserName { get; set; }

        public string? RequestAvatar { get; set; }

        public string Content { get; set; }

        public Guid RecivedId { get; set; } 

        public NoticeType NoticeType { get; set; }

        public bool Agree {  get; set; }

        public bool Read {  get; set; }

        public DateTime CreationTime { get; set; }
    }
}
