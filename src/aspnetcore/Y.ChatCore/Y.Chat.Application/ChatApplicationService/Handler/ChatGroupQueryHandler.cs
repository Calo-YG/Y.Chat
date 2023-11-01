using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Dtos;
using Y.Chat.Application.ChatApplicationService.Queries;
using Y.Chat.EntityCore;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatGroupQueryHandler
    {
        private readonly YChatContext _context;

        public ChatGroupQueryHandler(YChatContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 搜索群聊
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task QueryGroup(GroupQuery query)
        {
            var list = await _context.ChatGroups
                .Where(p => p.Name.Contains(query.Name) || p.GroupNumber.Contains(query.No))
                .ToListAsync();

            query.Result = list.Map<List<GroupDto>>();
        }
    }
}
