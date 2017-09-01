using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RepoSync
{
    /// <summary>
    /// This class can be used to create Provider instances from provider names with specified options
    /// </summary>
    public class ProviderFactory
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
            {
                try
                {
                    Assembly.Load(providerName);

                }
                catch (Exception ex)
                 {
                    throw new Exception("Can't load assembly: ", ex);
                }
            }

            var providerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes()).Single(t =>
                    t.Assembly.GetName().Name == providerName
                    &&
                    t.GetInterfaces().Contains(typeof(IRepoSyncProvider))
                );

            var provider = (IRepoSyncProvider)Activator.CreateInstance(providerType);
            provider.Settings = settings;

            return provider;
        }

        static List<Type> _Providers = null;
        public  List<Type> Providers
        {
            get
            {
                if (_Providers == null)
                {
                    LoadProviders();
                }
                return _Providers;
            }
            set { }
        }
        public static void LoadProviders()
        {
            _Providers = new List<Type>();
            var type = typeof(IRepoSyncProvider);

            List<Assembly> allAssemblies = new List<Assembly>();
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(Assembly.LoadFile(dll));
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                try
                {
                    var types = assembly.GetTypes();
                    _Providers.AddRange(types.Where(t =>
                        t.GetInterfaces().Contains(type)));
                }
                catch (Exception ex)
                {

                }

            }

        }
    }
}
