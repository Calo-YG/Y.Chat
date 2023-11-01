using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class NoticeCommandHandler
    {
        private readonly YChatContext _context;
        public NoticeCommandHandler(YChatContext context) 
        { 
            _context = context;
        }

        [EventHandler]
        public async Task SendNotice(SendApplyEvent @event)
        {
            var inviteuserId = @event.ApplyUserId;
            List<Notice> notices = new List<Notice>();
            Notice notice;
            var applyuser = await _context.Users.FirstOrDefaultAsync(p => p.Id == @event.UserId);
            var content = @event.IsGroup?$"{applyuser.Name}入群申请":$"{applyuser.Name}好友申请";
            if (@event.IsGroup)
            {
                var userids=await _context.GroupUsers.Where(p=>p.GroupId==@event.ApplyGroupId)
                     .Where(p=>p.IsAdmin||p.Grouper)
                    .Select(p=>  p.UserId)
                    .ToListAsync();

                foreach (var user in userids.Distinct())
                {
                    notice = new Notice(@event.UserId
                        ,user
                        ,""
                        ,NoticeType.GroupRequest);

                    notices.Add(notice);
                }
            }
            else
            {

            }
        }
    }
}
