using RepoSync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SenseNet.Client;

namespace FileSystemProvider
{
    public class FileSystemProvider : IRepoSyncProvider
    {
        public List<string> RequiredOptions => new List<string> { "Path" };
        public Dictionary<string, string> Settings { get; set; }
        public IRepoSyncFilter Filter { get; set; }
        public Task<Content> LoadAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> ReadPathsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Content>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<RepoSyncActionResult>> WriteAsync(List<Content> contents)
        {
            throw new NotImplementedException();
        }
    }
}
