using RepoSync.ContentExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RepoSync.WPFApp.Controls
{
    public class SyncContentBasedTreeViewItem:TreeViewItem
    {
        public SyncContent syncContent { get; set; }
    }
}
