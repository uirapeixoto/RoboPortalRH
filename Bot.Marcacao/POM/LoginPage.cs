using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Robo.PortalRh.Dominio.Enum;
using System;
using System.Configuration;

namespace Robo.PortalRh.POM
{
    public class LoginPage : IDisposable
    {
        private string _ambiente;
        private string _goToUrl;
        private IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _ambiente = ConfigurationManager.AppSettings.Get("ambiente");
            if (_ambiente.ToLower().Equals(AmbienteEnum.Producao.ToString().ToLower()))
            {
                _goToUrl = ConfigurationManager.AppSettings.Get("url_login_prod");
            }
            else
            {
                _goToUrl = ConfigurationManager.AppSettings.Get("url_login_local");
            }

            _driver = driver;
            PageFactory.InitElements(_driver, this);
            _driver.Navigate().GoToUrl(_goToUrl);
        }

        [FindsBy(How = How.Id, Using = "CtrlLogin1_txtIDNumerico")]
        [CacheLookup]
        public IWebElement UsuarioElement { get; set; }

        [FindsBy(How = How.Id, Using = "CtrlLogin1_txtSenhaAlfanumerico")]
        public IWebElement SenhaElement { get; set; }

        [FindsBy(How = How.Id, Using = "CtrlLogin1_btnIniciar")]
        [CacheLookup]
        public IWebElement BotaoConfirmaElement { get; set; }

        [FindsBy(How = How.Id, Using = "CtrlLogin1_lblMensagemAcesso")]
        [CacheLookup]
        public IWebElement MensagemAlertaElement { get; set; }

        public string UsuarioLogin
        {
            get
            {
                return UsuarioElement != null ? UsuarioElement.GetAttribute("value") : "";
            }
            set
            {
                if (UsuarioElement != null)
                {
                    UsuarioElement.Click();
                    UsuarioElement.Clear();
                    UsuarioElement.SendKeys(value);
                }
            }
        }

        public string Senha
        {
            get
            {
                return SenhaElement != null ? SenhaElement.GetAttribute("value") : "";
            }
            set
            {
                if (SenhaElement != null)
                {
                    SenhaElement.Click();
                    SenhaElement.Clear();
                    SenhaElement.SendKeys(value);
                }
            }
        }

        public string MensagemAlerta
        {
            get
            {
                try
                {
                    return MensagemAlertaElement != null ? MensagemAlertaElement.Text : "";

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool BusinessValidation()
        {
            if (string.IsNullOrEmpty(UsuarioLogin) || string.IsNullOrEmpty(Senha))
                return false;

            return true;
        }

        public bool ConfirmarLogin(IWebDriver driver)
        {
            bool retorno = false;
            /*WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(2));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("botaoLogin")));*/

            if (BusinessValidation())
            {
                BotaoConfirmaElement.Click();
                retorno = true;
            }

            return retorno;
        }

        public bool BotaoConfirma()
        {
            var result = true;
            if (BusinessValidation())
            {
                BotaoConfirmaElement.Click();
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void Dispose()
        {
        }
    }
}
