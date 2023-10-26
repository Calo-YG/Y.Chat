using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.FileApplicationService.Dtos;

namespace Y.Chat.Application.ChatApplicationService
{
    public interface IGroupApplicationService
    {
        Task Create(CreateGroupInput input);
        Task UploadAvatar([FromForm] IFormFile file);
        Task<List<GroupDto>> QueryGroup(GroupQueryInput input);
    }
}
