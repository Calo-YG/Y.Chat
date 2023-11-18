var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.ChatAspire_ApiService>("apiservice");

builder.AddProject<Projects.ChatAspire_Web>("webfrontend")
    .WithReference(apiservice);

builder.AddProject<Projects.Y_Chat_Host>("y.chat.host");

builder.Build().Run();
