using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Marcacao.Helper
{
    public class LibraryHelper : AbstractConfig
    {
        public static void WriterLog(string log)
        {
            try
            {
                var file = new StreamWriter(FileLog, true);
                file.WriteLine(log);
                file.Flush();
                file.Close();
            }
            catch (Exception e)
            {
                StreamWriter file = new StreamWriter(FileErrorLog, true);
                file.WriteLine(string.Format("{0} : {1}", DateTime.Now, e.Message.ToString()));
                file.Flush();
                file.Close();
            }
        }
    }
}
