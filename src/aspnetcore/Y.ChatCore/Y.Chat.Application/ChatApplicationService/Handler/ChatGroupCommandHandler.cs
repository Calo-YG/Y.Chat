using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupCommandHandler
    {
        private readonly YChatContext _context;
        private readonly IFileDomainService _fileDomainService;

        public ChatGroupCommandHandler(YChatContext context, IFileDomainService fileDomainService)
        {
            _context = context;
            _fileDomainService = fileDomainService;
        }

        public async Task CreateGroup(CreateGroupCommand command)
        {
            var count = await _context.ChatGroups.CountAsync(p => p.Bachelors == command.UserId);

            if (count >= 20)
            {
                throw new UserFriendlyException("用户最多创建10个群聊");
            }

            var chat = new ChatGroup(command.Name, command.UserId, command.Decription);

            chat.SetGroupNumber();
            await _context.AddAsync(chat);
            await _context.SaveChangesAsync();
        }

        public async Task UploadGroupAvatar(UploadGroupAvatarCommand command)
        {
            var group = await _context.ChatGroups.FirstOrDefaultAsync(p => p.Id == command.GroupId);
            if (group is null)
            {
                return;
            }

            var minioname = $"{YChatConst.MinioAvatar}_{group.GroupNumber}_{command.Name}";

            await _fileDomainService.UploadMinio(command.File
                , minioname
                , command.ContentType);

            var file = new FileSystem(command.Name);
            file.SetGroupAvatar();
            file.SetMinioName(minioname);

            await _context.AddAsync(file);

            group.SetGroupAvatar(minioname);
            _context.ChatGroups.Update(group);
            await _context.SaveChangesAsync();  
        }
    }
}
