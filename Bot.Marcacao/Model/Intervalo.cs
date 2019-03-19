using Bot.Marcacao.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Marcacao.Model
{
    public class Intervalo
    {
        public int Hora { get; set; }
        public int Minutos { get; set; }
        public int Segundos { get; set; }
        public IntervaloEnum Descricao { get; set; }

        private DateTime TimeNow
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime Data
        {
            get
            {
                return new DateTime(TimeNow.Year, TimeNow.Month, TimeNow.Day, Hora, Minutos, Segundos);
            }
        }

        public List<Intervalo> ListTime()
        {

            var rnd = new Random();
            var list = new List<Intervalo>
        {
            new Intervalo{
                Hora =  rnd.Next(9, 11),
                Minutos = (Hora == 9)? rnd.Next(1,59) : rnd.Next(1,10),
                Segundos = rnd.Next(1,59),
                Descricao = IntervaloEnum.InicioJornada
            },
            new Intervalo
            {
                Hora = rnd.Next(12,14),
                Minutos = (Hora == 12)? rnd.Next(1,59) : rnd.Next(1,10),
                Segundos = rnd.Next(1,59),
                Descricao = IntervaloEnum.InicioIntervalo
            },
            new Intervalo
            {
                Hora = 13,
                Minutos = (Hora == 12)? rnd.Next(1,59) : rnd.Next(1,30),
                Segundos = rnd.Next(1,59),
                Descricao = IntervaloEnum.FimIntervalo
            },
            new Intervalo
            {
                Hora = rnd.Next(18,19),
                Minutos = (Hora == 18)? rnd.Next(1,59) : rnd.Next(1,30),
                Segundos = rnd.Next(1,59),
                Descricao = IntervaloEnum.FimJornada
            }
        };

            var modificadorFimJornada = list.Where(x => x.Descricao == IntervaloEnum.InicioJornada).FirstOrDefault().Hora;
            if (modificadorFimJornada == 10)
            {
                list.Where(x => x.Descricao == IntervaloEnum.FimJornada).FirstOrDefault().Hora = 19;
            }

            var modificadorFimIntervalo = list.Where(x => x.Descricao == IntervaloEnum.InicioIntervalo).FirstOrDefault().Hora;
            if (modificadorFimIntervalo == 13)
            {
                list.Where(x => x.Descricao == IntervaloEnum.FimIntervalo).FirstOrDefault().Hora = 14;
            }

            return list;
        }
    }
}
