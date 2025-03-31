namespace GigaHouse.Core.Common.Settings
{
    public class RabbitMqSettings
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string QueueWebApiName { get; set; } = string.Empty;

        public string QueueWorkerApiName { get; set; } = string.Empty;

        public string QueueWebScrapingName { get; set; } = string.Empty;
    }
}
