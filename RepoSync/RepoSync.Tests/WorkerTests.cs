using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoSync.ContentExtensions;
using SenseNet.Client;

namespace RepoSync.Tests
{
    [TestClass]
    public class WorkerTests
    {

        private SyncContent CreateC1(string otherName = null)
        {
            return new SyncContent()
            {
                Fields = new Dictionary<string, object>
                {
                    {"Path", "Root/C1"},
                    {"Type", "Task"},
                    {"Name", string.IsNullOrWhiteSpace(otherName) ? "TestTask" : otherName}
                }
            };
        }
        private SyncContent CreateC2(string otherName = null)
        {
            return new SyncContent()
            {
                Fields = new Dictionary<string, object>
                {
                    {"Path", "Root/C2"},
                    {"Type", "Task"},
                    {"Name", string.IsNullOrWhiteSpace(otherName) ? "TestTask with different name" : otherName}
                }
            };
        }


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

            await memoryIn.WriteAsync(new List<SyncContent>
            {
                CreateC1()
            });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task CompareOneWithDifferent()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryIn.WriteAsync(new List<SyncContent> { CreateC1() });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask with different name", server: new ServerContext());
            c2.Path = "root/c1";

            await memoryOut.WriteAsync(new List<SyncContent> { CreateC2() });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 0);

        }

        [TestMethod]
        public async Task CompareOneWithSimilar()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryIn.WriteAsync(new List<SyncContent> { CreateC1() });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryOut.WriteAsync(new List<SyncContent> { CreateC1() });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 0);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Compare2With1()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryIn.WriteAsync(new List<SyncContent> { CreateC1(), CreateC2() });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryOut.WriteAsync(new List<SyncContent> { CreateC2() });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Compare2With2()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());

            await memoryIn.WriteAsync(new List<SyncContent> { CreateC1(), CreateC2() });
            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryOut.WriteAsync(new List<SyncContent> { CreateC1(), CreateC2() });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 0);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 2);
        }

        [TestMethod]
        public async Task Compare2With2_1Diff()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryIn.WriteAsync(new List<SyncContent> { CreateC1(), CreateC2() });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            await memoryOut.WriteAsync(new List<SyncContent> { CreateC1(), CreateC2("otherName") });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Sync1ToEmpty()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = CreateC1();

            await memoryIn.WriteAsync(new List<SyncContent> { c1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());

            await memoryOut.WriteAsync(new List<SyncContent> { });

            var w = new Worker(memoryIn, memoryOut, ActionType.Sync);
            IEnumerable<RepoSyncResult> result = await w.Run();

            Assert.AreEqual(result.Count(), 1);

            var loaded = await memoryOut.LoadAsync(c1.Path);
            Assert.AreEqual(c1.Name, loaded.Name);
        }
    }
}
