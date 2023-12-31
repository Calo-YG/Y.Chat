﻿using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public interface IMessageRepositroy:IRepository<Message,Guid>
    {
        Task<(List<MessageModel> Message, int Total)> QueryMessaeList(Guid chatId, Guid? userId, string? content, MessageType? messageType, DateTime? creationTime, int page, int pageSise);

        Task<Guid> GroupLastMessgeId(Guid groupId);
    }
}
