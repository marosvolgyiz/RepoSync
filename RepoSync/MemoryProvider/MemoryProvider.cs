using RepoSync;
using RepoSync.ContentExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoSync.Providers.MemoryProvider
{
    public class MemoryProvider : IRepoSyncProvider
    {
        private readonly List<SyncContent> _repository = new List<SyncContent>();
        public void AddContent(SyncContent content)
        {
            _repository.Add(content);
        }
        public IRepoSyncFilter Filter
        {
            get; set;
        }

        public List<string> RequiredSettings => new List<string> { };
        public Dictionary<string, string> Settings { get; set; }

        public async Task<SyncContent> LoadAsync(string path)
        {
            return _repository.SingleOrDefault(c => c.Path == path);
        }

        public async Task<List<SyncContent>> ReadAsync()
        {
            var paths = await ReadPathsAsync();
            return _repository.Where(c => paths.Contains(c.Path)).ToList();
        }

        public async Task<List<string>> ReadPathsAsync()
        {
            return _repository.Select(c => c.Path).ToList();
        }

        public async Task<List<RepoSyncActionResult>> WriteAsync(List<SyncContent> contents)
        {
            List<RepoSyncActionResult> result = new List<RepoSyncActionResult>();
            foreach (var item in contents)
            {
                this.AddContent(item);
                result.Add(new RepoSyncActionResult() { ContentResult = item, SourceContent = item, Success = true });
            }
            return result;
        }
    }
}
