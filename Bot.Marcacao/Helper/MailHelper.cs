using Bot.Marcacao.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;

namespace Bot.Marcacao.Helper
{
    public class Email
    {
        private static string _smtpServerAdress;
        private static string _smtpServerPort;
        private static string _credencialEmail;
        private static string _credencialSenha;
        private static string _emailFromAlias;

        public Email()
        {
            _smtpServerAdress = ConfigurationManager.AppSettings.Get("smtp_server_address");
            _smtpServerPort = ConfigurationManager.AppSettings.Get("smtp_server_port");
            _credencialEmail = ConfigurationManager.AppSettings.Get("credential_email");
            _credencialSenha = ConfigurationManager.AppSettings.Get("credential_senha");
            _emailFromAlias = ConfigurationManager.AppSettings.Get("email_from_alias");
        }

        /// <summary>
        /// Método para enviar email
        /// </summary>
        /// <param name="emailDestinatario">Email do destinatário</param>
        /// <param name="assunto">Assunto do email</param>
        /// <param name="mensagem">mensagem do email</param>
        public static void EnviarEmail(IEnumerable<DestinatariosEmailModel> destinatarios, string Assunto, string Texto)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtpServer = new SmtpClient(_smtpServerAdress);
                smtpServer.UseDefaultCredentials = false;
                smtpServer.EnableSsl = false;
                smtpServer.Credentials = new System.Net.NetworkCredential(_credencialEmail, _credencialSenha);
                smtpServer.Port = 587;

                mail.IsBodyHtml = true;
                mail.From = new MailAddress(_emailFromAlias);
                mail.Subject = Assunto;
                mail.Body = Texto;

                // Add a carbon copy recipient.
                foreach (var destinatario in destinatarios.Where(t => t.Principal == false))
                {
                    mail.CC.Add(new MailAddress(destinatario.Destinatario));
                }

                foreach (var destinatario in destinatarios.Where(t => t.Oculto == true))
                {
                    mail.Bcc.Add(new MailAddress(destinatario.Destinatario));
                }

                foreach (var destinatario in destinatarios.Where(t => t.Principal == true))
                {
                    mail.To.Add(new MailAddress(destinatario.Destinatario));
                }

                smtpServer.Send(mail);
                Console.WriteLine("Email enviado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
