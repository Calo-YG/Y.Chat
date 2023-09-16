using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using Y.Chat.EntityCore;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Y.Chat.Host
{
    public class ChatHostModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();

            // Add services to the container.
            context.Services.AddControllersWithViews();

            //配置数据上下文及生命周期
            context.Services.AddDbContext<YChatContext>(options =>
            {
                var connectionString = configuration.GetSection("App:ConnectionString:Default").Get<string>();

                options.UseSqlServer(connectionString);

                options.UseOpenIddict();

            },ServiceLifetime.Transient,ServiceLifetime.Singleton);

            context.Services.AddOpenIddict()
                            .AddCore(options =>
                            {
                               options.UseEntityFrameworkCore()
                                      .UseDbContext<YChatContext>()
                                      .ReplaceDefaultEntities<Guid>(); ;
                            });

            
            
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
