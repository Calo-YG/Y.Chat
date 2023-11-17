namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }

        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

        public string Content { get; set; }
    }
}
