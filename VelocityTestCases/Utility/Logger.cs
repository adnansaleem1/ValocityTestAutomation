using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityTestCases.References;

namespace VelocityTestCases.Utility
{
    static class Logger
    {
        public static void Log(string text)
        {
            if (Config.LogTestOutPut)
            {
                Console.WriteLine(text);
            }
        }
        public static void Log(string text, Exception ex)
        {
            if (Config.LogTestOutPut)
            {
                Console.WriteLine(text);
            }
            if (Config.LogException)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Log(Exception ex)
        {
            if (Config.LogException)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal static void Log(string p, RelevantCodes.ExtentReports.LogStatus logStatus)
        {
            throw new NotImplementedException();
        }

        internal static void Log(string p, LogStatus logStatus, RelevantCodes.ExtentReports.ExtentTest test)
        {
            test.Log(logStatus, p);
        }

        internal static void Log(string p, Exception ex, LogStatus logStatus, ExtentTest test)
        {
            test.Log(logStatus, p);
        }
    }
}
