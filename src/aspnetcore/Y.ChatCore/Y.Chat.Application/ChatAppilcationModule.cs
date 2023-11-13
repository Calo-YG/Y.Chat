using FluentValidation;
using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Chat.Application.UserApplicationService.Handler;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.UserDomain.Events;
using Y.EventBus;
using Y.Module;
using Y.Module.Modules;

namespace Y.Chat.Application
{
    [DependOn(typeof(ChatEntityCoreModule))]
    public class ChatAppilcationModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddMapster();
            context.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
            context.Services.AddEventBus(eventBusBuilder =>
            {
                eventBusBuilder.UseMiddleware(typeof(ValidatorEventMiddleware<>));
                eventBusBuilder.UseUoW<YChatContext>(options => options.UseSqlServer());
            });
            context.Services.AddEventBus();
            context.Services.Subscribes(p =>
            {
                p.Subscribe<SendEmailEvent,SendEmailHandler>();
            });

        }

        public override async Task LaterInitApplicationAsync(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateScope();

            var eventhandlerManager = scope.ServiceProvider.GetRequiredService<IEventHandlerManager>();

            await eventhandlerManager.CreateChannles();
        }
    }
}
