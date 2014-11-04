namespace WhiteBox.Kernel.Log
{
    using System;
    using System.IO;
    using log4net;
    using log4net.Config;

    public class BaseLog
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseLog));

        public BaseLog()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web.config")));
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void Error(object message)
        {
            log.Error(message);
        }

        public void Error(object message, Exception e)
        {
            log.Error(message, e);
        }

        public void Fatal(object message)
        {
            log.Fatal(message);
        }

        public void Fatal(object message, Exception e)
        {
            log.Fatal(message, e);
        }
    }
}
