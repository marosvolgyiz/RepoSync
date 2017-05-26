using System;
using RepoSync.ContentExtensions;

namespace RepoSync
{
    public class RepoSyncResult
    {
        /// <summary>
        /// Indicates if the action has succeeded
        /// </summary>
        public bool Success => FaultReason == null;

        /// <summary>
        /// The content whitch has been sent to write
        /// </summary>
        public SyncContent SourceContent { get; set; }

        /// <summary>
        /// The content that has been returned after a write
        /// </summary>
        public SyncContent TargetContent { get; set; }

        /// <summary>
        /// The exception if the write has been failed
        /// </summary>
        public Exception FaultReason { get; set; }
    }
}
