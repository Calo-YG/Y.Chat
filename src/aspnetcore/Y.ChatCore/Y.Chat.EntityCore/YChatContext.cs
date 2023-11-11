using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore
{
    public class YChatContext: MasaDbContext<YChatContext>
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ChatGroup> ChatGroups { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        ///public DbSet<FriendMessage> FriendMessages { get; set; }    

        public DbSet<GroupUser> GroupUsers { get; set; }

        public DbSet<Notice> Notices { get; set; }  

        public DbSet<FileSystem> FileSystems { get; set; }

        public DbSet<EmailRecords> EmailRecords { get; set; }

        public DbSet<Friends> Friends { get; set; }

        public DbSet<SystemMessage> SystemMessages { get; set; }
        public YChatContext(MasaDbContextOptions<YChatContext> masaDbContextOptions) : base(masaDbContextOptions)
        {
        }


        protected override void OnConfiguring(MasaDbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreatingExecuting(ModelBuilder builder)
        {
            builder.CreatingModel();
            base.OnModelCreatingExecuting(builder);
        }
    }
}
