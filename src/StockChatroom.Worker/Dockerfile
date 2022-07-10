#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/StockChatroom.Worker/StockChatroom.Worker.csproj", "src/StockChatroom.Worker/"]
COPY ["src/StockChatroom.Infrastructure.Broker.RabbitMq/StockChatroom.Infrastructure.Broker.RabbitMq.csproj", "src/StockChatroom.Infrastructure.Broker.RabbitMq/"]
COPY ["src/StockChatroom.Application/StockChatroom.Application.csproj", "src/StockChatroom.Application/"]
COPY ["src/StockChatroom.Infrastructure.Data/StockChatroom.Infrastructure.Data.csproj", "src/StockChatroom.Infrastructure.Data/"]
COPY ["src/StockChatroom.Domain/StockChatroom.Domain.csproj", "src/StockChatroom.Domain/"]
COPY ["src/Shared/StockChatroom.Shared.csproj", "src/Shared/"]
RUN dotnet restore "src/StockChatroom.Worker/StockChatroom.Worker.csproj"
COPY . .
WORKDIR "/src/src/StockChatroom.Worker"
RUN dotnet build "StockChatroom.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockChatroom.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockChatroom.Worker.dll"]