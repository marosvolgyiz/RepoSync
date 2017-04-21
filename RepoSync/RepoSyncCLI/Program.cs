using System;
using CommandLine;

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
        }
    }
}
