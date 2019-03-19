using Bot.Marcacao.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Marcacao
{
    class Program
    {
        static void Main(string[] args)
        {
            var schedule = new ScheduleHelper();
            schedule.Run();
        }

    }
}
