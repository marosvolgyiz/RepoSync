using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.ContentExtensions
{
    public class SyncContent
    {
        private int _Id;
        public int Id
        {
            get
            {
                //int Id = 0;
                //if (!Fields.ContainsKey("Id"))
                //{
                //    Fields.Add("Id", 0);
                //}
                //else
                //{
                //    int.TryParse(Fields["Id"].ToString(), out Id);
                //}
                return _Id;
            }
            set
            {
                //if (!Fields.ContainsKey("Id"))
                //{
                //    Fields.Add("Id", value);
                //}
                //else
                //{
                //    Fields["Id"] = value;
                //}
                _Id = value;
            }
        }
        private string _Name;
        public string Name
        {
            get
            {
                //string Name = string.Empty;
                //if (!Fields.ContainsKey("Name"))
                //{
                //    Fields.Add("Name", string.Empty);
                //}
                //else
                //{
                //    Path = Fields["Name"].ToString();
                //}
                return _Name;
            }
            set
            {
                //if (!Fields.ContainsKey("Name"))
                //{
                //    Fields.Add("Name", value);
                //}
                //else
                //{
                //    Fields["Name"] = value;
                //}
                _Name = value;
            }
        }
        private string _Path;
        public string Path
        {
            get
            {
                string Path = string.Empty;
                //if (!Fields.ContainsKey("Path"))
                //{
                //    Fields.Add("Path", string.Empty);
                //}
                //else
                //{
                //    Path = Fields["Path"].ToString();
                //}
                return _Path;
            }
            set
            {
                _Path = value;
                //if (!Fields.ContainsKey("Path"))
                //{
                //    Fields.Add("Path", value);
                //}
                //else
                //{
                //    Fields["Path"] = value;
                //}
            }
        }
        public Dictionary<string, object> Fields { get; set; }
        public Dictionary<string, string> Permissions { get; set; }
        public SyncContent()
        {
            Fields = new Dictionary<string, object>();
            Permissions = new Dictionary<string, string>();
        }
    }
}
