using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class NoticeQueryHandler
    {
        private readonly YChatContext _context;
        private readonly INoticeRepository _noticeRepository;
        public NoticeQueryHandler(YChatContext context,
            INoticeRepository noticeRepository) 
        { 
            _context= context;
            _noticeRepository= noticeRepository;
        }
        /// <summary>
        /// 用户通知
        /// </summary>
        /// <returns></returns>
        [EventHandler]
        public async Task UserNotice(UserNoticeQuery query)
        {
            var list = await _noticeRepository.UserNotice(query.userId);

            query.Result = list.Map<List<NoticeDto>>();
        }
    }
}
