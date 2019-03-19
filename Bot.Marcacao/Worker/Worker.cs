using Bot.Marcacao.Helper;
using Bot.Marcacao.Model;
using System;
using System.Threading.Tasks;

namespace Bot.Marcacao.Worker
{
    public class Worker
    {
        public delegate void ScheduleDelegate();

        private int _hora;
        private int _minutos;
        private int _segundos;
        private Random _rnd;

        public Worker(int hora, int minutos, int segundos)
        {
            _hora = hora;
            _minutos = minutos;
            _segundos = segundos;
        }

        public void Registrar()
        {

            Worker myWorker = new Worker(_hora, _minutos, _segundos);
            ScheduleDelegate fx = new ScheduleDelegate(myWorker.MarcarEntrada);
            ScheduleDelegate fy = new ScheduleDelegate(myWorker.MarcarEntradaIntervalo);
            ScheduleDelegate fz = new ScheduleDelegate(myWorker.MarcarSaidaIntervalo);
            ScheduleDelegate fw = new ScheduleDelegate(myWorker.MarcarSaidaIntervalo);

            Parallel.Invoke(
                new Action(fx),
                new Action(fy),
                new Action(fz),
                new Action(fw)
            );
        }

        private void MarcarEntrada()
        {
            var intervalo = new Intervalo();
           
        }

        private void MarcarEntradaIntervalo()
        {
            _hora += (_hora == 9) ? 12 : 13;
            _minutos = (_hora == 12) ? _rnd.Next(50, 59) : _rnd.Next(1, 30);
            _segundos = _rnd.Next(1, 59);
           
        }

        private void MarcarSaidaIntervalo()
        {
            _hora += _hora++;
            _minutos = _minutos + _rnd.Next(1, 30);
            _segundos = _rnd.Next(1, 59);
           
        }

        private void MarcarSaida()
        {
            _hora += _hora += 4;
            _minutos = _minutos + _rnd.Next(1, 30);
            _segundos = _rnd.Next(1, 59);
           
        }
    }
}
