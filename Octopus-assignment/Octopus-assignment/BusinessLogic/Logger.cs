using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Octopus_assignment.BusinessLogic
{
    internal static class Logger
    {
        public static void logMessage(string message)
        {
            string settingsLogFolder = ConfigurationManager.AppSettings["logPath"];
            File.AppendAllText(string.Concat(settingsLogFolder, @"\log.txt"), 
                string.Concat(System.Environment.NewLine, message));
        }
    }
}
