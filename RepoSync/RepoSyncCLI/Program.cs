using CommandLine;


namespace RepoSync
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            var isValid = Parser.Default.ParseArgumentsStrict(args, options);
        }
    }
}
