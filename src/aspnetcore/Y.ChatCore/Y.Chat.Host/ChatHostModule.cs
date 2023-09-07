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

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endOptions =>
            {
                endOptions.MapDefaultControllerRoute();
                endOptions.MapRazorPages();
            });
        }
    }
}
