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
            options.AddArgument("--headless=new"); // Melhor versão do headless
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-blink-features=AutomationControlled");

            // Define um user-agent para parecer um navegador real
            options.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/110.0.5481.177 Safari/537.36");

            var driver = new ChromeDriver(options);

            // Executa JavaScript para remover a detecção do Selenium
            var script = @"
        Object.defineProperty(navigator, 'webdriver', { get: () => undefined });
        window.chrome = { runtime: {} };
        Object.defineProperty(navigator, 'languages', { get: () => ['en-US', 'en'] });
        Object.defineProperty(navigator, 'plugins', { get: () => [1, 2, 3, 4, 5] });
    ";
            ((IJavaScriptExecutor)driver).ExecuteScript(script);

            return driver;
        }

    }
}
