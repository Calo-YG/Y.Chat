using Calo.Blog.Common.UserSession;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Hubs;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupCommandHandler
    {
        private readonly YChatContext _context;
        private readonly IFileDomainService _fileDomainService;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserSession _currentuser;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IEventBus _eventbus;
        private readonly IMessageRepositroy _messageRepository;

        public ChatGroupCommandHandler(YChatContext context
            , IFileDomainService fileDomainService
            , IGroupRepository groupRepository
            , IUserSession currentuser
            , IHubContext<ChatHub> hubContext
            , IEventBus eventBus
            , IMessageRepositroy messageRepository)
        {
            _context = context;
            _fileDomainService = fileDomainService;
            _groupRepository = groupRepository;
            _currentuser = currentuser;
            _hubContext = hubContext;
            _eventbus = eventBus;
            _messageRepository = messageRepository;
        }
        [EventHandler(Order =1)]
        public async Task CreateGroup(CreateGroupCommand command)
        {
            var count = await _context.ChatGroups.CountAsync(p => p.UserId == command.UserId);

            if (count >= 10)
            {
                throw new UserFriendlyException("用户最多创建10个群聊");
            }

            var chat = new Conversation(command.Name, command.Decription);

            chat.SetUser(command.UserId);

            chat.SetGroupNumber();

            var groupUser = new GroupUser(chat.Id, command.UserId);

            await _context.GroupUsers.AddAsync(groupUser);

            var key = $"{ChatConst.Online}_{command.UserId}";

            var exists = await RedisHelper.ExistsAsync(key);

            if (exists)
            {
                var connectionid = await RedisHelper.GetAsync(key);

                var groupkey = $"{ChatConst.Group}_{chat.Id}";

                await _hubContext.Groups.AddToGroupAsync(connectionid, chat.Id.ToString("N"));

                await RedisHelper.LPushAsync(groupkey, command.UserId);
            }
            
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
        }
        /// <summary>
        /// 加入群聊
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        [EventHandler]
        public async Task JoinGroup(JoinGroupCommand command)
        {
            var group = await _context.ChatGroups.FirstOrDefaultAsync(p=>p.Id == command.GroupId);
            if (group is null) 
            {
                throw new UserFriendlyException("群聊不存在");
            }
            var join =await _groupRepository.InGroup(command.GroupId, command.UserId);
            if (join)
            {
                return;
            }
            var groupUser = new GroupUser(command.GroupId, command.UserId);

            await _context.GroupUsers.AddAsync(groupUser);

            var key = $"{ChatConst.Online}_{command.UserId}";

            var exists = await RedisHelper.ExistsAsync(key);
            
            var lastMessageId = await _messageRepository.GroupLastMessgeId(command.GroupId);

            var chatList = new ChatList(command.UserId,
                command.GroupId,
                group.Name,
                group.Avatar,
                EntityCore.Domain.ChatDomain.Shared.ChatType.Group,
                lastMessageId);

            await _context.AddAsync(chatList);

            await _context.SaveChangesAsync();

            if (exists)
            {
                var connectionid = await RedisHelper.GetAsync(key);

                var groupkey = $"{ChatConst.Group}_{command.GroupId}";

                await _hubContext.Groups.AddToGroupAsync(connectionid, command.GroupId.ToString("N"));

                await RedisHelper.LPushAsync(groupkey, command.UserId);
            }
        }
    }
}
