#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

RUN ls

COPY ["Y.Chat.Host/Y.Chat.Host.csproj", "Y.Chat.Host/"]
#COPY ["Y.Chat.Application/Y.Chat.Application.csproj", "Y.Chat.Application/"]
#COPY ["Y.Chat.EntityCore/Y.Chat.EntityCore.csproj", "Y.Chat.EntityCore/"]

RUN dotnet restore "./Y.Chat.Host/Y.Chat.Host.csproj"
COPY . .
WORKDIR "/src/Y.Chat.Host"

RUN dotnet build "./Y.Chat.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Y.Chat.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Y.Chat.Host.dll"]