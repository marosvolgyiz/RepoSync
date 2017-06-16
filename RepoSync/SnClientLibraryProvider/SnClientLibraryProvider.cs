using System.Collections.Generic;
using System.Threading.Tasks;
using RepoSync;
using SenseNet.Client;
using RepoSync.ContentExtensions;
using System;

namespace RepoSync.Providers.SnClientLibraryProvider
{
    public class SnClientLibraryProvider : IRepoSyncProvider
    {
        public List<string> RequiredSettings => new List<string> { };
        public Dictionary<string, string> Settings { get; set; }
        public IRepoSyncFilter Filter { get; set; }
        public Task<SyncContent> LoadAsync(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> ReadPathsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<SyncContent>> ReadAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<RepoSyncActionResult>> WriteAsync(List<SyncContent> contents)
        {
            throw new NotImplementedException();
        }
    }
}
