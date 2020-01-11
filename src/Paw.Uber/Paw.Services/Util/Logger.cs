using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Paw.Services.Util
{
    public static class Logger
    {
        public static void Info(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
        }

        public static void Warning(string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
        }

        public static void Error(string format, params object[] args)
        {
            Trace.TraceError(format, args);
        }
    }
}
