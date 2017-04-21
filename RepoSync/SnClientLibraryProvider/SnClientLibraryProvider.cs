using System.Collections.Generic;
using System.Threading.Tasks;
using RepoSync;
using SenseNet.Client;

namespace SnClientLibraryProvider
{
    public class SnClientLibraryProvider : IRepoSyncProvider
    {
        public List<string> RequiredOptions => new List<string> { };
        public Dictionary<string, string> Settings { get; set; }
        public IRepoSyncFilter Filter { get; set; }
        public Task<Content> LoadAsync(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<string>> ReadPathsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Content>> ReadAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<RepoSyncActionResult>> WriteAsync(List<Content> contents)
        {
            throw new System.NotImplementedException();
        }
    }
}
