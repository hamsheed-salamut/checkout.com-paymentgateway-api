using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Common.Logger
{
    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
    }
}
