using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Ddd.Domain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Shared;

namespace Y.Chat.EntityCore.Domain.ChatDomain.Entities
{
    public class ChatList:Entity<Guid>,ISoftDelete
    {
        public Guid UserId { get;private set; }

        public Guid ConversationId { get;private set; }

        public int UnReadCount { get;private set; }    

        public Guid? LastMessageId { get; private set; }

        public bool IsDeleted { get;  set; } = false;

        public string Avatart {  get; private set; }

        public ChatType ChatType { get; private set; }

        public Guid? FriendId {  get; private set; }

        public string Name { get; private set; }

        public DateTime CreationTime { get; private set; }=DateTime.Now;

        public ChatList() { }

        public ChatList(Guid userId, Guid conversationId,string name, string avatart,ChatType chatType)
        {
            UserId = userId;
            ConversationId = conversationId;
            Avatart = avatart;
            ChatType = chatType;
            Name = name;
        }

        public void SetFriend(Guid friendId)
        {
            FriendId = friendId;
            ChatType = ChatType.Default;
        }

        public void SetAvatar(string avatar)
        {
            Avatart = avatar;
        }
    }
}
