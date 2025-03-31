using OpenQA.Selenium;

namespace GigaHouse.Infrastructure.Interfaces.Factories
{
    public interface ISeleniumDriverFactory
    {
        IWebDriver Create();
    }
}
