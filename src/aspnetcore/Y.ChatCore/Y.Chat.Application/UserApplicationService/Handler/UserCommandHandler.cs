﻿using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Masuit.Tools.Security;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.UserApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Events;

namespace Y.Chat.Application.UserApplicationService.Handler
{
    public class UserCommandHandler
    {
        private readonly YChatContext _chatContext;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IUserDomainService _userDomainService;
        private readonly IEventBus _eventBus;
        private readonly IChatListRepositroy _chatListRepository;
        public UserCommandHandler(IRepository<User,Guid> userRepositroy
            ,YChatContext context
            ,IUserDomainService userDomainService
            , IEventBus eventBus
            , IChatListRepositroy chatListRepositroy)
        {
            _userRepository = userRepositroy;
            _chatContext = context; 
            _userDomainService = userDomainService;
            _eventBus = eventBus;
            _chatListRepository=chatListRepositroy;
        }

        [EventHandler]
        public async Task CreateUser(CreateUserCommand createUserCommand)
        {
            await _userDomainService.CheckEmailCode(createUserCommand.Input.Email
                      , createUserCommand.Input.Code);

            var existsEmail =await _chatContext.Users
                .AnyAsync(p=>p.Email==createUserCommand.Input.Email);

            if (existsEmail)
            {
                throw new UserFriendlyException("该邮箱已被注册");
            }

            var password = createUserCommand.Input.Password.SHA256();

            var user = new User(createUserCommand.Input.UserName
                ,password
                ,createUserCommand.Input.Email);

            await _chatContext.Users.AddAsync(user);

            var defaultgroup = await _chatContext.ChatGroups.FirstOrDefaultAsync(p => p.Name == "世界频道");

            var defaultuser = await _chatContext.Users.FirstOrDefaultAsync(p => p.Name == "lhl");

            var joindefaultusercmd = new CreateFriendCommand(user.Id, defaultuser.Id, "lhl");

            var cmd = new JoinGroupCommand(defaultgroup.Id, user.Id);

            await _eventBus.PublishAsync(joindefaultusercmd);

            await _eventBus.PublishAsync(cmd);
        }

        [EventHandler]
        public async Task SendCode(SendEmailCommand sendEmailCommand)
        {
          await _userDomainService.SendEmailCode(sendEmailCommand.email);
        }
        [EventHandler]
        public async Task SetSign(SetUserSignCommand command) 
        {
            var user =await _chatContext.Users.FirstOrDefaultAsync(p => p.Id == command.UserId);
            if(user is null)
            {
                throw new UserFriendlyException("用户不存在");
            }

            user.SetAutograph(command.Sign);
            _chatContext.Users.Update(user); 
        }
        /// <summary>
        /// 修改好友备注
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task UpdateFriendRemark(UpdateFriendReamrkCommand cmd)
        {
            var entity = await _chatContext.Friends.FirstOrDefaultAsync(p=>p.UserId==cmd.UserId&&p.FriendId==cmd.FriendId);

            if(entity is null)
            {
                throw new UserFriendlyException("尚未添加好友");
            }

            entity.SetComment(cmd.Content);

            _chatContext.Update(entity);
        }
        [EventHandler]
        public async Task UpdateChatListAvatar(UpdateAvatarEvent @event)
        {
            var chatlist = await _chatListRepository.GetListAsync(p => p.FriendId == @event.userId);

            foreach (var chat in chatlist)
            {
                chat.SetAvatar(@event.avatar);
            }

            await _chatListRepository.UpdateRangeAsync(chatlist);
        }
        [EventHandler]
        public async Task UpdateUser(UserUpdateCommand cmd)
        {
            var user = await _chatContext.Users.FirstOrDefaultAsync(p => p.Id == cmd.id);

            if(user is null)
            {
                throw new UserFriendlyException("");
            }

            user.SetName(cmd.Name);
            user.SetAutograph(cmd.Sign);
            user.SetPassword(cmd.Password);

            _chatContext.Update(user);

            cmd.UnitOfWork.EntityState = Masa.BuildingBlocks.Data.UoW.EntityState.Changed;
        }
    }
}
