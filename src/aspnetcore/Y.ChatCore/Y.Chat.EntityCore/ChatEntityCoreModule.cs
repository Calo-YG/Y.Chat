using Calo.Blog.Common;
using Calo.Blog.Common.Minio;
using Calo.Blog.Common.Redis;
using Masa.BuildingBlocks.Caching;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

namespace Y.Chat.EntityCore
{
    [DependOn(typeof(CommonModule), typeof(MinioModule), typeof(RedisModule))]
    public class ChatEntityCoreModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddMasaDbContext<YChatContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer();
                optionsBuilder.UseFilter();
            });

            context.Services.AddDomainEventBus(options =>
            {
                options.UseRepository<YChatContext>();
            });

            context.Services.AddMultilevelCache(
                cacheBuilder => cacheBuilder.UseStackExchangeRedisCache()
            );

            context.Services.AddSequentialGuidGenerator();
        }
    }
}
