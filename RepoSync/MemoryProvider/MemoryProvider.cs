using RepoSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.Providers.MemoryProvider
{
    public class MemoryProvider : IRepoSyncProvider
    {
        private List<SenseNet.Client.Content> Repository = new List<SenseNet.Client.Content>();
        public void AddContent(SenseNet.Client.Content content)
        {
            Repository.Add(content);
        }
        public IRepoSyncFilter Filter
        {
            get;set;
        }

        public IRepoSyncProviderSettings Settings
        {
            get; set;
        }

        public async Task<SenseNet.Client.Content> LoadAsync(string Path)
        {
            return Repository.SingleOrDefault(c => c.Path == Path);
        }

        public async Task<List<SenseNet.Client.Content>> ReadAsync()
        {
            var paths = await ReadPathsAsync();
            return Repository.Where(c => paths.Contains(c.Path)).ToList();
        }

        public async Task<List<string>> ReadPathsAsync()
        {
            return Repository.Select(c => c.Path).ToList();
        }

        public async Task< List<RepoSyncActionResult>> WriteAsync(List<SenseNet.Client.Content> contents)
        {
            List<RepoSyncActionResult> result = new List<RepoSyncActionResult>();
            foreach (var item in contents)
            {
                this.AddContent(item);
                result.Add(new RepoSyncActionResult() { ContentResult = item, SourceContent = item, Success = true});
            }
            return result;
        }
    }
}
