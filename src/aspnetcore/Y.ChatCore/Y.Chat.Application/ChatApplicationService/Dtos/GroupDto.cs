namespace Y.Chat.Application.ChatApplicationService.Dtos
{
    public class GroupDto
    {
        public Guid Id { get; set; }    
        public string Name { get; private set; }

        public string? Description { get; private set; }

        public string Avatar { get; private set; }
    }
}
