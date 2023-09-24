using Calo.Blog.Common;
using Calo.Blog.Common.Minio;
using Calo.Blog.Common.Redis;
using Y.Module;
using Y.Module.Modules;

namespace Y.Chat.EntityCore
{
    [DependOn(typeof(CommonModule)
        ,typeof(MinioModule)
        ,typeof(RedisModule))]
    public class ChatEntityCoreModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {

            base.ConfigerService(context);
        }
    }
}
