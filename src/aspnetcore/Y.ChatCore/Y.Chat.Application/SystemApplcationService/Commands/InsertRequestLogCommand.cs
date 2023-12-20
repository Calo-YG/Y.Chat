using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.SystemApplcationService.Commands
{
    public record class InsertRequestLogCommand(string Method,string Path,string? Ip,string? Param,string Duration,string? Exception,Guid? RequestUserId):Command;
}
