using GigaHouse.Core.Common.Settings;
using GigaHouse.Infrastructure.Events;
using GigaHouse.Infrastructure.Events.ProductMedia;
using GigaHouse.Infrastructure.Events.Task;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Routing.TypeBased;
using Rebus.Serialization.Json;
using Serilog;

namespace GigaHouse.Infrastructure.RabbitMQ
{
    public static class RebusServiceCollectionExtensions
    {
        public static void AddRebusMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddRebus(rebus => rebus
                    .Logging(l => l.Console())
                    .Transport(t => t.UseRabbitMq(settings.ConnectionString, settings.QueueWebApiName))
                    .Routing(r => { r.TypeBased().Map<EventsLayer>(settings.QueueWorkerApiName); })
                    .Serialization(s => s.UseNewtonsoftJson())
                    );


            var handlers = typeof(EventsLayer).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>))).ToList();

            Log.Information("🔍 Rebus Handlers Registrados: {Count}", handlers.Count);
            handlers.ForEach(h => Log.Information("📌 Handler: {HandlerName}", h.Name));

            services.AutoRegisterHandlersFromAssemblyNamespaceOf<EventsLayer>();
        }

        public static void AddRebusMessagingWorker(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddRebus(rebus => rebus
                    .Logging(l => l.Console())
                    .Transport(t => t.UseRabbitMq(settings.ConnectionString, settings.QueueWorkerApiName).InputQueueOptions(q => q.SetAutoDelete(false)))
                    .Routing(r => { r.TypeBased().Map<HandlersLayer.HandlersLayer>(settings.QueueWebScrapingName); })
                    .Serialization(s => s.UseNewtonsoftJson())
                    .Sagas(s => s.StoreInPostgres(configuration.GetConnectionString("DefaultConnection"), "sagas", "saga_indexes")),
                    onCreated: async bus => { await SubscribeToEvents(bus); }
                    );

            //services.AddRebusRouting(settings);

            var handlers = typeof(HandlersLayer.HandlersLayer).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>))).ToList();

            Log.Information("🔍 Rebus Handlers Registrados: {Count}", handlers.Count);
            handlers.ForEach(h => Log.Information("📌 Handler: {HandlerName}", h.Name));

            services.AutoRegisterHandlersFromAssemblyNamespaceOf<HandlersLayer.HandlersLayer>();
        }

        public static void AddRebusMessagingScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddRebus(rebus => rebus
                    .Logging(l => l.Console())
                    .Transport(t => t.UseRabbitMq(settings.ConnectionString, settings.QueueWebApiName))
                    .Routing(r => { r.TypeBased().Map<HandlersScraping.HandlersScraping>(settings.QueueWebScrapingName); })
                    .Serialization(s => s.UseNewtonsoftJson()));

            var handlers = typeof(HandlersScraping.HandlersScraping).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>))).ToList();

            Log.Information("🔍 Rebus Handlers Registrados: {Count}", handlers.Count);
            handlers.ForEach(h => Log.Information("📌 Handler: {HandlerName}", h.Name));

            services.AutoRegisterHandlersFromAssemblyNamespaceOf<HandlersScraping.HandlersScraping>();
        }

        public static void AddRebusMessagingScraping(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

            services.AddRebus(rebus => rebus
                    .Logging(l => l.Console())
                    .Transport(t => t.UseRabbitMq(settings.ConnectionString, settings.QueueWebScrapingName).InputQueueOptions(q => q.SetAutoDelete(false)))
                    .Serialization(s => s.UseNewtonsoftJson()),
                    onCreated: async bus => { await SubscribeScrapingToEvents(bus); }
                    );

            var handlers = typeof(HandlersScraping.HandlersScraping).Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandleMessages<>))).ToList();

            Log.Information("🔍 Rebus Handlers Registrados: {Count}", handlers.Count);
            handlers.ForEach(h => Log.Information("📌 Handler: {HandlerName}", h.Name));

            services.AutoRegisterHandlersFromAssemblyNamespaceOf<HandlersScraping.HandlersScraping>();
        }

        private static void AddRebusRouting(this IServiceCollection services, RabbitMqSettings settings)
        {
            services.AddRebus(rebus => rebus
                    .Routing(r =>
                    {
                        r.TypeBased().Map<ProductMediaCreatedEvent>(settings.QueueWebScrapingName);
                    })
            );
        }

        private static async Task SubscribeToEvents(IBus bus)
        {
            await bus.Subscribe<ProductMediaCreatedEvent>();
            await bus.Subscribe<TaskCreatedEvent>();
            await bus.Advanced.Topics.Unsubscribe(nameof(TaskScrapingEvent));
        }

        private static async Task SubscribeScrapingToEvents(IBus bus)
        {
            await bus.Subscribe<TaskScrapingEvent>();
        }

        private static async Task UnsubscribeEvents(IBus bus)
        {
            await bus.Advanced.Topics.Unsubscribe(nameof(ProductMediaCreatedEvent));
        }
    }
}
