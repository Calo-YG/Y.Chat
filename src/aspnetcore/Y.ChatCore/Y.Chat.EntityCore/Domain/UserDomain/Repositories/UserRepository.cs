﻿using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.UserDomain.Repositories
{
    public class UserRepository : Repository<YChatContext, User, Guid>, IUserRepository
    {
        public UserRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {

        }
        /// <summary>
        /// 获取用户好友
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<User> GetFriends(Guid userId)
        {
            var query= from f in Context.Friends
                       .Where(p => p.UserId == userId)
                       join u in Context.Users on f.FriendId equals u.Id
                       select u;

            return query;
        }
    }
}
