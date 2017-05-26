using RepoSync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SenseNet.Client;
using System.IO;
using System.Linq;
using RepoSync.ContentExtensions;
using RepoSync.Providers.FileSystemProvider.Helpers;

namespace RepoSync.Providers.FileSystemProvider
{
    public class FileSystemProvider : IRepoSyncProvider
    {
        public FileSystemProvider()
        {
            if(Settings == null)
            {
                Settings = new Dictionary<string, string>();
            }
        }
        public const string sncExtension = ".snc";
        public List<string> RequiredSettings => new List<string> { "Path" };
        public Dictionary<string, string> Settings { get; set; }
        public IRepoSyncFilter Filter { get; set; }
        private FileInfo[] _files = null;
        public FileInfo[] Files
        {
            get
            {
                CheckRequireSetting();
                if (_files == null)
                {
                    var di = new DirectoryInfo(Settings["Path"]);
                    _files = di.GetFiles("*.*", SearchOption.AllDirectories);
                }
                return _files;
            }
        }
        public async Task<SyncContent> LoadAsync(string path)
        {
            throw new NotImplementedException();
        }
        private void CheckRequireSetting()
        {
            foreach (var item in RequiredSettings)
            {
                if (!Settings.ContainsKey(item))
                {
                    throw new ArgumentException("The provider need required setting: " + item);
                }
            }
        }
        public async Task<List<string>> ReadPathsAsync()
        {
            return Files.Select(f=>f.FullName).ToList();
        }

        public async Task<List<SyncContent>> ReadAsync()
        {
            List<SyncContent> contents = new List<SyncContent>();
            List<string> allfiles = await ReadPathsAsync();
            foreach (var item in allfiles)
            {
                var fi = new FileInfo(item);
               
                //Read .snc
                string sncText = string.Empty;
                string Name = fi.Name;
                if (fi.FullName.EndsWith(sncExtension))
                {
                    //Find binary 
                    Name = fi.Name.Substring(fi.Name.Length - sncExtension.Length);
                    var binaryFi = new FileInfo(fi.FullName.Substring(0, fi.FullName.Length - sncExtension.Length));
                    
                    if (binaryFi == null)
                    {
                        //TODO:Content does not have binary
                    }
                    else
                    {
                        //TODO:Content does have binary
                    }
                    sncText = File.ReadAllText(fi.FullName);
                }
                else
                {
                    //Only binary found, the snc is not found
                    //TODO: Create a content with name of the file
                    
                    //TODO: Set Binary
                }
                
                //Create Content object
                var contentObject = sncText.JSON2Content();
                //TODO: Set Path
                string Path = "/" + fi.FullName.Replace(Settings["Path"],"").Replace('\\', '/');
                if (Path.EndsWith(sncExtension))
                {
                    Path = Path.Substring(0, Path.Length - sncExtension.Length);
                }
                contentObject.Path = Path;
                contentObject.Name = Name;
                contents.Add(contentObject);
            }
            return contents;
        }

        public async Task<List<RepoSyncActionResult>> WriteAsync(List<SyncContent> contents)
        {
            List<RepoSyncActionResult> result = new List<RepoSyncActionResult>();
            foreach(var content in contents)
            {
                try
                {
                    //Create folders recursively 
                    IOHelpers.CreateInnerFolders(new DirectoryInfo(Settings["Path"]), content.Path);
                    File.WriteAllText(Settings["Path"] + content.Path.Replace("/",@"\") + ".snc", content.Content2JSON());
                    result.Add(new RepoSyncActionResult() { ContentResult = content, SourceContent = content, FaultReason = null, Success = true });
                }
                catch(Exception ex)
                {
                    result.Add(new RepoSyncActionResult() { ContentResult = content, SourceContent = content, FaultReason = ex, Success = false });
                }
            }
            return result;
        }
    }
}
