using GigaHouse.Application;
using GigaHouse.Data.Context;
using GigaHouse.Core.Common.HealthChecks;
using GigaHouse.Core.Common.Security;
using GigaHouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Serilog;
using GigaHouse.Core.Common.Validation;
using MediatR;
using GigaHouse.TaskList.Middleware;
using GigaHouse.Infrastructure.RabbitMQ;
using GigaHouse.Core.Common.Settings;
using GigaHouse.Infrastructure.HandlersLayer;
using GigaHouse.Infrastructure.Events.ProductMedia;
using Microsoft.Extensions.Configuration;
using Rebus.Handlers;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using GigaHouse.Infrastructure.Events;

public class Program
{
    private static void Main(string[] args)
    {
        var logger = NLog.LogManager.GetCurrentClassLogger();

        try
        {
            logger.Info("Init app");

            var builder = WebApplication.CreateBuilder(args);

            var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost:8085";
            Log.Information("ASPNETCORE_URLS: {Url}", urls);
            builder.WebHost.UseUrls(urls);

            builder.Configuration
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            builder.Host.UseNLog();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.AddBasicHealthChecks();

            builder.Services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type =>
                {
                    if (type.IsGenericType)
                    {
                        var genericType = type.GetGenericTypeDefinition().Name.Split('`')[0];
                        var genericArgs = string.Join("_", type.GetGenericArguments().Select(t => t.FullName?.Replace(".", "_")));
                        return $"{genericType}_{genericArgs}";
                    }
                    return type.FullName?.Replace(".", "_");
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT assim: Bearer {seu token}",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                        {
                            {
                                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                                {
                                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                    {
                                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                        });
            });


            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddAuthorization();
            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly, typeof(EventsLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            //builder.Services.AddMemoryCache();
            builder.Services.AddLogging();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
            builder.Services.AddRebusMessaging(builder.Configuration);

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                int retries = 5;
                while (retries > 0)
                {
                    try
                    {
                        Log.Information("Applying database migrations...");
                        dbContext.Database.Migrate();
                        Log.Information("Database migrations applied successfully.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Log.Warning(ex, "Database migration failed, retrying in 5s...");
                        Thread.Sleep(5000);
                        retries--;
                    }
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseBasicHealthChecks();
            app.MapControllers();

            app.Run();
        }
        catch (Exception error)
        {
            logger.Error(error, "Stopped program because of exception");
            throw;
        }
        finally
        {
            NLog.LogManager.Shutdown();
        }
    }
}