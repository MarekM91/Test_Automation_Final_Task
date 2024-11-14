using log4net.Config;
using log4net;
using System.Reflection;

namespace Test_Automation_Final_Task
{
    public static class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        public static ILog Log { get; } = LogManager.GetLogger(typeof(Logger));
    }
}
