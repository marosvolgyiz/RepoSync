using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoSync.ContentExtensions;
using SenseNet.Client;

namespace RepoSync.Tests
{
    [TestClass]
    public class ConvertersTest
    {

        [TestMethod]
        public async Task TestSerializeFromSnContent()
        {
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            var json = c1.Content2JSON();
            Assert.AreEqual(json, "{\"Id\":0,\"Name\":\"TestTask\",\"Path\":null,\"Fields\":{\"Path\":null,\"Name\":\"TestTask\",\"Id\":0},\"Permissions\":{}}");
        }

        [TestMethod]
        public async Task TestSerializeFromSyncContent()
        {
            var c1 = new SyncContent()
            {
                Path="Root/Example",
                Name="TestContentName"
            };
            var json = c1.Content2JSON();
            Assert.AreEqual(json, "{\"Id\":0,\"Name\":\"TestContentName\",\"Path\":\"Root/Example\",\"Fields\":{\"Path\":\"Root/Example\",\"Name\":\"TestContentName\",\"Id\":0},\"Permissions\":{}}");
        }

        [TestMethod]
        public async Task TestDeserializeBasic()
        {
            var contentJson = "{\"Fields\":{\"Path\":null,\"Name\":\"TestTask\",\"Id\":2,\"ExampleProp\":1},\"Permissions\":{}}";
            var c1 = contentJson.JSON2Content();
            Assert.AreEqual(c1.Fields["Name"], "TestTask");
            Assert.AreEqual(c1.Fields["ExampleProp"], (Int64)1);
            Assert.AreEqual(c1.Fields["Id"], (Int64)2);
        }
    }
}
