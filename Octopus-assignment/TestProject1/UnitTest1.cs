using System.IO;
using NUnit.Framework;
using Octopus_assignment.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestProject1
{
    public class Tests
    {
        public string DataPath
        {
            get
            {
                DirectoryInfo tc = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);
                string testDataFolder = tc.Parent != null ? tc.Parent.Parent != null ?
                    tc.Parent.Parent.Parent != null ? tc.Parent.Parent.Parent.FullName : string.Empty : string.Empty : string.Empty;
                testDataFolder = testDataFolder + @"\TestData";
                return testDataFolder;
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var bll = new AssertSettings(DataPath);
            bll.DeserializeDataIntoModels();
            var orderedDeployments = bll.GetOrderedDeployments();

            Assert.AreEqual(orderedDeployments.Count, 3);
            Assert.AreEqual(orderedDeployments.Keys.First(), "Project-1|Environment-1");
            Assert.AreEqual(orderedDeployments.Values.First().DeployedAt.ToString("MM/dd/yyyy hh:mm:ss tt"), "01/02/2000 10:00:00 AM");
            Assert.AreEqual(orderedDeployments.Values.Last().DeployedAt.ToString("MM/dd/yyyy hh:mm:ss tt"), "01/02/2000 11:00:00 AM");
        }

        [Test]
        public void Test2()
        {
            var bll = new AssertSettings("");
            bll.DeserializeDataIntoModels();
            var orderedDeployments = bll.GetOrderedDeployments();

            Assert.AreEqual(orderedDeployments.Count, 0);
        }
    }
}