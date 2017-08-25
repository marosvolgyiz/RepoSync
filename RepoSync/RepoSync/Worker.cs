using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using RepoSync.ContentExtensions;

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
        private readonly TSourceProvider _sourceProvider;

        /// <summary>
        /// The target provider instance
        /// </summary>
        private readonly TTargetProvider _targetProvider;

        /// <summary>
        /// The type of the action
        /// </summary>
        private readonly ActionType _actionType;

        /// <summary>
        /// The list of fields to compare / sync
        /// </summary>
        public readonly ReadOnlyCollection<string> FieldList;

        /// <summary>
        /// The behavior of the specified fields
        /// </summary>
        public readonly FieldNameBehavior FieldNameBehavior;

        /// <summary>
        /// Public endpoint for running a synchronization job.
        /// </summary>
        public async Task<IEnumerable<RepoSyncResult>> Run()
        {
            await _sourceProvider.ReadPathsAsync();
            var contents = await _sourceProvider.ReadAsync();

            if (_actionType == ActionType.Compare)
            {
                return await RunCompare(contents);
            }
            else
            {
                return await RunSync(contents);
            }

        }

        private async Task<IEnumerable<RepoSyncCompareResult>> RunCompare(List<SyncContent> sourceContents)
        {
            var diff = new List<RepoSyncCompareResult>();
            
            //ToDo: Improve this, figure out if can be parallelized
            foreach (var sourceContent in sourceContents)
            {
                var targetContent = await _targetProvider.LoadAsync(sourceContent.Path);

                diff.Add(new RepoSyncCompareResult
                {
                    IsDifferent = !sourceContent.CompareTo(targetContent, FieldList?.ToList(), FieldNameBehavior),
                    SourceContent = sourceContent,
                    TargetContent = targetContent
                });
            }
            return diff;
        }

        private async Task<IEnumerable<RepoSyncActionResult>> RunSync(List<SyncContent> contents)
        {
            return await _targetProvider.WriteAsync(contents);
        }

        /// <summary>
        /// Protected constructor for the Worker Instance
        /// </summary>
        /// <param name="sourceProvider">The source provider instance</param>
        /// <param name="targetProvider">The target provider instance</param>
        /// <param name="actionType">The action type</param>
        /// <param name="fieldNameBehavior">The behavior for the specified field name list</param>
        /// <param name="fieldList">A list of fields that will act as specified in the behavior property</param>
        protected Worker(TSourceProvider sourceProvider, TTargetProvider targetProvider, ActionType actionType, FieldNameBehavior fieldNameBehavior = FieldNameBehavior.Blacklist, IEnumerable<string> fieldList = null)
        {
            _sourceProvider = sourceProvider;
            _targetProvider = targetProvider;
            _actionType = actionType;
            FieldNameBehavior = fieldNameBehavior;
            FieldList = fieldList?.ToList().AsReadOnly();
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
        /// <param name="fieldNameBehavior">The behavior for the specified field name list</param>
        /// <param name="fieldList">A list of fields that will act as specified in the behavior property</param>
        public Worker(IRepoSyncProvider sourceProvider, IRepoSyncProvider targetProvider, ActionType actionType, FieldNameBehavior fieldNameBehavior = FieldNameBehavior.Blacklist, IEnumerable<string> fieldList = null) : base(sourceProvider, targetProvider, actionType)
        {
        }
    }
}
