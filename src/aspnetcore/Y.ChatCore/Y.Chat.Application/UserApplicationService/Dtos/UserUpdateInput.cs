namespace Y.Chat.Application.UserApplicationService.Dtos
{
    public class UserUpdateInput
    {
        public Guid Id { get; set; }

        public string? Sign { get; set; }

        public string? Name { get; set; }

        public string? Password { get; set; }

        public string? Avatar { get; set; }
    }
}
