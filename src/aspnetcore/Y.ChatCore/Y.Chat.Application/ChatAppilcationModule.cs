using Y.Chat.EntityCore;
using Y.Module;
using Y.Module.Modules;

namespace Y.Chat.Application
{
    [DependOn(typeof(ChatEntityCoreModule))]
    public class ChatAppilcationModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            base.ConfigerService(context);
        }
    }
}
