using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.WPFApp.Code.Settings
{
    public class RepoSyncDrive
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Provider { get; set; }
        public string FA_Icon { get; set; }
        public List<KeyValuePair<string,string>> ProviderArguments { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
