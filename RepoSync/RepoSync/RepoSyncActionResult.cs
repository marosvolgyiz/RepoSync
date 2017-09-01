using RepoSync.ContentExtensions;
namespace RepoSync
{
    /// <summary>
    /// Represents an action result whitch contains data about a writing of a content
    /// </summary>
    public class RepoSyncActionResult : RepoSyncResult
    {
        /// <summary>
        /// The content that has been returned after a write
        /// </summary>
        public SyncContent ContentResult { get; set; }
    }
}
