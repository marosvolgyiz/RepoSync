using RepoSync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SenseNet.Client;
using System.IO;
using System.Linq;

namespace RepoSync.Providers.FileSystemProvider
{
    public class FileSystemProvider : IRepoSyncProvider
    {
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
        public async Task<Content> LoadAsync(string path)
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

        public async Task<List<Content>> ReadAsync()
        {
            List<Content> contents = new List<Content>();
            List<string> allfiles = await ReadPathsAsync();
            foreach (var item in allfiles)
            {
                var fi = new FileInfo(item);
                string sncExtension = ".snc";
                if (fi.FullName.EndsWith(sncExtension))
                {
                    //TODO: Find binary 
                    var binaryFi = new FileInfo(fi.FullName.Substring(0, fi.FullName.Length - sncExtension.Length));
                    if (binaryFi == null)
                    {
                        //Content does not have binary

                    }
                    else
                    {
                        //Content does have binary

                    }
                    //TODO: check the contents contains this item before add to contents list
                
                }
                else
                {

                }
                //TODO: Read .snc
                //TODO: Create Content object
                //TODO: Fill all fields
            }
            return contents;
        }

        public async Task<List<RepoSyncActionResult>> WriteAsync(List<Content> contents)
        {
            throw new NotImplementedException();
        }
    }
}
