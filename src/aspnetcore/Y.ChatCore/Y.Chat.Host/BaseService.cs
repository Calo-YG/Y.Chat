﻿using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.Host
{
    public abstract class BaseService<T> : ServiceBase where T : class
    {
        protected IEventBus _eventBus=>GetRequiredService<IEventBus>();   
        
        protected ILogger _logger => GetRequiredService<ILoggerFactory>().CreateLogger<T>();

        protected IHttpContextAccessor HttpContextAccessor => GetRequiredService<IHttpContextAccessor>() ?? throw new ArgumentNullException($"{nameof(HttpContextAccessor)} 未注入");
    }
}
