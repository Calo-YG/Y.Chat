﻿using Y.Chat.Application.Hubs;

namespace Y.Chat.EntityCore.Hubs
{
    public class SendMessageModel
    {
        public Guid? RecivedUserId { get; set; }

        public string Message { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? ReplyMessagedId { get; set; }

        public ChatType ChatType { get; set; }
    }
}
