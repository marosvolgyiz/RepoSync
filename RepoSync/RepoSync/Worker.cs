namespace RepoSync
{
    /// <summary>
    /// Generic instance of a Sync Worker
    /// </summary>
    /// <typeparam name="TSourceProvider">The type of the source provider</typeparam>
    /// <typeparam name="TTargetProvider">The type of the target provider</typeparam>
    public class Worker<TSourceProvider, TTargetProvider> where TSourceProvider : IRepoSyncProvider
                                                          where TTargetProvider : IRepoSyncProvider
    {
        /// <summary>
        /// The source provider instance
        /// </summary>
        protected readonly TSourceProvider _sourceProvider;

        /// <summary>
        /// The target provider instance
        /// </summary>
        protected readonly TTargetProvider _targetProvider;

        /// <summary>
        /// The type of the action
        /// </summary>
        protected readonly ActionType _actionType;

        /// <summary>
        /// Protected constructor for the Worker Instance
        /// </summary>
        /// <param name="sourceProvider">The source provider instance</param>
        /// <param name="targetProvider">The target provider instance</param>
        /// <param name="actionType">The action type</param>
        protected Worker(TSourceProvider sourceProvider, TTargetProvider targetProvider, ActionType actionType)
        {
            _sourceProvider = sourceProvider;
            _targetProvider = targetProvider;
            _actionType = actionType;
        }
    }

    public class Worker : Worker<IRepoSyncProvider, IRepoSyncProvider>
    {
        /// <summary>
        /// Protected constructor for the Worker Instance
        /// </summary>
        /// <param name="sourceProvider">The source provider instance</param>
        /// <param name="targetProvider">The target provider instance</param>
        /// <param name="actionType">The action type</param>
        public Worker(IRepoSyncProvider sourceProvider, IRepoSyncProvider targetProvider, ActionType actionType) : base(sourceProvider, targetProvider, actionType)
        {
        }

        /// <summary>
        /// Public endpoint for running a synchronization job.
        /// </summary>
        public void Run()
        {
            // ToDO: Sync method basics
            var contets = _sourceProvider.ReadPathsAsync();

        }
    }
}
