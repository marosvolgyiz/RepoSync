using System;
using System.Threading.Tasks;
using CommandLine;
using RepoSync.Providers.MemoryProvider;

namespace RepoSync.CLI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            var config = RepoSyncConfiguration.Current;

            Parser.Default.ParseArguments(args, options);
            
            config.OverrideWithCliOptions(options);

            var source = ProviderFactory.Create(config.SourceProviderName, config.SourceProviderSettingsDictionary);
            var target = ProviderFactory.Create(config.TargetProviderName, config.TargetProviderSettingsDictionary);

            var a = new Worker(source, target, ActionType.Compare);

            Task.WaitAll(a.Run());

            Console.ReadLine();
        }
    }
}
