namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class GroupUserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Avatar { get; set; }

        public string? Sign { get; set; }

        public string Comment { get; set; }

        public bool Online { get; set; }
    }
}
