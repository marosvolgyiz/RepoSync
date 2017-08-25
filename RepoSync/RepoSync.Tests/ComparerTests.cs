using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoSync.ContentExtensions;

namespace RepoSync.Tests
{
    [TestClass]
    public class ComparerTests
    {

        [TestMethod]
        public async Task CompareOneWithNull()
        {
            var c1 = new SyncContent();
            var result = c1.CompareTo(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CompareWithSame()
        {
            var c1 = new SyncContent() { Name = "Content1" };
            var c2 = new SyncContent() { Name = "Content1" };
            var result = c1.CompareTo(c2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CompareWithDifferent()
        {
            var c1 = new SyncContent() { Name = "Content1" };
            var c2 = new SyncContent() { Name = "Content Other Name" };
            var result = c1.CompareTo(c2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task CompareSameWithDifferentId()
        {
            var c1 = new SyncContent() { Name = "Content1", Id = 1 };
            var c2 = new SyncContent() { Name = "Content1", Id = 2 };
            var result = c1.CompareTo(c2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task CompareSameWithDifferentParentId()
        {
            var c1 = new SyncContent()
            {
                Name = "Content1",
                Fields = new Dictionary<string, object>
                    {
                        {"ParentId", 1 }
                    }
            };
            var c2 = new SyncContent()
            {
                Name = "Content1",
                Fields = new Dictionary<string, object>
                {
                    {"ParentId", 3 }
                }
            };

            var result = c1.CompareTo(c2);
            Assert.IsTrue(result);
        }
    }
}
