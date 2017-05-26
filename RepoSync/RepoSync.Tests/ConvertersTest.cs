using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task TestSerializeBasic()
        {
            var c1 = Content.CreateNew<Content>("Root", "Task", "TestTask", server: new ServerContext());
            var json = c1.Content2JSON();
            Assert.AreEqual(json, "{\"Fields\":{\"Path\":null,\"Name\":\"TestTask\",\"Id\":0},\"Permissions\":{}}");
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
