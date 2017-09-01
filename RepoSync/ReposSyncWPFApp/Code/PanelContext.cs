using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.WPFApp.Code
{
    class PanelContext
    {
        public SidesEnum Side { get; set; }
        public Settings.RepoSyncDrive Drive { get; set; }
        public RepoSync.IRepoSyncProvider ProviderInstance { get; set; }
        public bool Connected { get; set; }
        public string CurrentPath { get; set; }
    }
    
}
