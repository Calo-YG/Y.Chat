using Y.Chat.EntityCore;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageQueryHandler
    {
        private readonly YChatContext Context;
        public ChatMessageQueryHandler(YChatContext context) 
        {
            Context = context;  
        }    

    }
}
