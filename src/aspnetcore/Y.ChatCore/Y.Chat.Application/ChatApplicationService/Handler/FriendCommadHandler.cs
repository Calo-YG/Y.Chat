using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class FriendCommadHandler
    {
        private readonly YChatContext Context;
        public  FriendCommadHandler(YChatContext context)
        {
            Context = context;
        }

        public async Task CreateFriend(CreateFriendCommand cmd)
        {
            var frienduer = await Context.Users.FirstOrDefaultAsync(p => p.Id == cmd.FriendId);
            if (frienduer is null)
            {
                throw new UserFriendlyException("用户不存在");
            }
            var comment = cmd.Comment ?? frienduer.Name;
            var friend = new Friends(cmd.UserId,
                cmd.FriendId,
                comment);

            var group = new ChatGroup(comment,
                null,
                "",
                "");

            GroupUser user = new GroupUser();
            GroupUser friendUser = new GroupUser();

            List<GroupUser> groupUsers = new List<GroupUser>()
            {
                user,
                friendUser,
            };

            await Context.AddAsync(friend);
            await Context.AddAsync(group);
            await Context.AddRangeAsync(groupUsers);
            await Context.SaveChangesAsync();
        }
    }
}
