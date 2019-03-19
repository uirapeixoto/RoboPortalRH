using System.Configuration;
using System.IO;

namespace Bot.Marcacao.Helper
{
    public class AbstractConfig
    {
        public static string FolderLog
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("LogFolder"); ;
            }
        }

        public static string FileLog
        {
            get
            {
                return Path.Combine(FolderLog, ConfigurationManager.AppSettings.Get("LogServiceFile"));
            }
        }

        public static string FileErrorLog
        {
            get
            {
                return Path.Combine(FolderLog, ConfigurationManager.AppSettings.Get("LogServiceFileError"));
            }
        }
    }
}
