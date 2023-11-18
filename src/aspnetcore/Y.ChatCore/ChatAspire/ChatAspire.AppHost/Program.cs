var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Y_Chat_Host>("y.chat.host");

builder.Build().Run();
