using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
using Y.Chat.Application.FileApplicationService.Dtos;

namespace Y.Chat.Application.FileApplicationService.Queries
{
    public record class GetFileQuery(string FileName) : Query<FileStreamDto>
    {
        public override FileStreamDto Result { get; set; }
    }
}
