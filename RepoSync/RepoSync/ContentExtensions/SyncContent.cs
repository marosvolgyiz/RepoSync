using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.ContentExtensions
{
    public class SyncContent
    {
        public Dictionary<string, object> Fields { get; set; }
        public Dictionary<string, string> Permissions { get; set; }
        public SyncContent()
        {
            Fields = new Dictionary<string, object>();
            Permissions = new Dictionary<string, string>();
        }
    }
}
