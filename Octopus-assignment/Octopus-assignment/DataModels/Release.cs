using System;

namespace Octopus_assignment.Models
{
    public class Release
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
        public DateTime Created { get; set; }
    }
}
