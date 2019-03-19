using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Robo.PortalRh.Dominio.Enum;
using System;
using System.Configuration;

namespace Robo.PortalRh.POM
{
    public class PrincipalPage : IDisposable
    {
        private string _ambiente;
        private string _goToUrl;
        private IWebDriver _driver;

        public PrincipalPage(IWebDriver driver)
        {
            _ambiente = ConfigurationManager.AppSettings.Get("ambiente");
            if (_ambiente.ToLower().Equals(AmbienteEnum.Producao.ToString().ToLower()))
            {
                _goToUrl = ConfigurationManager.AppSettings.Get("url_principal_prod");
            }
            else
            {
                _goToUrl = ConfigurationManager.AppSettings.Get("url_principal_local");
            }

            _driver = driver;
            PageFactory.InitElements(_driver, this);

            _driver.SwitchTo().Frame("mainFrame");

        }

        [FindsBy(How = How.CssSelector, Using = "#Table1 > tbody > tr:nth-child(1) > td:nth-child(2) > a")]
        [CacheLookup]
        public IWebElement MarcacaoPontoElement { get; set; }

        public bool MarcacaoPontoClicar()
        {
            if (MarcacaoPontoElement != null)
            {
                MarcacaoPontoElement.Click();
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
