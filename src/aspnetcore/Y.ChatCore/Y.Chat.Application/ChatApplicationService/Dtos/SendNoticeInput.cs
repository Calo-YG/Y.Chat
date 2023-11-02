namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class SendNoticeInput
    {
        public bool IsGroup { get; set; }

        public Guid UserId { get; set; }

        public Guid? ApplyUserId { get; set; }

        public Guid? ApplyGroupId { get; set; }

        public string Remark { get; set; }
    }
}
