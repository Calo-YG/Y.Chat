using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Data.UoW.EFCore;
using Masa.Contrib.Dispatcher.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.Application.Hubs;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Events;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.EntityCore.Hubs;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class NoticeCommandHandler
    {
        private readonly YChatContext _context;
        private readonly INoticeRepository _noticeRepository;
        private readonly IEventBus _eventbus;
        private readonly IHubContext<ChatHub> _hubContext;
        
        public NoticeCommandHandler(YChatContext context,
            INoticeRepository noticeRepository,
            IEventBus eventBus,
            IHubContext<ChatHub> hubContext) 
        { 
            _context = context;
            _noticeRepository = noticeRepository;
            _eventbus = eventBus;
            _hubContext = hubContext;
        }

        [EventHandler]
        public async Task SendNotice(SendApplyCommand cmd)
        {
            var inviteuserId = cmd.ApplyUserId;

            List<Notice> notices = new List<Notice>();

            Notice notice;

            Func<string,string, Task> sendNotice = async (param,type) =>
            {
                var key = ChatConst.GetOnlineKey(param);

                var exists =await RedisHelper.ExistsAsync(ChatConst.GetOnlineKey(param));

                if (!exists)
                {
                    return;
                }

                var connection = await RedisHelper.GetAsync(key);

                await _hubContext.Clients.Groups("").SendAsync(ChatConst.Notice, type);
            };

            if (cmd.IsGroup)
            {
                var type = "Gropu";

                var group =await _context.ChatGroups.FirstOrDefaultAsync(p => p.Id == cmd.ApplyGroupId);

                if(group is null)
                {
                    throw new UserFriendlyException("好友不存在");
                }

                var userids=await _context.GroupUsers.Where(p=>p.GroupId== cmd.ApplyGroupId)
                     .Where(p=>p.IsAdmin||p.Grouper)
                     .Select(p=>  p.UserId)
                     .ToListAsync();

                foreach (var user in userids.Distinct())
                {
                    notice = new Notice(cmd.UserId
                        ,user
                        ,"入群申请"
                        ,NoticeType.GroupRequest
                        ,cmd.Remark);

                    notices.Add(notice);

                    await sendNotice(user.ToString(), type);
                }
            }
            else
            {
                var type = "Friend";

                var applyuser = await _context.Users.FirstOrDefaultAsync(p => p.Id == cmd.ApplyUserId);

                if(applyuser is null)
                {
                    throw new UserFriendlyException("好友不存在");
                }

                notice = new Notice(cmd.UserId
                    , (Guid)cmd.ApplyUserId
                    , "好友申请"
                    , NoticeType.FriendRequest
                    , cmd.Remark);

                notices.Add(notice);

                await sendNotice(applyuser.Id.ToString(), type);
            }
           await _noticeRepository.AddRangeAsync(notices);
           await _context.SaveChangesAsync();
        }

        [EventHandler]
        public async Task NoticeAggred(NoticeAgreeCommand cmd)
        {
            var notice = await _context.Notices.FirstOrDefaultAsync(p => p.Id == cmd.Id && p.RecivedUserId == cmd.ReceviedUserId);

            if (notice.NoticeType==NoticeType.FriendRequest)
            {
                var friendcmd = new CreateFriendCommand(notice.InviteUserId,
                    notice.RecivedUserId,
                    notice.Remark);

                await _eventbus.PublishAsync(friendcmd);

                notice.SetAggreed();

                notice.SetRead();

                await _noticeRepository.UpdateAsync(notice);
            }
            else
            {
                var joingroupcmd = new JoinGroupCommand((Guid)notice.GroupId,
                    notice.InviteUserId);

                await _eventbus.PublishAsync(joingroupcmd);

                await _noticeRepository.GroupRequestAggree(notice.InviteUserId, (Guid)notice.GroupId);
            }
        }

        [EventHandler]
        public async Task NoticeRead(NoticeReadCommand cmd)
        {
            var notice = await _noticeRepository.FindAsync(p => p.Id == cmd.Id && p.RecivedUserId == cmd.ReceviedId);

            notice.SetRead();

            await _noticeRepository.UpdateAsync(notice);
        }

        [EventHandler]
        public async Task Delete(DeleteNoticeCommand cmd)
        {
            await _noticeRepository.RemoveAsync(cmd.id);

            cmd.UnitOfWork.EntityState = Masa.BuildingBlocks.Data.UoW.EntityState.Changed;
        }
    }
}
