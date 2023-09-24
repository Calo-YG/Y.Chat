using Serilog;
using Serilog.Events;
using Y.Chat.Host;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) =>
        configuration.ReadFrom
            .Configuration(context.Configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console());

// ×¢Èë·þÎñ
builder.Services.AddApplication<ChatHostModule>();

var app = builder.Build();

await app.InitApplicationAsync();

app.Run();
