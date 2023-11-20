using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using EntityState = Masa.BuildingBlocks.Data.UoW.EntityState;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageCommandHandler
    {
        private readonly YChatContext Context;
        private readonly IRepository<Message> _messageRepository;
        private readonly IChatDomainService _chatDomainService;
        public ChatMessageCommandHandler(YChatContext context,
            IRepository<Message> messageRepositroy,
            IChatDomainService chatDomainService)
        { 
             Context = context;
             _messageRepository = messageRepositroy;
             _chatDomainService = chatDomainService;
        }

        [EventHandler]
        public async Task CreateMessage(CreateMessageCommand cmd)
        {
            var message = new Message(cmd.UserId,
                cmd.GroupId,
                cmd.Content,
                cmd.Type);

            await Context.AddAsync(message);

            await _chatDomainService.UpdateChatListMessage(cmd.GroupId, message.Id);

            cmd.UnitOfWork.EntityState = EntityState.Changed;
        }
        [EventHandler]
        public  async Task DeleteFriendAndGroupMessageHandler(DeleteFriendAllMessageEvent evnet)
        {
            var messages = await Context.ChatMessages.Where(p => p.GroupId == evnet.GroupId).ToListAsync();

            await _messageRepository.RemoveRangeAsync(messages);

            await Context.SaveChangesAsync();
        }
    }
}
