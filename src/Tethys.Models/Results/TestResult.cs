using System.ComponentModel.DataAnnotations.Schema;
using NodaTime;

namespace Tethys.Models;

public record TestResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TestStatus Status { get; set; }
        public decimal Duration { get; set; }
        public Instant RunAt { get; set; }
        public ResultFormat TestType { get; set; }
        public List<string> Tags { get; set; } = new();
        public TestTrigger Trigger { get; set; }
        // Re-enable once we can create projects
        //public Project Project { get; set; }
        public string TestFile { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public Metadata? Metadata { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class Metadata
    {
        public GitHubData GithubData { get; set; } = new();
    }

    public class GitHubData
    {
        public string SHA { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string RunId { get; set; } = string.Empty;
        public string GitHubUrl { get; set; } = string.Empty;
        public string GitHubRepository { get; set; } = string.Empty;
    }
