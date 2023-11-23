using Microsoft.Identity.Client;

namespace Y.Chat.Application.UserApplicationService.Dtos;

public  class UserInfoDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Avatar { get; set; }

    public string Email { get; set; }   

    public string Sign { get; set; }
}
