namespace Y.Chat.Application.UserApplicationService.Dtos
{
    public class UpdateFriendRemarkInput
    {
        public Guid UserId { get; set; }

        public Guid FriendId { get; set; }

        public string Content { get; set; }
    }
}
