﻿using Calo.Blog.Common.UserSession;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupCommandHandler
    {
        private readonly YChatContext _context;
        private readonly IFileDomainService _fileDomainService;
        private readonly IGroupRepository _groupRepository;
        private IUserSession _currentuser;

        public ChatGroupCommandHandler(YChatContext context
            , IFileDomainService fileDomainService
            , IGroupRepository groupRepository
            , IUserSession currentuser)
        {
            _context = context;
            _fileDomainService = fileDomainService;
            _groupRepository = groupRepository;
            _currentuser = currentuser;
        }
        [EventHandler]
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

        [EventHandler]
        public async Task UploadGroupAvatar(UploadGroupAvatarCommand command)
        {
            var group = await _context.ChatGroups.FirstOrDefaultAsync(p => p.Id == command.GroupId);
            if (group is null)
            {
                return;
            }

            Guid userId;
            var tryparse=Guid.TryParse(_currentuser.UserId, out userId);
            if (!tryparse)
            {
                throw new UserFriendlyException("用户信息异常");
            }
            var grouponwer =await _groupRepository.IsGroupOnwer(group.Id, userId);
            if (!grouponwer)
            {
                throw new UserFriendlyException("没有修改群聊头像权限");
            }

            var minioname = $"{YChatConst.MinioAvatar}_{group.GroupNumber}_{command.Name}";

            await _fileDomainService.UploadMinio(command.File
                , minioname
                , command.ContentType);

            await command.File.DisposeAsync();    

            var file = new FileSystem(command.Name);
            file.SetGroupAvatar();
            file.SetMinioName(minioname);

            await _context.AddAsync(file);

            group.SetGroupAvatar(minioname);

            _context.ChatGroups.Update(group);
            await _context.SaveChangesAsync();  
        }
        [EventHandler]
        public async Task JoinGroup(JoinGroupCommand command)
        {
            var join =await _groupRepository.InGroup(command.GroupId, command.UserId);
            if (join)
            {
                return;
            }
            var groupUser = new GroupUser(command.GroupId, command.UserId);

            await _context.GroupUsers.AddAsync(groupUser);

            await _context.SaveChangesAsync();  
        }
    }
}
