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
using AngleSharp.Dom;

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
            WaitForPageLoad(driver);

            var taskHistory = new TaskHistory();

            var hasErrorVendorUnits = ExtractElementValue<int>(_logger, projectCssSelector.VendorUnits, driver, value => taskHistory.VendorUnits = value);
            var hasErrorVendor = ExtractElementValue<string>(_logger, projectCssSelector.Vendor, driver, value => taskHistory.Vendor = value);
            var hasErrorShippingPrice = ExtractElementValue<decimal>(_logger, projectCssSelector.ShippingPrice, driver, value => taskHistory.ShippingPrice = value);
            var hasErrorAnnouncement = ExtractElementValue<string>(_logger, projectCssSelector.Announcement, driver, value => taskHistory.Announcement = value);
            var hasErrorBreadCrumb = ExtractElementValue<string>(_logger, projectCssSelector.BreadCrumb, driver, value => taskHistory.BreadCrumb = value, "href");
            var hasErrorCoupon = ExtractElementValue<string>(_logger, projectCssSelector.Coupon, driver, value => taskHistory.Coupon = value);
            var hasErrorInstallments = ExtractElementValue<int>(_logger, projectCssSelector.Installments, driver, value => taskHistory.Installments = value);
            var hasErrorInstallmentPrice = ExtractElementValue<decimal>(_logger, projectCssSelector.InstallmentPrice, driver, value => taskHistory.InstallmentPrice = value);
            var hasErrorProductPrice = ExtractElementValue<decimal>(_logger, projectCssSelector.ProductPrice, driver, value => taskHistory.ProductPrice = value);
            var hasErrorThermometer = ExtractElementValue<string>(_logger, projectCssSelector.Thermometer, driver, value => taskHistory.Thermometer = value);
            var hasErrorRating = ExtractElementValue<double>(_logger, projectCssSelector.Rating, driver, value => taskHistory.Rating = value);
            var hasErrorIsFull = ExtractElementValue<bool>(_logger, projectCssSelector.IsFull, driver, value => taskHistory.IsFull = value);
            var hasErrorFreeShipping = ExtractElementValue<bool>(_logger, projectCssSelector.FreeShipping, driver, value => taskHistory.IsFreeShipping = value);
            var hasErrorRatingCount = ExtractElementValue<string>(_logger, projectCssSelector.RatingCount, driver, value => taskHistory.RatingCount = value);

            if(hasErrorVendorUnits) 
                ExtractElementValue<int>(_logger, projectCssSelector.VendorUnits, driver, value => taskHistory.VendorUnits = value);

            if(hasErrorVendor)
                ExtractElementValue<string>(_logger, projectCssSelector.Vendor, driver, value => taskHistory.Vendor = value);

            if(hasErrorShippingPrice)
                ExtractElementValue<decimal>(_logger, projectCssSelector.ShippingPrice, driver, value => taskHistory.ShippingPrice = value);

            if(hasErrorAnnouncement)
                ExtractElementValue<string>(_logger, projectCssSelector.Announcement, driver, value => taskHistory.Announcement = value);

            if(hasErrorBreadCrumb)
                ExtractElementValue<string>(_logger, projectCssSelector.BreadCrumb, driver, value => taskHistory.BreadCrumb = value, "href");

            if(hasErrorCoupon)
                ExtractElementValue<string>(_logger, projectCssSelector.Coupon, driver, value => taskHistory.Coupon = value);

            if(hasErrorInstallments)
                ExtractElementValue<int>(_logger, projectCssSelector.Installments, driver, value => taskHistory.Installments = value);

            if(hasErrorInstallmentPrice)
                ExtractElementValue<decimal>(_logger, projectCssSelector.InstallmentPrice, driver, value => taskHistory.InstallmentPrice = value);

            if(hasErrorProductPrice)
                ExtractElementValue<decimal>(_logger, projectCssSelector.ProductPrice, driver, value => taskHistory.ProductPrice = value);

            if(hasErrorThermometer)
                ExtractElementValue<string>(_logger, projectCssSelector.Thermometer, driver, value => taskHistory.Thermometer = value);

            if(hasErrorRating)
                ExtractElementValue<double>(_logger, projectCssSelector.Rating, driver, value => taskHistory.Rating = value);

            if(hasErrorIsFull)
                ExtractElementValue<bool>(_logger, projectCssSelector.IsFull, driver, value => taskHistory.IsFull = value);

            if(hasErrorFreeShipping)
                ExtractElementValue<bool>(_logger, projectCssSelector.FreeShipping, driver, value => taskHistory.IsFreeShipping = value);

            if(hasErrorRatingCount)
                ExtractElementValue<string>(_logger, projectCssSelector.RatingCount, driver, value => taskHistory.RatingCount = value);

            taskHistory.CreatedAt = DateTime.Now;
            taskHistory.UpdatedAt = DateTime.Now;

            return taskHistory;
        }

        private void WaitForPageLoad(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").Equals("complete"));
        }

        private static bool ExtractElementValue<T>(ILogger<TaskScrapingEvent> logger, string selector, IWebDriver driver, Action<T> setValue, string? attribute = null)
        {
            bool hasError = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                string[] xpathArray = selector.Split(';');
                string text = "";

                foreach (var xpath in xpathArray)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(xpath.Trim()))
                        {
                            IWebElement element;

                            if (xpath.Trim().StartsWith("/"))
                                element = wait.Until(drv => drv.FindElement(By.XPath(xpath.Trim())));
                            else
                                element = wait.Until(drv => drv.FindElement(By.CssSelector(xpath.Trim())));

                            string? value = !string.IsNullOrEmpty(attribute) ? element.GetAttribute(attribute) : element.Text;

                            if (!string.IsNullOrEmpty(value))
                                text += value.Trim();
                        }
                    }
                    catch (Exception errorXpath)
                    {
                        hasError = true;
                        logger.LogInformation($"{errorXpath.Message}: {xpath}");
                    }
                }

                if (typeof(T) == typeof(string))
                {
                    setValue((T)(object)text);
                }
                else if (typeof(T) == typeof(int))
                {
                    var match = System.Text.RegularExpressions.Regex.Match(text, @"\d+");
                    if (match.Success)
                    {
                        var parsedValue = int.TryParse(match.Groups[0].Value, out var intValue) ? intValue : default;
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
                hasError = true;
                logger.LogInformation(error.Message);
            }

            return hasError;
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

                                if (taskHistory.Installments == null || taskHistory.InstallmentPrice == null || taskHistory.Vendor == null || taskHistory.VendorUnits == null)
                                {
                                    var lastRecord = await _taskHistoryService.GetLastHistoryTaskByTaskIdAsync(taskHistory.TaskId, (decimal)taskHistory.ProductPrice);

                                    if (lastRecord != null)
                                    {
                                        if (taskHistory.Installments == null)
                                            taskHistory.Installments = lastRecord.Installments;

                                        if (taskHistory.InstallmentPrice == null)
                                            taskHistory.InstallmentPrice = lastRecord.InstallmentPrice;

                                        if (taskHistory.Vendor == null)
                                            taskHistory.Vendor = lastRecord.Vendor;

                                        if (taskHistory.VendorUnits == null)
                                            taskHistory.VendorUnits = lastRecord.VendorUnits;
                                    }
                                }

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
