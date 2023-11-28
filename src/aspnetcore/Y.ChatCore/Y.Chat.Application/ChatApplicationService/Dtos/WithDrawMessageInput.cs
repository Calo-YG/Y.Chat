namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class WithDrawMessageInput
    {
        public Guid ChatId { get; set; }

        public Guid UserId { get; set; }

        public Guid MessageId { get; set; }
    }
}
