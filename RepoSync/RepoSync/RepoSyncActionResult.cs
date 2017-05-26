using SenseNet.Client;
using System;
using RepoSync.ContentExtensions;
namespace RepoSync
{

    /// <summary>
    /// Represents an action result whitch contains data about a writing of a content
    /// </summary>
    public class RepoSyncActionResult
    {
        /// <summary>
        /// Indicates if the action has succeeded
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// The content whitch has been sent to write
        /// </summary>
        public SyncContent SourceContent { get; set; }

        /// <summary>
        /// The content that has been returned after a write
        /// </summary>
        public SyncContent ContentResult { get; set; }

        /// <summary>
        /// The exception if the write has been failed
        /// </summary>
        public Exception FaultReason { get; set; }

    }
}
