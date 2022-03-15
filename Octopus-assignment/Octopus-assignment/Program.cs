using System.Configuration;
using Octopus_assignment.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using Octopus_assignment.Models;
using System;

namespace Octopus_assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string settingConfigFolder = ConfigurationManager.AppSettings["configPath"];
           
            AssertSettings bll = new AssertSettings(settingConfigFolder);
            bll.DeserializeDataIntoModels();

            var orderedDeployments = bll.GetOrderedDeployments();
            orderedDeployments.ToList().
            ForEach(d =>
            {
                string[] key = d.Key.Split('|');
                string message = String.Format("`{0}` is kept because it was the most recently deployed to `{1}`",
                    d.Value.ReleaseId, key[1]);
                Console.WriteLine(message);
                Logger.logMessage(message);
            });

            if(orderedDeployments.Count == 0)
            {
                Console.WriteLine("The data seems incorrect");
            }

            Console.WriteLine(System.Environment.NewLine);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
