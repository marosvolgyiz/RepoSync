using CommandLine;


namespace RepoSync
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigReader.Current;
            var options = new CommandLineOptions();
            var isValid = Parser.Default.ParseArgumentsStrict(args, options);
        }
    }
}
