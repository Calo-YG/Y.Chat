using Microsoft.EntityFrameworkCore;

namespace Y.Chat.EntityCore
{
    public class YChatContext: MasaDbContext<YChatContext>
    {
        public YChatContext(MasaDbContextOptions<YChatContext> masaDbContextOptions) : base(masaDbContextOptions)
        {
        }
    }
}
