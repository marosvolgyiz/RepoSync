using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.Client;

namespace RepoSync.Tests
{
    [TestClass]
    public class WorkerTests
    {

        [TestMethod]
        public void CreateWorker()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            Assert.IsNotNull(w);
        }

        [TestMethod]
        public async Task CompareTwoEmpties()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            var result = await w.Run();
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public async Task CompareOneWithEmpty()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());

            await memoryIn.WriteAsync(new List<Content>
            {
                Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext())
            });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.IsTrue(result.Single().IsDifferent);
            Assert.IsTrue(result.Single().Success);
        }
    }
}
