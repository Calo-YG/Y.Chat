#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Y.Chat.Host/Y.Chat.Host.csproj", "Y.Chat.Host/"]
COPY ["Y.Chat.Application/Y.Chat.Application.csproj", "Y.Chat.Application/"]
COPY ["Y.Chat.EntityCore/Y.Chat.EntityCore.csproj", "Y.Chat.EntityCore/"]
COPY ["Y.Chat.Domain/Y.Chat.Domain.csproj", "Y.Chat.Domain/"]
RUN dotnet restore "Y.Chat.Host/Y.Chat.Host.csproj"
COPY . .
WORKDIR "/src/Y.Chat.Host"
RUN dotnet build "Y.Chat.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Y.Chat.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Y.Chat.Host.dll"]