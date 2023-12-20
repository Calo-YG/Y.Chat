using Masa.Contrib.Dispatcher.Events;
using Y.Chat.Application.SystemApplcationService.Commands;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.SystemDomain;

namespace Y.Chat.Application.SystemApplcationService.Handler
{
    public class RequestLogCommandHandler
    {
        private readonly YChatContext Context;
        public RequestLogCommandHandler(YChatContext context) 
        {
            Context = context;
        }
        [EventHandler]
        public async Task InserRequestLog(InsertRequestLogCommand cmd)
        {
            var log = new RequestLogInfo(cmd.Method,
                cmd.Ip,
                cmd.Param,
                cmd.Duration,
                cmd.Path,
                cmd.RequestUserId);

            if(cmd.Exception is not null)
            {
                log.SetException(cmd.Exception);
            }

            await Context.AddAsync(log);

            await Context.SaveChangesAsync();
        }
    }
}
