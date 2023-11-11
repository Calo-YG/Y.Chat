using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class CreateNotifyCommand(Guid UserId,string Content,NotfiyType Type,Guid? Recived=null):Command;
}
