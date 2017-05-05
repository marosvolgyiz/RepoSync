﻿using SenseNet.Client;

namespace RepoSync
{
    public class RepoSyncCompareResult : RepoSyncResult
    {
        /// <summary>
        /// Indicates if the action has succeeded
        /// </summary>
        public bool IsDifferent => SourceContent == TargetContent;
    }
}
