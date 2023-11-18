using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Hubs;
using Y.Chat.Host;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Host.UseSerilog(
    (context, services, configuration) =>
        configuration.ReadFrom
            .Configuration(context.Configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());

var configruartion = builder.Configuration;

builder.Services.AddMasaDbContext<YChatContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(configruartion.GetSection("ConnectionString:Default").Value);
    optionsBuilder.UseFilter();
   // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddDomainEventBus(options =>
{
    options.UseUoW<YChatContext>();
    options.UseRepository<YChatContext>();
});

// 注入服务
builder.Services.AddApplication<ChatHostModule>();

builder.Services.AddMasaMinimalAPIs(options =>
{
   
});

var app = builder.Build();  
  
app.MapDefaultEndpoints();

await app.InitApplicationAsync();

app.MapHub<ChatHub>("/chat");

app.MapMasaMinimalAPIs();

app.Run();
