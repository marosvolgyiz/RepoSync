using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RepoSync
{
    /// <summary>
    /// This class can be used to create Provider instances from provider names with specified options
    /// </summary>
    public static class ProviderFactory
    {
        /// <summary>
        /// This method creates a specified provider from a provider name and sets up it's options
        /// </summary>
        /// <param name="providerName">The name of the provider. The system will try to load from 'RepoSync.Providers."providerName"' assembly. </param>
        /// <param name="settings">The settings for the provider. </param>
        /// <returns>The created provider instance. </returns>
        public static IRepoSyncProvider Create(string providerName, Dictionary<string, string> settings)
        {
            if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.GetName().Name != providerName))
                Assembly.Load(providerName);

            var providerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()).Single(t =>
                    t.Assembly.GetName().Name == providerName
                    &&
                    t.GetInterfaces().Contains(typeof(IRepoSyncProvider))
                );

            var provider = (IRepoSyncProvider) Activator.CreateInstance(providerType);
            provider.Settings = settings;

            return provider;
        }
    }
}
