using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools;
using Microsoft.AspNetCore.Mvc;
using Y.Chat.Application.FileApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Domain.UserDomain;

namespace Y.Chat.Application.FileApplicationService.Handler
{
    public class FileCommadHandler
    {
        private readonly IFileDomainService _fileDomainService;
        private readonly YChatContext _context;
        private readonly IUserDomainService _userDomainService;
        public FileCommadHandler(IFileDomainService fileDomainService
            ,YChatContext context
            ,IUserDomainService userDomainService) 
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


            var file = new FileSystem(command.FileName);
            file.SetAvatar();
            file.SetMinioName(minioname);   

            await _context.FileSystems.AddAsync(file);
            //await _context.SaveChangesAsync();

            await _userDomainService.SetAvatar(command.UserId, minioname);   
            command.FilePath= file.MinioName;
        }
    }
}
