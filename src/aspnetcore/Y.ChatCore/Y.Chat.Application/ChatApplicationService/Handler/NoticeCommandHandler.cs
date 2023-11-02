using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class NoticeCommandHandler
    {
        private readonly YChatContext _context;
        private readonly INoticeRepository _noticeRepository;
        
        public NoticeCommandHandler(YChatContext context,
            INoticeRepository noticeRepository) 
        { 
            _context = context;
            _noticeRepository = noticeRepository;
        }

        [EventHandler]
        public async Task SendNotice(SendApplyCommand cmd)
        {
            var inviteuserId = cmd.ApplyUserId;
            var applyuser = await _context.Users.FirstOrDefaultAsync(p => p.Id == cmd.UserId);
            var content = cmd.IsGroup ? $"{applyuser.Name}入群申请" : $"{applyuser.Name}好友申请";
            List<Notice> notices = new List<Notice>();
            Notice notice;
            if (cmd.IsGroup)
            {
                var userids=await _context.GroupUsers.Where(p=>p.GroupId== cmd.ApplyGroupId)
                     .Where(p=>p.IsAdmin||p.Grouper)
                     .Select(p=>  p.UserId)
                     .ToListAsync();

                foreach (var user in userids.Distinct())
                {
                    notice = new Notice(cmd.UserId
                        ,user
                        ,content
                        ,NoticeType.GroupRequest
                        ,cmd.Remark);

                    notices.Add(notice);
                }
            }
            else
            {
                notice = new Notice(cmd.UserId
                    , (Guid)cmd.ApplyUserId
                    , content
                    , NoticeType.FriendRequest
                    , cmd.Remark);
                notices.Add(notice);
            }
           await _noticeRepository.AddRangeAsync(notices);
        }
    }
}
