﻿using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Masuit.Tools;
using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public class MessageRepository : Repository<YChatContext, Message, Guid>, IMessageRepositroy
    {
        public MessageRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<(List<MessageModel> Message, int Total)> QueryMessaeList(Guid chatId,Guid? userId,string? content,MessageType? messageType,DateTime? creationTime,int page,int pageSise)
        {
            var query = (from m in Context.ChatMessages

                         join u in Context.Users on m.UserId equals u.Id 

                         select new MessageModel()
                         {
                             Id = m.Id,
                             ChatId = m.GroupId,
                             UserId = m.UserId,
                             Avatar = u.Avatar,
                             Name = u.Name,
                             Content = m.Content,
                             MessageType = m.MessageType,
                             Created = m.CreationTime,
                             WithDraw=m.Withdraw
                         })
                         .WhereIf(userId.HasValue,p=>p.UserId==userId)
                         .WhereIf(!string.IsNullOrEmpty(content),p=>p.Content.Contains(content)&&p.MessageType==MessageType.Text)
                         .WhereIf(messageType.HasValue,p=>p.MessageType==messageType)
                         .WhereIf(creationTime.HasValue,p=>p.Created<=creationTime);

            var count = await query.CountAsync();

            page = page > 0 ? page : 0;

            var data = await query
                .OrderByDescending(p=>p.Created)
                .Skip(page*pageSise)
                .Take(pageSise)
                .ToListAsync();

            data = data.OrderBy(p => p.Created).ToList();

            return (data,count);
        }

        public  Task<Guid> GroupLastMessgeId(Guid groupId)
        {
            return Context.ChatMessages.Where(p=>p.GroupId==groupId)
                .OrderBy(p=>p.CreationTime)
                .Select(p=>p.Id)
                .FirstAsync();
        }
    }
}
