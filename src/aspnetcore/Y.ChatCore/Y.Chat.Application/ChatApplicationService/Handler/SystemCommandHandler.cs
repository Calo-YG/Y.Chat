using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class SystemCommandHandler
    {
        private readonly YChatContext Context;
        public SystemCommandHandler(YChatContext context) 
        {
            Context = context;  
        }
        [EventHandler]
        public async Task Create(CreateNotifyCommand cmd)
        {
            var message = new SystemMessage(cmd.UserId,
                cmd.Content,
                cmd.Type,
                cmd.Recived);

            await Context.AddAsync(message);

            await Context.SaveChangesAsync();
        }
    }
}
