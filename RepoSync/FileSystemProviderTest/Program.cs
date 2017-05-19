using SenseNet.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RepoSync.ContentExtensions;

namespace FileSystemProviderTest
{
    class Program
    {
        static ServerContext sctx;
        static void Main(string[] args)
        {
            sctx = new ServerContext
            {
                Url = ConfigurationManager.AppSettings["SiteUrl"],
                Username = ConfigurationManager.AppSettings["Username"],
                Password = ConfigurationManager.AppSettings["Password"]
            };
            ClientContext.Initialize(new[] { sctx

            });
            var azraelContent = Content.LoadAsync("/Root/Sites/Azrael");
            azraelContent.Wait();
            var json = azraelContent.Result.Content2JSON();
            SyncContent sc = json.JSON2Content();
            var translatedJson = sc.Content2JSON();

        }
    }
}
