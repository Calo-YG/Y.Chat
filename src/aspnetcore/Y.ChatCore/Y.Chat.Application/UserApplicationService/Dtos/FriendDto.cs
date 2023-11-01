namespace Y.Chat.Application.UserApplicationService.Dtos
{
    public class FriendDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }    

        public string? Avatar {  get; set; }

        public string? Sign {  get; set; }

        private string comment;
        public string Comment { get=>comment; set {
                if (value != null)
                {
                    comment = value;
                }
                else
                {
                    comment = Name;
                }
            }
        }
    }
}
