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

            //Content conversions

            var json = azraelContent.Result.Content2JSON();
            SyncContent sc = json.JSON2Content();
            var translatedJson = sc.Content2JSON();
            //Use the FileSystem provider
            RepoSync.Providers.FileSystemProvider.FileSystemProvider fsProvider = new RepoSync.Providers.FileSystemProvider.FileSystemProvider();
            fsProvider.Settings.Add("Path", @"c:\temp\test");

            //Collect contents
            List<SyncContent> syncContentList = new List<SyncContent>();
            syncContentList.Add(sc);
            var collection = Content.LoadCollectionAsync("/Root/Sites/Azrael");
            collection.Wait();
            //Convert SenseNet.Client.Content to SyncContent
            syncContentList.AddRange(collection.Result.Select(c => c.Content2SyncContent()));

            //Write Contents to the FileSystem
            fsProvider.WriteAsync(syncContentList).Wait();
            //Read Contents from the FileSystem
            var ra = fsProvider.ReadAsync();
            ra.Wait();

        }
    }
}
