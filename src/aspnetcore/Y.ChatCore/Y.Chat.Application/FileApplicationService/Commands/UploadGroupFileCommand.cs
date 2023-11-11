using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.FileApplicationService.Commands
{
    public record class UploadGroupFileCommand(Stream File,Guid GroupId,Guid? ParentId,string ContentType,string FileName):Command;
}
