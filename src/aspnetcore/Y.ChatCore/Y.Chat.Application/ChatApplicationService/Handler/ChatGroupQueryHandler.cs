﻿using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Minio.DataModel;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using ChatType = Y.Chat.EntityCore.Domain.ChatDomain.Shared.ChatType;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupQueryHandler
    {
        private readonly YChatContext _context;
        private readonly IGroupRepository _groupRepository;
        private readonly IChatListRepositroy _chatListRepository;

        public ChatGroupQueryHandler(YChatContext context,
            IGroupRepository groupRepository,
            IChatListRepositroy chatListRepository)
        {
            _context = context;
            _groupRepository = groupRepository;
            _chatListRepository = chatListRepository;   
        }
        /// <summary>
        /// 搜索群聊
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task QueryGroup(GroupQuery query)
        {
            var list = await _context.ChatGroups
                .Where(p => p.Name.Contains(query.Name) || p.GroupNumber.Contains(query.No))
                .ToListAsync();

            query.Result = list.Map<List<GroupDto>>();
        }
        [EventHandler]
        public async Task UserGroup(UserGroupQuery query)
        {
            var list = await _groupRepository.UserGroups(query.userId).ToListAsync();
            query.Result = list.Map<List<GroupDto>>();
        }
        [EventHandler]
        public async Task GroupUser(GroupUserQuery query)
        {
            List<User>? users;

            if (query.Type == ChatType.Default)
            {
                users = await _chatListRepository.GetUsersByChatId(query.GroupId).ToListAsync();

                query.Result = users.Map<List<GroupUserDto>>();

                return;
            }
            
            users = await _groupRepository.GroupUsers(query.GroupId).ToListAsync();

            var data = users.Map<List<GroupUserDto>>();

            var key = $"{ChatConst.Group}_{query.GroupId}";

            string[]? onlinuser = null;

            var exists = await RedisHelper.ExistsAsync(key);

            if (exists)
            {
                onlinuser = await RedisHelper.LRangeAsync(key,0,-1);
                onlinuser = onlinuser.Distinct().ToArray();
            }

            foreach (var user in data)
            {
                var userid = user.Id.ToString();

                user.Online = onlinuser?.Contains(userid) ?? false;
            }

            query.Result = data;
        }

    }
}
