using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageCommandHandler
    {
        private readonly YChatContext Context;
        public ChatMessageCommandHandler(YChatContext context)
        { 
             Context = context;
        }

        [EventHandler]
        public async Task CreateMessage(CreateMessageCommand cmd)
        {
            var message = new ChatMessage(cmd.UserId,
                cmd.GroupId,
                cmd.Content,
                cmd.Type);

            await Context.AddAsync(message); 

            await Context.SaveChangesAsync();   
        }
    }
}
