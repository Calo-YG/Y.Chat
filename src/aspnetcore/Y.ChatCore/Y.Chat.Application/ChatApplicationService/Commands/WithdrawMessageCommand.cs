using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Y.Chat.Application.ChatApplicationService.Commands
{
    public record class WithdrawMessageCommand(Guid MessageId,Guid UserId,Guid ChatId) : Command
    {
        /// <summary>
        /// 撤回的消息是否是最新的消息
        /// </summary>
        public bool IsLast { get; set; }
    }
}
