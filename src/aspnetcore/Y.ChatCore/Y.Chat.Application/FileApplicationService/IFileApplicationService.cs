using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Y.Chat.Application.FileApplicationService
{
    public interface IFileApplicationService
    {
        Task UploadAvatar([FromForm] IFormFile file);

        Task<IResult> GetFile(string filename);
    }
}
