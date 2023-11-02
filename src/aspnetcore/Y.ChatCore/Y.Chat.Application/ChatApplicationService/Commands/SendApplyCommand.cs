using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Events
{
    public record SendApplyCommand(bool IsGroup, Guid UserId, Guid? ApplyUserId,Guid? ApplyGroupId,string Remark) :Command;
}
