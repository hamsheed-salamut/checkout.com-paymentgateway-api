using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Common.Logger
{
    public class Logger : ILogger
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public Logger()
        {

        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
