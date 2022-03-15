using System;

namespace Octopus_assignment.Models
{
    public class Deployment
    {
        public string Id { get; set; }
        public string ReleaseId { get; set; }
        public string EnvironmentId { get; set; }
        public DateTime DeployedAt { get; set; }
        public Release Release { get; set; }
        public Project Project { get; set; }
        public Environment Environment { get; set; }
    }
}
