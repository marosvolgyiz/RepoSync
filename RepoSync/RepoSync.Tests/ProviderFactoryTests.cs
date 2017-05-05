using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RepoSync.Tests
{
    [TestClass]
    public class ProviderFactoryTests
    {
        [TestMethod]
        public void CreateProvider()
        {
            var providerList = new List<string>{
                    "RepoSync.Providers.MemoryProvider",
                    "RepoSync.Providers.FileSystemProvider",
                    "RepoSync.Providers.SnClientLibraryProvider"
            };

            foreach (var providerName in providerList)
            {
                var createdProvider = ProviderFactory.Create(providerName, new Dictionary<string, string>());
                var type = createdProvider.GetType();
                Assert.AreEqual(type.Namespace, providerName);

            }
        }
    }
}
