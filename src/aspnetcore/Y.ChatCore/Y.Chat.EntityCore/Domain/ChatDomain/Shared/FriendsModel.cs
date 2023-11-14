namespace Y.Chat.EntityCore.Domain.ChatDomain.Shared
{
    public class FriendsModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public string? Sign { get; set; }    

        public Guid ChatId { get; set; }

        public string Avatar { get; set; }
    }
}
