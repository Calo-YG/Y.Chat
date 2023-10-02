using FluentValidation;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
            context.Services.AddMapster();
            context.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
            context.Services.AddEventBus(eventBusBuilder => eventBusBuilder.UseMiddleware(typeof(ValidatorEventMiddleware<>)));
        }
    }
}
