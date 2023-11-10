using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.Extensions;
using Calo.Blog.Common.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using Y.Chat.Application;
using Y.Chat.Application.ChatApplicationService;
using Y.Chat.Application.FileApplicationService;
using Y.Chat.Application.UserApplicationService;
using Y.Chat.EntityCore;
using Y.Chat.EntityCore.Domain.ChatDomain.Entities;
using Y.Chat.EntityCore.Domain.UserDomain.Entities;
using Y.Chat.Host.Services;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Y.Chat.Host
{
    [DependOn(typeof(ChatAppilcationModule))]
    public class ChatHostModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();

            context.Services.AddEndpointsApiExplorer();

            context.Services
                .AddMvc()
                .AddRazorPagesOptions(options => { })
                .AddRazorRuntimeCompilation();

            context.Services.AddHttpContextAccessor();

            var jwtsetting = configuration.GetSection("App:JWtSetting").Get<JwtSetting>();
            //添加cokkie认证和cokkie
            context.Services
                .AddAuthentication(context =>
                {
                    //需要登录进行鉴权认证
                    context.RequireAuthenticatedSignIn = true;
                    context.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    context.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, //是否验证Issuer
                        ValidIssuer = jwtsetting.Issuer, //发行人Issuer
                        ValidateAudience = true, //是否验证Audience
                        ValidAudience = jwtsetting.Audience, //
                        ValidateIssuerSigningKey = true, //是否验证SecurityKey
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtsetting.SecretKey)
                        ), //SecurityKey
                        ValidateLifetime = true, //是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
                        RequireExpirationTime = true,
                        SaveSigninToken = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var chatToken = context.Request.Query["access_token"];
                            //signlir提供token
                            if (!string.IsNullOrEmpty(chatToken) && context.Request.Path.StartsWithSegments("/chat"))
                            {
                                context.Token = chatToken;
                            }
                            return Task.CompletedTask;
                        },
                    };
                });

            context.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "Y.Chat API v1",
                        Title = "Y.Chat API",
                        Description = "Web API for managing By Calo-YG",
                        TermsOfService = new Uri("https://gitee.com/wen-yaoguang"),
                        Contact = new OpenApiContact
                        {
                            Name = "Gitee 地址",
                            Url = new Uri("https://gitee.com/wen-yaoguang/Colo.Blog")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "个人博客",
                            Url = new Uri("https://www.se.cnblogs.com/lonely-wen/")
                        }
                    }
                );
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.OrderActionsBy(o => o.RelativePath);
            });

            context.Services.AddCors(
                options =>
                    options.AddPolicy(
                        "YChat",
                        builder =>
                            builder
                                .WithOrigins(
                                    // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                                    configuration["App:CorsOriginscors"]
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(o => o.RemoteFix("/"))
                                        .ToArray()
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                    )
            );

            context.Services.AddAutoInject(Assembly.GetExecutingAssembly());

            context.Services.AddScoped<IGroupApplicationService, GroupService>();
            context.Services.AddScoped<IUserApplicationService, UserService>();
            context.Services.AddScoped<INoticeApplicationService, NoticeService>();
            context.Services.AddScoped<IFileApplicationService, FileService>();
            context.Services.AddScoped<IChatMessageApplicationService, ChatMessageService>();
        }

        public override void InitApplication(InitApplicationContext context)
        {
            using var scope = context.ServiceProvider.CreateAsyncScope();
            var provider = scope.ServiceProvider;
            var dbcontext = provider.GetRequiredService<YChatContext>();
            var any = dbcontext.ChatGroups.Any(p => p.Name == "世界频道");
            if (!any)
            {
                var user = dbcontext.Users.FirstOrDefault(p => p.Name == "wyg");

                if (user is null)
                {
                    user = new User("wyg","wyg154511","3164522206");
                    dbcontext.Users.Add(user);
                }

                var defaultGroup = new ChatGroup("世界频道",
                    user.Id,
                    "世界频道欢迎来访");
                defaultGroup.SetGroupNumber();

                var chatgoupuser = new GroupUser(defaultGroup.Id,user.Id);
                chatgoupuser.Owner();

                dbcontext.ChatGroups.Add(defaultGroup);
                dbcontext.GroupUsers.Add(chatgoupuser);
                dbcontext.SaveChanges();
            }

            var app = context.GetApplicationBuilder();

            var env = (IHostEnvironment)
                context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            app.UseCors("YChat");

            app.UseSerilogRequestLogging();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endOptions =>
            {
                endOptions.MapDefaultControllerRoute();
                endOptions.MapRazorPages();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Y.Chat API V1");
                    options.EnableDeepLinking();
                    options.DocExpansion(DocExpansion.None);
                    options.IndexStream = () =>
                    {
                        var path = Path.Join(
                            env.ContentRootPath,
                            "wwwroot",
                            "pages",
                            "swagger.html"
                        );
                        return new FileInfo(path).OpenRead();
                    };
                });
            }
        }
    }
}
