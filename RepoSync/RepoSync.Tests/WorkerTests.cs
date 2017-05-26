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
            Assert.AreEqual(result.Count(r=>r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task CompareOneWithDifferent()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";
            await memoryIn.WriteAsync(new List<Content> { c1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask with different name", server: new ServerContext());
            c2.Path = "root/c1";

            await memoryOut.WriteAsync(new List<Content> {c2});

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 0);

        }

        [TestMethod]
        public async Task CompareOneWithSimilar()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";
            await memoryIn.WriteAsync(new List<Content> { c1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c2.Path = "root/c1";

            await memoryOut.WriteAsync(new List<Content> { c2 });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 0);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Compare2With1()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";
            var c1_1 = Content.CreateNew<Content>("Root", "Task", "TestTask_1", server: new ServerContext());
            c1_1.Path = "root/c1_1";

            await memoryIn.WriteAsync(new List<Content> { c1, c1_1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c2.Path = "root/c1";

            await memoryOut.WriteAsync(new List<Content> { c2 });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Compare2With2()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";
            var c1_1 = Content.CreateNew<Content>("Root", "Task", "TestTask_1", server: new ServerContext());
            c1_1.Path = "root/c1_1";

            await memoryIn.WriteAsync(new List<Content> { c1, c1_1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c2.Path = "root/c1";
            var c2_1 = Content.CreateNew<Content>("Root", "Task", "TestTask_1", server: new ServerContext());
            c2_1.Path = "root/c1_1";

            await memoryOut.WriteAsync(new List<Content> { c2, c2_1 });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 0);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 2);
        }

        [TestMethod]
        public async Task Compare2With2_1Diff()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";
            var c1_1 = Content.CreateNew<Content>("Root", "Task", "TestTask_1", server: new ServerContext());
            c1_1.Path = "root/c1_1";

            await memoryIn.WriteAsync(new List<Content> { c1, c1_1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c2 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c2.Path = "root/c1";
            var c2_1 = Content.CreateNew<Content>("Root", "Task", "TestTask_1_Diff", server: new ServerContext());
            c2_1.Path = "root/c1_1";

            await memoryOut.WriteAsync(new List<Content> { c2, c2_1 });

            var w = new Worker(memoryIn, memoryOut, ActionType.Compare);
            List<RepoSyncCompareResult> result = (await w.Run()).Cast<RepoSyncCompareResult>().ToList();
            Assert.AreEqual(result.Count(r => r.IsDifferent), 1);
            Assert.AreEqual(result.Count(r => !r.IsDifferent), 1);
        }

        [TestMethod]
        public async Task Sync1ToEmpty()
        {
            var memoryIn = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            c1.Path = "root/c1";

            await memoryIn.WriteAsync(new List<Content> { c1 });

            var memoryOut = ProviderFactory.Create("RepoSync.Providers.MemoryProvider", new Dictionary<string, string>());

            await memoryOut.WriteAsync(new List<Content> { });

            var w = new Worker(memoryIn, memoryOut, ActionType.Sync);
            IEnumerable<RepoSyncResult> result = await w.Run();

            Assert.AreEqual(result.Count(), 1);

        }
    }
}
