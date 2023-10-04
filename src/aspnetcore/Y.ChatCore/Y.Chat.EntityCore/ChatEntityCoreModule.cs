using Calo.Blog.Common;
using Calo.Blog.Common.Minio;
using Calo.Blog.Common.Redis;
using Masa.BuildingBlocks.Caching;
using Microsoft.Extensions.DependencyInjection;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Module;
using Y.Module.Modules;

namespace Y.Chat.EntityCore
{
    [DependOn(typeof(CommonModule)
        , typeof(MinioModule)
        , typeof(RedisModule))]
    public class ChatEntityCoreModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddMultilevelCache(
                cacheBuilder => cacheBuilder.UseStackExchangeRedisCache()
            );
            context.Services.AddSignalR();

            context.Services.AddSequentialGuidGenerator();

            context.Services.AddTransient<IUserDomainService, UserDomainService>();

            //context.Services.AddAutoInject(Assembly.GetExecutingAssembly());
        }
    }
}
