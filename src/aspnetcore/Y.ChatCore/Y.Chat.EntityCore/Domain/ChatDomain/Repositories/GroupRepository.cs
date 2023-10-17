﻿using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public class GroupRepository : Repository<YChatContext, ChatGroup, Guid>, IGroupRepository
    {
        public GroupRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public Task<bool> InGroup(Guid groupId, Guid userId)
        {
          return  Context.GroupUsers.AnyAsync(p=>p.UserId == userId&&p.GroupId==groupId); 
        }
    }
}