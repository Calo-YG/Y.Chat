using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.FileDomain.Entitis;
using Y.Chat.EntityCore.Domain.SystemDomain;
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
                options.Property(p => p.Avatar).HasDefaultValue("https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4").HasMaxLength(500);
                options.Property(p=>p.Autograph).HasMaxLength(50);
            });

            builder.Entity<Friends>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.Property(p=>p.Comment).HasMaxLength(20);
            });
            #endregion

            #region chat

            builder.Entity<Conversation>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.Property(p => p.Avatar).IsRequired().HasMaxLength(500);
                options.Property(p => p.Description).HasMaxLength(200);
                options.Property(p => p.Name).IsRequired().HasMaxLength(20);
                options.Property(p=>p.GroupNumber).IsRequired().HasMaxLength(30);   
            });

            builder.Entity<GroupUser>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.HasKey(x => new { x.UserId, x.GroupId });
            });

            builder.Entity<Message>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasIndex(p => p.Id);

                //options.HasKey(x => new { x.UserId ,x.GroupId});
            });

            builder.Entity<ChatList>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasIndex(p => p.Id);
            });

            builder.Entity<Notice>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);
            });

            builder.Entity<SystemMessage>(options =>
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

            #region System

            builder.Entity<RequestLogInfo>(options =>
            {
                options.TryConfigureConcurrencyStamp();
                options.HasKey(p => p.Id);

                options.Property(p => p.Exception).HasMaxLength(550);
            });

            #endregion

            #region 初始化数据
            var user = new User()
            {
                Name = "admin",
                Password= "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=",
                Email= "3164522206@qq.com",
                Account = "3164522206",
                Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4"
            };
            var current = new User()
            {
                Name = "wyg",
                Password = "zDPJCnWP9Y4Vzpe0s5pNRGz1crROpP9HrjkwlF9Q7x4=",
                Email = "3164522206@qq.com",
                Account = "3164522207",
                Avatar = "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4"
            };

            var conversation = new Conversation()
            {
                Name= "世界频道",
                Description= "世界频道欢迎来访",
                Avatar= "https://avatars.githubusercontent.com/u/74019004?s=400&u=bf9fc0cb7908138aed27fdd71cce648f29b624f5&v=4",
                GroupNumber= "3164522207"
            };
            var groupuser = new GroupUser(conversation.Id,user.Id);
            var currentuser = new GroupUser(conversation.Id,current.Id);
            builder.Entity<User>().HasData(user, current);
            builder.Entity<Conversation>().HasData(conversation);
            builder.Entity<GroupUser>().HasData(groupuser, currentuser);
            #endregion
        }
    }
}
