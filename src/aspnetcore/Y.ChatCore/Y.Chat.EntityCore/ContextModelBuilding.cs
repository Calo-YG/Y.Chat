using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore
{
    public static class ContextModelBuilding
    {
        public static void CreatingModel(this ModelBuilder builder)
        {
            #region 用户
            builder.Entity<User>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);
                //唯一约束
                options.HasIndex(p => p.Account).IsUnique();

                options.Property(p => p.Email).HasMaxLength(30);
                options.Property(p => p.Account).HasMaxLength(30);
                options.Property(p => p.Password).HasMaxLength(200);
                options.Property(p => p.Avatar).HasMaxLength(200);
            });

            builder.Entity<Friends>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
            });
            #endregion

            #region chat

            builder.Entity<ChatGroup>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.Property(p => p.Avatar).IsRequired().HasMaxLength(200);
                options.Property(p => p.Description).HasMaxLength(200);
                options.Property(p => p.Name).IsRequired().HasMaxLength(20);
                options.Property(p=>p.GroupNumber).IsRequired().HasMaxLength(30);   
            });

            builder.Entity<GroupUser>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.HasKey(x => new { x.UserId, x.GroupId });

                options.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
                options.HasOne(p => p.ChatGroup).WithMany().HasForeignKey(p => p.GroupId);
            });

            builder.Entity<ChatMessage>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasIndex(p => p.Id);

                options.HasKey(x => new { x.UserId, x.GroupId });

                options.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
                options.HasOne(p => p.ChatGroup).WithMany().HasForeignKey(p => p.GroupId);
            });

            builder.Entity<FriendMessage>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasIndex(p => p.Id);
            });

            builder.Entity<Notice>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);
            });
            #endregion

            #region file
            builder.Entity<FileSystem>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.Property(p=>p.Name).IsRequired().HasMaxLength(100);
                options.Property(p=>p.Description).HasMaxLength(200);

                options.HasOne(p => p.ChatGroup).WithMany().HasForeignKey(p => p.GroupId);
            });
            #endregion
        }
    }
}
