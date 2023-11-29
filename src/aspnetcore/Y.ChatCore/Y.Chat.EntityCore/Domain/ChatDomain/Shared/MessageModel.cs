namespace Y.Chat.EntityCore.Domain.ChatDomain.Shared
{
    public class MessageModel
    {
        public Guid Id { get; set; }    

        public string Content { get; set; }

        public MessageType MessageType { get; set; }    

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public Guid ChatId { get; set; }

        public string Avatar {  get; set; }   

        public bool WithDraw { get; set; }
    }
}
