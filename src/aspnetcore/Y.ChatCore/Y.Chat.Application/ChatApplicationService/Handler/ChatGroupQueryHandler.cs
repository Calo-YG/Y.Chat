﻿using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupQueryHandler
    {
        private readonly YChatContext _context;
        private readonly IGroupRepository _groupRepository;

        public ChatGroupQueryHandler(YChatContext context,
            IGroupRepository groupRepository)
        {
            _context = context;
            _groupRepository = groupRepository;
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

    }
}