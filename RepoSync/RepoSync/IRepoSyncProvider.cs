using SenseNet.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoSync
{
    /// <summary>
    /// Interface that represents a RepoSyncProvider
    /// </summary>
    public interface IRepoSyncProvider
    {
        /// <summary>
        /// The settings for the provider
        /// </summary>
        IRepoSyncProviderSettings Settings { get; set; }
        
        /// <summary>
        /// The filter whitch is used by ReadPaths() to filter contents
        /// </summary>
        IRepoSyncFilter Filter { get; set; }

        /// <summary>
        /// Loads a subtree of contents from a repository
        /// </summary>
        /// <param name="Path">The Repository path</param>
        /// <returns></returns>
        Task<Content> LoadAsync(string Path);
        
        /// <summary>
        /// Read the filtered paths
        /// </summary>
        /// <returns>A list of avaliable paths</returns>
        Task<List<string>> ReadPathsAsync();
        
        /// <summary>
        /// Reads all contents from ReadPaths()
        /// </summary>
        /// <returns></returns>
        Task<List<Content>> ReadAsync();

        /// <summary>
        /// Writes a list of contents
        /// </summary>
        /// <param name="contetns">A list of contents to be written</param>
        /// <returns>The result of writing contents</returns>
        List<RepoSyncActionResult> WriteAsync(List<Content> contetns);

    }
}
