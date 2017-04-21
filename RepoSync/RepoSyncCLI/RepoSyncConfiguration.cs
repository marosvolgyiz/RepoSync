using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace RepoSync
{
    public class RepoSyncConfiguration
    {
        private const string SourceProviderNameKey = "SourceProviderName";
        private const string SourceProviderOptionsKey = "SourceProviderName";

        private const string TargetProviderNameKey = "TargetProviderName";
        private const string TargetProviderOptionsKey = "TargetProviderName";

        private const string ActionTypeKey = "ActionType";

        private static RepoSyncConfiguration _instance;
        public static RepoSyncConfiguration Current => _instance ?? (_instance = new RepoSyncConfiguration());

        public string SourceProviderName { get; private set; }
        public string SourceProviderSettings { get; private set; }
        public Dictionary<string, string> SourceProviderSettingsDictionary => GetOptionsDictionary(SourceProviderSettings);

        public string TargetProviderName { get; private set; }
        public string TargetProviderSettings { get; private set; }
        public Dictionary<string, string> TargetProviderSettingsDictionary => GetOptionsDictionary(TargetProviderSettings);

        public ActionType ActionType { get; private set; }

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

        public void OverrideWithCliOptions(CommandLineOptions cliOptions)
        {
            var cliOptionsType = cliOptions.GetType();
            var cliPropertyInfos = cliOptionsType.GetProperties();
            var typeInfo = GetType();
            foreach (var cliPropertyInfo in cliPropertyInfos)
            {
                var configPropertyInfo = typeInfo.GetProperty(cliPropertyInfo.Name);
                if (configPropertyInfo != null)
                {
                    var configValue = configPropertyInfo.GetValue(this);
                    var cliOptionValue = cliPropertyInfo.GetValue(cliOptions);
                    if (configValue != null
                        &&
                        cliOptionValue != null
                        &&
                        cliPropertyInfo.PropertyType == configPropertyInfo.PropertyType
                        &&
                        configPropertyInfo.CanWrite
                    )
                    {
                        configPropertyInfo.SetValue(this, cliOptionValue);
                    }
                }
            }
        }

        private RepoSyncConfiguration()
        {
            SourceProviderName = ConfigurationManager.AppSettings[SourceProviderNameKey];
            SourceProviderSettings = ConfigurationManager.AppSettings[SourceProviderOptionsKey];

            TargetProviderName = ConfigurationManager.AppSettings[TargetProviderNameKey];
            TargetProviderSettings = ConfigurationManager.AppSettings[TargetProviderOptionsKey];

            ActionType actionType;
            if (Enum.TryParse(ConfigurationManager.AppSettings[ActionTypeKey], true, out actionType))
            {
                ActionType = actionType;
            }
        }
    }
}
