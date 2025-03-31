using GigaHouse.Infrastructure.Interfaces.Factories;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace GigaHouse.Infrastructure.Factories
{
    public class SeleniumDriverFactory : ISeleniumDriverFactory
    {
        public IWebDriver Create()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            return new ChromeDriver(options);
        }
    }
}
