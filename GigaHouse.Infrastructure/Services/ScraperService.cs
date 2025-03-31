using AutoMapper;
using GigaHouse.Data.Domain;
using GigaHouse.Infrastructure.Events.Task;
using GigaHouse.Infrastructure.Interfaces.Factories;
using GigaHouse.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using GigaHouse.Infrastructure.HandlersScraping.Tasks;
using OpenQA.Selenium.Support.UI;
using GigaHouse.Infrastructure.Models.Caches;
using GigaHouse.Infrastructure.MongoDB;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace GigaHouse.Infrastructure.Services
{
    public class ScraperService : IScraperService
    {
        private readonly ISeleniumDriverFactory _driverFactory;
        private readonly ILogger<TaskScrapingEvent> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly IProjectCssSelectorService _projectCssSelectorService;
        private readonly ITaskHistoryService _taskHistoryService;
        private readonly IRequestLogService _requestLogService;

        public ScraperService(ISeleniumDriverFactory driverFactory, ILogger<TaskScrapingEvent> logger, IMapper mapper, ITaskService taskService, IProjectCssSelectorService projectCssSelectorService, ITaskHistoryService taskHistoryService, IRequestLogService requestLogService)
        {
            _driverFactory = driverFactory;
            _logger = logger;
            _mapper = mapper;
            _taskService = taskService;
            _projectCssSelectorService = projectCssSelectorService;
            _taskHistoryService = taskHistoryService;
            _requestLogService = requestLogService;
        }

        public async Task<TaskHistory> ScrapeDataAsync(string url, ProjectCssSelector projectCssSelector)
        {
            using var driver = _driverFactory.Create();
            driver.Navigate().GoToUrl(url);

            var taskHistory = new TaskHistory();

            ExtractElementValue<int>(projectCssSelector.VendorUnits, driver, value => taskHistory.VendorUnits = value);
            ExtractElementValue<string>(projectCssSelector.Vendor, driver, value => taskHistory.Vendor = value);
            ExtractElementValue<decimal>(projectCssSelector.ShippingPrice, driver, value => taskHistory.ShippingPrice = value);
            ExtractElementValue<string>(projectCssSelector.Announcement, driver, value => taskHistory.Announcement = value);
            ExtractElementValue<string>(projectCssSelector.BreadCrumb, driver, value => taskHistory.BreadCrumb = value);
            ExtractElementValue<string>(projectCssSelector.Coupon, driver, value => taskHistory.Coupon = value);
            ExtractElementValue<int>(projectCssSelector.Installments, driver, value => taskHistory.Installments = value);
            ExtractElementValue<decimal>(projectCssSelector.InstallmentPrice, driver, value => taskHistory.InstallmentPrice = value);
            ExtractElementValue<decimal>(projectCssSelector.ProductPrice, driver, value => taskHistory.ProductPrice = value);
            ExtractElementValue<string>(projectCssSelector.Thermometer, driver, value => taskHistory.Thermometer = value);
            ExtractElementValue<double>(projectCssSelector.Rating, driver, value => taskHistory.Rating = value);
            ExtractElementValue<bool>(projectCssSelector.IsFull, driver, value => taskHistory.IsFull = value);
            ExtractElementValue<bool>(projectCssSelector.FreeShipping, driver, value => taskHistory.IsFreeShipping = value);
            ExtractElementValue<string>(projectCssSelector.RatingCount, driver, value => taskHistory.RatingCount = value);

            taskHistory.CreatedAt = DateTime.Now;
            taskHistory.UpdatedAt = DateTime.Now;

            return taskHistory;
        }

        private static void ExtractElementValue<T>(string selector, IWebDriver driver, Action<T> setValue)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                string[] xpathArray = selector.Split(';');
                string text = "";

                foreach (var xpath in xpathArray)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(xpath.Trim()))
                            text += wait.Until(drv => drv.FindElement(By.XPath(xpath.Trim()))).Text.Trim();
                    }
                    catch (Exception)
                    {

                    }
                }

                if (typeof(T) == typeof(string))
                {
                    setValue((T)(object)text);
                }
                else if (typeof(T) == typeof(int))
                {
                    var match = System.Text.RegularExpressions.Regex.Match(text, @"(\d+)x");
                    if (match.Success)
                    {
                        var parsedValue = int.TryParse(match.Groups[1].Value, out var intValue) ? intValue : default;
                        setValue((T)(object)parsedValue);
                    }
                }
                else
                {
                    var convertedValue = GetElementValue<T>(text);

                    if (convertedValue != null)
                        setValue(convertedValue);
                }
            }
            catch (Exception error)
            {

            }
        }

        public static T? GetElementValue<T>(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return default;

            string cleanedText = Regex.Replace(text, @"[^\d.,-]", "");

            if (cleanedText.Contains(",") && cleanedText.Contains("."))
                cleanedText = cleanedText.Replace(".", "");

            cleanedText = cleanedText.Replace(",", ".");

            try
            {
                return (T)Convert.ChangeType(cleanedText, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default;
            }
        }

        public async System.Threading.Tasks.Task ScrapeTaskScrapingEventAsync(TaskScrapingEvent message)
        {
            try
            {
                _logger.LogInformation($" [x] TaskScrapingEvent received: {message.Id}");

                if (message?.Task == null)
                {
                    _logger.LogWarning("TaskScrapingEvent received with null object.");
                    return;
                }

                try
                {
                    var item = JsonConvert.DeserializeObject<MessageTask>(message.Task.ToString());

                    if (item == null)
                    {
                        _logger.LogError("Failed to deserialize message from event message: Deserialization returned null.");
                        return;
                    }
                    else
                    {
                        var task = await _taskService.GetByIdAsync(message.Id);

                        if (task != null && task.Status == Core.Enums.TaskStatus.Active)
                        {
                            var cssSelector = await _projectCssSelectorService.GetByProjectIdAsync(task.ProjectId);

                            if (cssSelector != null)
                            {
                                var taskHistory = await ScrapeDataAsync(item.Link, cssSelector);
                                taskHistory.TaskId = message.Id;
                                taskHistory.ProjectId = task.ProjectId;

                                await _taskHistoryService.CreateAsync(taskHistory);

                                task.UpdatedAt = DateTime.Now;
                                task.LastDateSearch = DateTime.Now;
                                await _taskService.UpdateAsync(task);

                                var requestLog = await _requestLogService.GetRequestByIdAsync(task.ProductId.ToString());

                                var currentLog = new UserProductPriceLog()
                                {
                                    Id = taskHistory.Id.ToString(),
                                    Link = task.Link,
                                    InstallmentPrice = taskHistory.InstallmentPrice,
                                    Installments = taskHistory.Installments,
                                    LastDateSearch = task.LastDateSearch,
                                    ProductPrice = taskHistory.ProductPrice
                                };

                                if (requestLog == null)
                                {
                                    var document = new UserProductLog();

                                    document.Id = task.ProductId.ToString();
                                    document.Gtin = task.Product.Gtin;
                                    document.Name = task.Product.Name;
                                    document.ProductPrices = new List<UserProductPriceLog> { currentLog };

                                    await _requestLogService.SaveRequestAsync(task.ProductId.ToString(), document.ToBsonDocument());
                                }
                                else
                                {
                                    var log = BsonSerializer.Deserialize<UserProductLog>(requestLog.RequestBody);

                                    if (log == null)
                                        return;

                                    log.ProductPrices.Add(currentLog);

                                    await _requestLogService.UpdateRequestAsync(task.ProductId.ToString(), log.ToBsonDocument());
                                }

                                _logger.LogInformation($" [✓] Processed TaskHistory Creation: {message.Id}");
                            }
                        }
                    }
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, "Failed to deserialize message from event message: Invalid JSON format.");
                    return;
                }
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error processing TaskScrapingEvent: {Id}", message.Id);
                throw;
            }
        }
    }
}
