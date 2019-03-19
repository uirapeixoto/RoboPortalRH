using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Robo.PortalRh.Dominio.Enum;
using System;
using System.Configuration;

namespace Robo.PortalRh.POM
{
    public class IncluirMarcacaoPage : IDisposable
    {
        private string _ambiente;
        private string _goToUrl;
        private IWebDriver _driver;

        public IncluirMarcacaoPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);

        }

        [FindsBy(How = How.Id, Using = "Button1")]
        [CacheLookup]
        public IWebElement BotaoIncluirElement { get; set; }

        public bool BotaoIncluirClicar()
        {
            if (BotaoIncluirElement != null)
            {
                BotaoIncluirElement.Click();
            }
            else
            {
                return false;
            }
            return true;
        }

        public void Dispose()
        {
        }
    }
}
