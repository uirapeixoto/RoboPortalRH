using Bot.Marcacao.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Timers;

namespace Bot.Marcacao.Helper
{
    public class ScheduleHelper
    {
        static Timer timer;

        public static int _hora;
        public static int _minuto;
        public static int _segundos;
        public static string _sistema;
        public static Intervalo _intervalo;

        public static bool _executarAgora;
        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="hora"></param>
        /// <param name="minuto"></param>
        /// <param name="segundos"></param>
        public ScheduleHelper()
        {
            _intervalo = new Intervalo();
        }

        /// <summary>
        /// Executa o planejador
        /// </summary>
        public void Run()
        {
            //Parallel.Invoke(
            //new Action(schedule_Timer_PortalRh),
            //new Action(schedule_Timer_DesbloquearUsuario)
            //);
            Intervalo _intervalos = new Intervalo();
            DateTime now = DateTime.Now;
            if (!(now.DayOfWeek == DayOfWeek.Sunday || now.DayOfWeek == DayOfWeek.Saturday))
                schedule_Timer_PortalRh();
        }
        /// <summary>
        /// 
        /// </summary>
        static void schedule_Timer_PortalRh()
        {
            Console.WriteLine("..:: Timer Schedule Started ::..");
            DateTime nowTime = DateTime.Now;
            var intervalo = _intervalo.ListTime().Where(x => x.Data < nowTime).LastOrDefault();
            Console.WriteLine($"..:: Previsão de execução as { intervalo.Hora}:{intervalo.Minutos}:{intervalo.Segundos} ::..");
            DateTime scheduledTime = intervalo.Data;
            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
                if (intervalo.Descricao == Enum.IntervaloEnum.FimJornada)
                    _intervalo = new Intervalo();
            }

            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            timer = new Timer(tickTime);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed_PortalRh);
            timer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void timer_Elapsed_PortalRh(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("..:: Timer Schedule Stopped ::..");
            timer.Stop();
            LibraryHelper.WriterLog(string.Format("atualização de senha do sistema sicaq iniciado em {0}", nowTime));
            //executar ação


            schedule_Timer_PortalRh();
        }

        protected static IEnumerable<UsuarioPortalRhModel> UsuariosLogin()
        {
            try
            {
                var usuario = new List<UsuarioPortalRhModel>
                {
                    new UsuarioPortalRhModel{
                        Usuario = ConfigurationManager.AppSettings.Get("Usuario"),
                        Senha = ConfigurationManager.AppSettings.Get("Senha")
                    }
                };
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
