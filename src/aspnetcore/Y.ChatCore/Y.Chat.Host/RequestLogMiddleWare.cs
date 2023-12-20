using Masa.BuildingBlocks.Dispatcher.Events;
using Masuit.Tools;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Text;
using Y.Chat.Application.SystemApplcationService.Commands;

namespace Y.Chat.Host
{
    public class RequestLogMiddleWare
    {
        private IEventBus _eventBus;
        private readonly RequestLogOptions Options;
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;
        public RequestLogMiddleWare(RequestDelegate next,IServiceProvider serviceProvider, IOptions<RequestLogOptions> options)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            Options = options.Value;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var requestPath = context.Request.Path.Value ?? "";
            var notex = true;
            if (requestPath.StartsWith(Options.LogPath))
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                _eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    await _next(context);

                }
                catch (Exception ex)
                {
                    notex = false;
                    await HandRequestLog(context, stopwatch, ex);
                    throw;
                }
                finally
                {
                    if (notex)
                    {
                        await HandRequestLog(context, stopwatch, null);
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandRequestLog(HttpContext httpContext, Stopwatch stopwatch, Exception? exception)
        {
            stopwatch.Stop();
            var ip = httpContext.Request.Headers.ContainsKey("X-Forwarded-For") ? httpContext.Request.Headers["X-Forwarded-For"].ToString():null;
            var path = httpContext.Request.Path;
            var method = httpContext.Request.Method;

            Func<HttpContext, Task<string>?> func = async (context) =>
            {
                if (context.Request.QueryString.HasValue)
                {
                    return context.Request.QueryString.Value;
                }
                if (context.Request.Body is not null)
                {
                    using StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                    return await reader.ReadToEndAsync();
  
                }
                return null;
            };

            var result = httpContext.User?.Claims?.FirstOrDefault(p => p.Type == "Id")?.Value;

            Guid userId;

            var parse = Guid.TryParse(result,out userId);

            var param =await func.Invoke(httpContext);

            var cmd = new InsertRequestLogCommand(method,
                path,
                ip,
                param  ,
                stopwatch.ElapsedMilliseconds.ToString()
                ,exception?.Message
                ,userId);

            await _eventBus.PublishAsync(cmd);
        }
    }
}
