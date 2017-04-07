namespace RepoSync
{
    /// <summary>
    /// Interface for parsing provider-specific settings to IRepoSyncProviders
    /// </summary>
    public interface IRepoSyncProviderSettings
    {
        string UserName { get; set; }
        string Path { get; set; }
    }
}
