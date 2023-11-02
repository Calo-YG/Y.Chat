namespace Y.Chat.EntityCore.Domain.UserDomain.Shared
{
    public class UserFriendModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Avatar { get; set; }

        public string? Sign { get; set; }
        private string comment;
        public string Comment { get=>comment; set { 
            
                if(value is null || string.IsNullOrEmpty(value))
                {
                    comment = this.Name;
                }
                else
                {
                    comment = value;
                }
            }
        }
    }
}
