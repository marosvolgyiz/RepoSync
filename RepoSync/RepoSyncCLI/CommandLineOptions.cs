using CommandLine;

namespace RepoSync
{
    public class CommandLineOptions
    {
        [Option("sourceProvider", DefaultValue = "SnClientLibraryProvider")]
        public string SourceProviderName { get; set; }

        [Option('s', "sourceProviderSettings", HelpText = "The provider specific settings for the source provider")]
        public string SourceProviderSettings { get; set; }

        [Option('t', "targetProviderSettings", HelpText = "The provider specific settings for the target provider")]
        public string TargetProviderSettings { get; set; }

        [Option("targetProvider", DefaultValue = "FileSystemProvider")]
        public string TargetProviderName { get; set; }

        [Option('a', "ActionType", DefaultValue = ActionType.Compare)]
        public ActionType ActionType { get; set; }
    }
}
