using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Y.Chat.Host
{
    public class ChatHostModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            // Add services to the container.
            context.Services.AddControllersWithViews();
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            var env = (IHostEnvironment)context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        }
    }
}
