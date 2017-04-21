using System;
using System.Diagnostics;
using CommandLine;
using RepoSync.Providers.MemoryProvider;
using RepoSync;
using RepoSync.Providers.FileSystemProvider;

namespace RepoSync.CLI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            try
            {
                var isValid = Parser.Default.ParseArguments(args, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            RepoSyncConfiguration.Current.OverrideWithCliOptions(options);
            Console.ReadLine();
            MemoryProvider source = new MemoryProvider();
            MemoryProvider target = new MemoryProvider();

            var a = new Worker(source, target, ActionType.Compare);
        }
    }
}
