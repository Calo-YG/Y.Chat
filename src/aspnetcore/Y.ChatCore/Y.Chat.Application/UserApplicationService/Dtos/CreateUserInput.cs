namespace Y.Chat.Application.UserApplicationService.Dtos
{
    public class CreateUserInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }
    }
}
