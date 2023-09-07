using Y.Chat.Host;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ×¢Èë·þÎñ
builder.Services.AddApplication<ChatHostModule>();

var app = builder.Build();

app.InitApplication();

app.Run();
