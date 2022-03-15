using System.Collections.Generic;
using System.Linq;
using Octopus_assignment.DataModels;
using Octopus_assignment.Models;

namespace Octopus_assignment.BusinessLogic
{
    public class AssertSettings
    {
        public List<Models.Deployment> Deployments { get; private set; }
        public List<Models.Environment> Environments { get; private set; }
        public List<Models.Project> Projects { get; private set; }
        public List<Models.Release> Releases { get; private set; }
        public string DataConfigPath { get; private set; }

        public AssertSettings(string configPath)
        {
            this.DataConfigPath = configPath;
        }

        private string GetDataPath(DataType dataType)
        {
            if (!string.IsNullOrWhiteSpace(DataConfigPath))
            {
                return DataConfigPath + @"\" + dataType.ToString() + ".json";
            }
            return "";
        }

        public void DeserializeDataIntoModels()
        {
            Deployments = DeserializeData.AssertData<Deployment>(GetDataPath(DataType.Deployments));
            Environments = DeserializeData.AssertData<Models.Environment>(GetDataPath(DataType.Environments));
            Projects = DeserializeData.AssertData<Project>(GetDataPath(DataType.Projects));
            Releases = DeserializeData.AssertData<Release>(GetDataPath(DataType.Releases));

            Deployments.ForEach(de =>
            {
                de.Environment = Environments.Where(e => e.Id == de.EnvironmentId).FirstOrDefault();
                de.Release = Releases.Where(r => r.Id == de.ReleaseId).FirstOrDefault();
                de.Project = Projects.Where(p => p.Id == de.Release.ProjectId).FirstOrDefault();
            });
        }

        public Dictionary<string, Deployment> GetOrderedDeployments()
        {
            if (Deployments.Count > 0)
            {
                var orderedDeployments =
                Deployments.
                Where(d => d.Project != null && d.Environment != null).
                GroupBy(d => new { ProjectId = d.Project.Id, d.EnvironmentId }).
                ToDictionary(
                    d => string.Concat(d.Key.ProjectId, "|", d.Key.EnvironmentId),
                    d => d.OrderByDescending(m => m.DeployedAt).First()
                );

                return orderedDeployments;
            }
            return new Dictionary<string, Deployment>();
        }
    }
}
