using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageCommandHandler
    {
        private readonly YChatContext Context;
        private readonly IRepository<Message> _messageRepository;
        public ChatMessageCommandHandler(YChatContext context,
            IRepository<Message> messageRepositroy)
        { 
             Context = context;
             _messageRepository = messageRepositroy;
        }

        [EventHandler]
        public async Task CreateMessage(CreateMessageCommand cmd)
        {
            var message = new Message(cmd.UserId,
                cmd.GroupId,
                cmd.Content,
                cmd.Type);

            await Context.AddAsync(message); 

            await Context.SaveChangesAsync();   
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
