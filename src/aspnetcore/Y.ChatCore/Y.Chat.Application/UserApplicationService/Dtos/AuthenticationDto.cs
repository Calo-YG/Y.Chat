namespace Y.Chat.Application.UserApplicationService.Dtos
{
    public class AuthenticationDto
    {
        public string UserName { get; set;}
        public Guid UserId { get; set;}
        public string Token {  get; set;}  
        public string Avatar { get; set; }

        public string Email { get; set; }   

        public string? Sign { get; set; }
    }
}
