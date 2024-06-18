
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GigaHouse.TaskList/GigaHouse.TaskList.csproj", "GigaHouse.TaskList/"]
RUN dotnet restore "./GigaHouse.TaskList/GigaHouse.TaskList.csproj"
COPY . .
WORKDIR "/src/GigaHouse.TaskList"
RUN dotnet build "./GigaHouse.TaskList.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GigaHouse.TaskList.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "GigaHouse.TaskList.dll"]