

using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.FileApplicationService.Commands
{
    public record class UploadAvatarCommand(Stream File,string FileName,string ContentType,Guid UserId):Command
    {
        public string FilePath { get; set; }
    }
}
