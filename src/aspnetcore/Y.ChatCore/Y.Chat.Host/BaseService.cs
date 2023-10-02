using Masa.BuildingBlocks.Dispatcher.Events;

namespace Y.Chat.Host
{
    public class BaseService<T> : ServiceBase where T : class
    {
        protected IEventBus _eventBus=>GetRequiredService<IEventBus>();   
        
        protected ILogger _logger => GetRequiredService<ILoggerFactory>().CreateLogger<T>();
    }
}
