using Calo.Blog.Common;
using Calo.Blog.Common.Minio;
using Calo.Blog.Common.Redis;
using Masa.BuildingBlocks.Caching;
using Microsoft.Extensions.DependencyInjection;
using Y.Chat.EntityCore.Domain.ChatDomain.Repositories;
using Y.Chat.EntityCore.Domain.FileDomain;
using Y.Chat.EntityCore.Domain.UserDomain;
using Y.Chat.EntityCore.Domain.UserDomain.Repositories;
using Y.EventBus;
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
            context.Services.AddSignalR()
                .AddMessagePackProtocol();
                //.AddStackExchangeRedis("124.71.15.19:6379,password=154511,defaultDatabase=1,ssl=false,writeBuffer=10240");

            context.Services.AddSequentialGuidGenerator();
            context.Services.AddEventBus();

            context.Services.AddTransient<IUserDomainService, UserDomainService>();
            context.Services.AddTransient<IFileDomainService, FileDomainService>();
            context.Services.AddTransient<IUserRepository,UserRepository>();
            context.Services.AddTransient<IGroupRepository, GroupRepository>();
            context.Services.AddTransient<IFriendRepository, FriendRepository>();
            context.Services.AddTransient<INoticeRepository, NoticeRepository>();
            context.Services.AddTransient<IMessageRepositroy, MessageRepository>();

            //context.Services.AddAutoInject(Assembly.GetExecutingAssembly());
        }
    }
}
