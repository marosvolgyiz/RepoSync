using System.Configuration;
using CommandLine;

namespace RepoSync
{
    public class CommandLineOptions
    {


        [Option("sourceProvider")]
        public string SourceProviderName { get; set; }

        [Option("sourceProviderSettings", HelpText = "The provider specific settings for the source provider")]
        public string SourceProviderSettings { get; set; }

        [Option("targetProvider")]
        public string TargetProviderName { get; set; }

        [Option("targetProviderSettings", HelpText = "The provider specific settings for the target provider")]
        public string TargetProviderSettings { get; set; }

        [Option('a', "ActionType")]
        public ActionType ActionType { get; set; }
    }
}
