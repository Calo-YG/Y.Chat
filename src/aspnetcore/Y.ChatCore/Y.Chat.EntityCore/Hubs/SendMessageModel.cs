namespace Y.Chat.EntityCore.Hubs
{
    public class SendMessageModel
    {
        public Guid? RecivedUserId { get; set; }

        public string Message { get; set; }

        public Guid? GroupId { get; set; }
    }
}
