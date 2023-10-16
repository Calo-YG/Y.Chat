namespace Y.Chat.Application.FileApplicationService.Dtos
{
    public class CreateGroupInput
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } 
    }
}
