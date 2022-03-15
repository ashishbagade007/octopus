using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace Octopus_assignment.BusinessLogic
{
    internal static class DeserializeData
    {
        public static List<T> AssertData<T>(string configFilePath)
        {
            try
            {
                using (StreamReader r = new StreamReader(configFilePath))
                {
                    string json = r.ReadToEnd();
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    return jss.Deserialize<List<T>>(json);
                }
            }
            catch(Exception ex)
            {
                Logger.logMessage(ex.ToString());
            }
            return new List<T>();
        }
    }
}
