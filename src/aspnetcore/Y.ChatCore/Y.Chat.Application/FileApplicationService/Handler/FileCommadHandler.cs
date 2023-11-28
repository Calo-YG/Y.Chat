using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools;
using Y.Chat.Application.FileApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Domain.FileDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain;

namespace Y.Chat.Application.FileApplicationService.Handler
{
    public class FileCommadHandler
    {
        private readonly IFileDomainService _fileDomainService;
        private readonly YChatContext _context;
        private readonly IUserDomainService _userDomainService;
        private readonly IEventBus _eventBus;
        public FileCommadHandler(IFileDomainService fileDomainService
            ,YChatContext context
            ,IUserDomainService userDomainService
            ,IEventBus eventBus) 
        {
           _context = context;
           _userDomainService = userDomainService;
           _fileDomainService = fileDomainService;
        }
        [EventHandler]
        public async Task UploadAvatar(UploadAvatarCommand command)
        {
            Random rnd = new Random();
            var num = rnd.StrictNext();

            var minioname = $"{YChatConst.MinioAvatar}_{num}_{command.FileName}";

            await _fileDomainService.UploadMinio(command.File
                , minioname
                , command.ContentType);

            await command.File.DisposeAsync();

            var file = new FileSystem(command.FileName);
            file.SetAvatar();
            file.SetMinioName(minioname);   

            await _context.FileSystems.AddAsync(file);

            await _userDomainService.SetAvatar(command.UserId, minioname);   
            command.FilePath= file.MinioName;

            command.UnitOfWork.EntityState = EntityState.Changed;
        }

        [EventHandler]
        public async Task UploadGroupFile(UploadGroupFileCommand cmd)
        {
            await _fileDomainService.UploadMinio(cmd.File
                  , cmd.FileName
                  , cmd.ContentType);

            await cmd.File.DisposeAsync();

            var file = new FileSystem(cmd.FileName,
                 "群聊文件",
                 false,
                 FileType.GroupFile,
                 cmd.GroupId,
                 cmd.ParentId);

           await _context.FileSystems.AddAsync(file);

           await _context.SaveChangesAsync();
        }

        [EventHandler]
        public async Task UploadChatFile(UploadChatFileCommand cmd)
        {
            var filename = $"{cmd.file.FileName}";
            var stream = cmd.file.OpenReadStream();

            await _fileDomainService.UploadMinio(stream,
                filename,
                cmd.file.ContentType);

            var file = new FileSystem(filename,
                      "群聊文件",
                      false,
                      FileType.GroupFile,
                      cmd.chatId,
                      null);

            await _context.FileSystems.AddAsync(file);

            await _context.SaveChangesAsync();

            cmd.Path = filename;
        }
    }
}
