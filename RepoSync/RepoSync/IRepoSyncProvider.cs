using SenseNet.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepoSync.ContentExtensions;
namespace RepoSync
{
    /// <summary>
    /// Interface that represents a RepoSyncProvider
    /// </summary>
    public interface IRepoSyncProvider
    {

        List<string> RequiredSettings { get; }
        /// <summary>
        /// The provider-specific settings for the provider
        /// </summary>
        Dictionary<string, string> Settings { get; set; }

        /// <summary>
        /// The filter whitch is used by ReadPaths() to filter contents
        /// </summary>
        IRepoSyncFilter Filter { get; set; }

        /// <summary>
        /// Loads a subtree of contents from a repository
        /// </summary>
        /// <param name="path">The Repository path</param>
        /// <returns></returns>
        Task<SyncContent> LoadAsync(string path);

        /// <summary>
        /// Read the filtered paths
        /// </summary>
        /// <returns>A list of avaliable paths</returns>
        Task<List<string>> ReadPathsAsync();

        /// <summary>
        /// Reads all contents from ReadPaths()
        /// </summary>
        /// <returns></returns>
        Task<List<SyncContent>> ReadAsync();

        /// <summary>
        /// Writes a list of contents
        /// </summary>
        /// <param name="contents">A list of contents to be written</param>
        /// <returns>The result of writing contents</returns>
        Task<List<RepoSyncActionResult>> WriteAsync(List<SyncContent> contents);
    }
}
