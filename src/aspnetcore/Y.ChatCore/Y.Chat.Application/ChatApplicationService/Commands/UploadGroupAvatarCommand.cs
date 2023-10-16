using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class UploadGroupAvatarCommand(Stream File,string Name,Guid GroupId,string ContentType):Command;
}
