using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StudInfoSys.Helpers
{
    public class Log
    {
        static Log()
        {
            LogFileDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        }

        public static string LogFileDirectory { get; set; }
        

        /// <summary>
        /// Logs a message to the given log file
        /// </summary>
        /// <param name="logFile">The filename to log to</param>
        /// <param name="text">The message to log</param>
        public static void WriteLog(string logFile, string text)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(DateTime.Now.ToString());
            message.AppendLine(text);
            message.AppendLine("=========================================");

            System.IO.File.AppendAllText(LogFileDirectory + logFile, message.ToString());
        }
    }
}