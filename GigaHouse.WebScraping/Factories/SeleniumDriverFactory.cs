//using GigaHouse.WebScraping.Interfaces;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium;
//using WebDriverManager.DriverConfigs.Impl;
//using WebDriverManager;

//namespace GigaHouse.WebScraping.Factories
//{
//    public class SeleniumDriverFactory : ISeleniumDriverFactory
//    {
//        public IWebDriver Create()
//        {
//            new DriverManager().SetUpDriver(new ChromeConfig());

//            var options = new ChromeOptions();
//            options.AddArgument("--headless");
//            options.AddArgument("--disable-gpu");
//            options.AddArgument("--no-sandbox");
//            options.AddArgument("--disable-dev-shm-usage");

//            return new ChromeDriver(options);
//        }
//    }
//}
