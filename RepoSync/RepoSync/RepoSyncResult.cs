using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SenseNet.Client;

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
        public Content SourceContent { get; set; }

        /// <summary>
        /// The content that has been returned after a write
        /// </summary>
        public Content TargetContent { get; set; }

        /// <summary>
        /// The exception if the write has been failed
        /// </summary>
        public Exception FaultReason { get; set; }
    }
}
