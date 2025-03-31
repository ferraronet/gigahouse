//using GigaHouse.WebScraping.Interfaces;
//using OpenQA.Selenium;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GigaHouse.WebScraping.Services
//{
//    public class ScraperService : IScraperService
//    {
//        private readonly ISeleniumDriverFactory _driverFactory;

//        public ScraperService(ISeleniumDriverFactory driverFactory)
//        {
//            _driverFactory = driverFactory;
//        }

//        public async Task<string> ScrapeDataAsync(string url)
//        {
//            using var driver = _driverFactory.Create();
//            driver.Navigate().GoToUrl(url);

//            var data = driver.FindElement(By.CssSelector(".product-name")).Text;
//            return await Task.FromResult(data);
//        }
//    }
//}
