using CommandLine;
using RepoSync;

namespace RepoSync
{
    public class CommandLineOptions
    {
        [Option('s', "source", Required = true, HelpText = "The source path")]
        public string SourcePath { get; set; }

        [Option("sourceProvider", DefaultValue = "SnClientLibraryProvider")]
        public string SourceProviderName { get; set; }

        [Option('t', "target", Required = true, HelpText = "The target path")]
        public string TargetPath { get; set; }

        [Option("targetProvider", DefaultValue = "FileSystemProvider")]
        public string TargetProviderName { get; set; }

        [Option('a', "ActionType", DefaultValue = ActionType.Compare)]
        public ActionType ActionType { get; set; }
    }
}
