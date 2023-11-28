using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Microsoft.AspNetCore.Http;

namespace Y.Chat.Application.FileApplicationService.Commands
{
    public record class UploadChatFileCommand(Guid chatId,IFormFile file):Command
    {
        public string Path { get; set; }
    }
}
