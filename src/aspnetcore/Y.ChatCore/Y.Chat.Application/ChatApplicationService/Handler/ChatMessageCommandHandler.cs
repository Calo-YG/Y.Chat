using Masa.Contrib.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Y.Chat.Application.ChatApplicationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using EntityState = Masa.BuildingBlocks.Data.UoW.EntityState;

namespace Y.Chat.Application.ChatApplicationService.Handler
{
    public class ChatMessageCommandHandler
    {
        private readonly YChatContext Context;
        private readonly IMessageRepositroy _messageRepository;
        private readonly IChatDomainService _chatDomainService;
        public ChatMessageCommandHandler(YChatContext context,
            IMessageRepositroy messageRepositroy,
            IChatDomainService chatDomainService)
        { 
             Context = context;
             _messageRepository = messageRepositroy;
             _chatDomainService = chatDomainService;
        }

        [EventHandler]
        public async Task CreateMessage(CreateMessageCommand cmd)
        {
            var message = new Message(cmd.UserId,
                cmd.GroupId,
                cmd.Content,
                cmd.Type);

            await Context.AddAsync(message);

            await _chatDomainService.UpdateChatListMessage(cmd.GroupId, message.Id);

            cmd.UnitOfWork.EntityState = EntityState.Changed;

            cmd.MessageId = message.Id;
        }

        [EventHandler]
        public  async Task DeleteFriendAndGroupMessageHandler(DeleteFriendAllMessageEvent evnet)
        {
            var messages = await Context.ChatMessages.Where(p => p.GroupId == evnet.GroupId).ToListAsync();

            await _messageRepository.RemoveRangeAsync(messages);

            await Context.SaveChangesAsync();
        }

        [EventHandler(Order =1)]
        public async Task WithDrawMessageId(WithdrawMessageCommand cmd)
        {
            var message =await Context.ChatMessages.FirstOrDefaultAsync(p => p.Id == cmd.MessageId);

            var lastMessageId = await _messageRepository.GroupLastMessgeId(cmd.ChatId);

            if(message == null)
            {
                throw new UserFriendlyException("消息不存在！！");
            }

            if (message.UserId != cmd.UserId)
            {
                throw new UserFriendlyException("只能撤回自己发送的消息");
            }

            message.WithdrawMessage();
            Context.Update(message);

            cmd.UnitOfWork.EntityState = EntityState.Changed;

            cmd.IsLast = cmd.MessageId == lastMessageId;
        }

        [EventHandler(Order = 2)]
        public async Task UpdateWithDrawMessage(WithdrawMessageCommand cmd)
        {
            if (!cmd.IsLast)
            {
                return;
            }

            var lastMessageId = await _messageRepository.GroupLastMessgeId(cmd.ChatId);

            await _chatDomainService.UpdateChatListMessage(cmd.ChatId, lastMessageId);
        }
    }
}
