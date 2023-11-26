using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Repositories
{
    public class ChatListRepository : Repository<YChatContext, ChatList, Guid>, IChatListRepositroy
    {
        public ChatListRepository(YChatContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {

        }

        /// <summary>
        /// 通过chatid获取用户
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public IQueryable<User> GetUsersByChatId(Guid conversationId)
        {
            var query = from c in Context.ChatLists
                        where c.ConversationId == conversationId
                        join u in Context.Users on c.UserId equals u.Id
                        select u;

            return query;
        }
    }
}
