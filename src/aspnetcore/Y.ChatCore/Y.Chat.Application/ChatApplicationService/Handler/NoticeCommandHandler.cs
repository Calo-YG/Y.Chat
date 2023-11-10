using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
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
        private readonly IEventBus _eventbus;
        
        public NoticeCommandHandler(YChatContext context,
            INoticeRepository noticeRepository,
            IEventBus eventBus) 
        { 
            _context = context;
            _noticeRepository = noticeRepository;
            _eventbus = eventBus;
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
           await _context.SaveChangesAsync();
        }

        [EventHandler]
        public async Task NoticeAggred(NoticeAgreeCommand cmd)
        {
            var notice = await _noticeRepository.FindAsync(p => p.Id == cmd.Id && p.RecivedUserId == cmd.ReceviedUserId);

            notice.SetAggreed();

            notice.SetRead();

            await _noticeRepository.UpdateAsync(notice);

            if(notice.NoticeType==NoticeType.FriendRequest)
            {
                var friendcmd = new CreateFriendCommand(notice.InviteUserId,
                    notice.RecivedUserId,
                    notice.Remark);
                await _eventbus.PublishAsync(friendcmd);
            }
            else
            {
                var joingroupcmd = new JoinGroupCommand((Guid)notice.GroupId,
                    notice.InviteUserId);
                await _eventbus.PublishAsync(joingroupcmd);
            }

            await _context.SaveChangesAsync();
        }

        [EventHandler]
        public async Task NoticeRead(NoticeReadCommand cmd)
        {
            var notice = await _noticeRepository.FindAsync(p => p.Id == cmd.Id && p.RecivedUserId == cmd.ReceviedId);

            notice.SetAggreed();

            await _noticeRepository.UpdateAsync(notice);

            await _context.SaveChangesAsync();
        }
    }
}
