using RepoSync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SenseNet.Client;

namespace RepoSync.Providers.FileSystemProvider
{
    public class FileSystemProvider : IRepoSyncProvider
    {
        public List<string> RequiredSettings => new List<string> { "Path" };
        public Dictionary<string, string> Settings { get; set; }
        public IRepoSyncFilter Filter { get; set; }
        public async Task<Content> LoadAsync(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<List<string>> ReadPathsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Content>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RepoSyncActionResult>> WriteAsync(List<Content> contents)
        {
            throw new NotImplementedException();
        }
    }
}
