FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN apt update && apt install -y iproute2 curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GigaHouse.Worker.Scheduler/GigaHouse.Worker.Scheduler.csproj", "GigaHouse.Worker.Scheduler/"]
RUN dotnet restore "./GigaHouse.Worker.Scheduler/GigaHouse.Worker.Scheduler.csproj"
COPY . .
WORKDIR "/src/GigaHouse.Worker.Scheduler"
RUN dotnet build "./GigaHouse.Worker.Scheduler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GigaHouse.Worker.Scheduler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GigaHouse.Worker.Scheduler.dll"]