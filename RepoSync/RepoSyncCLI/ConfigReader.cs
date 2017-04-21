using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace RepoSync
{
    public class ConfigReader
    {
        private const string SourceProviderNameKey = "RepoSync-SourceProviderName";
        private const string SourceProviderOptionsKey = "RepoSync-SourceProviderName";

        private const string TargetProviderNameKey = "RepoSync-TargetProviderName";
        private const string TargetProviderOptionsKey = "RepoSync-TargetProviderName";

        private static ConfigReader _instance;
        public static ConfigReader Current => _instance ?? (_instance = new ConfigReader());

        public string SourceProviderName { get; }
        public Dictionary<string, string> SourceProviderOptions { get; }

        public string TargetProviderName { get; }
        public Dictionary<string, string> TargetProviderOptions { get; }


        private static Dictionary<string, string> GetOptionsDictionary(string options)
        {
            if (!string.IsNullOrWhiteSpace(options))
            {
                try
                {
                    var dictionary = options.Split(';')
                        .Select(a =>
                        {
                            var kv = a.Split('=');
                            return new KeyValuePair<string, string>(kv[0], kv[1]);
                        })
                        .ToDictionary(a => a.Key, a => a.Value);
                    return dictionary;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error: {e.Message} \r\n ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        $"There was an error parsing options. Please use ';' separated key/value pairs \\r\\n e.g.: path=c:\\temp;username=admin) \r\n Value: '{options}'");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            return new Dictionary<string, string>();
        }

        private ConfigReader()
        {
            SourceProviderName = ConfigurationManager.AppSettings[SourceProviderNameKey];
            SourceProviderOptions = GetOptionsDictionary(ConfigurationManager.AppSettings[SourceProviderOptionsKey]);

            TargetProviderName = ConfigurationManager.AppSettings[TargetProviderNameKey];
            TargetProviderOptions = GetOptionsDictionary(ConfigurationManager.AppSettings[TargetProviderOptionsKey]);
        }
    }
}
