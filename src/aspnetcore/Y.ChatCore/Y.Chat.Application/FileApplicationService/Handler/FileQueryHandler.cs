using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.FileApplicationService.Dtos;
using Y.Chat.Application.FileApplicationService.Queries;
using Y.Chat.EntityCore.Domain.FileDomain;

namespace Y.Chat.Application.FileApplicationService.Handler
{
    public class FileQueryHandler
    {
        private readonly IFileDomainService _fileDomainService;
        public FileQueryHandler(IFileDomainService fileDomainService) 
        {
            _fileDomainService = fileDomainService;
        }
        [EventHandler]
        public async Task GetFile(GetFileQuery query)
        {
            var output = await _fileDomainService.GetFile(query.FileName);

            query.Result = new FileStreamDto()
            {
                ContentType = output.ContentType,
                FileStream = output.Stream
            };
        }
    }
}
