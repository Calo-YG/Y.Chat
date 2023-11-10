using Y.Chat.EntityCore;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageCommandHandler
    {
        private readonly YChatContext Context;
        public ChatMessageCommandHandler(YChatContext context)
        { 
             Context = context;
        }
    }
}
