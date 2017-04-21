using System;
using CommandLine;


namespace RepoSync
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            var isValid = Parser.Default.ParseArgumentsStrict(args, options);
            RepoSyncConfiguration.Current.OverrideWithCliOptions(options);
            Console.ReadLine();
        }
    }
}
