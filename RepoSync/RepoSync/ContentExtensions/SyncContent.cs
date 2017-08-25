using System.Collections.Generic;

namespace RepoSync.ContentExtensions
{
    public class SyncContent
    {
        public int Id
        {
            get
            {
                int id = 0;
                if (!Fields.ContainsKey("Id"))
                {
                    Fields.Add("Id", 0);
                }
                else
                {
                    int.TryParse(Fields["Id"].ToString(), out id);
                }
                return id;
            }
            set
            {
                if (!Fields.ContainsKey("Id"))
                {
                    Fields.Add("Id", value);
                }
                else
                {
                    Fields["Id"] = value;
                }
            }
        }
        public string Name
        {
            get
            {
                string name = string.Empty;
                if (!Fields.ContainsKey("Name"))
                {
                    Fields.Add("Name", string.Empty);
                }
                else
                {
                    name = Fields["Name"].ToString();
                }
                return name;
            }
            set
            {
                if (!Fields.ContainsKey("Name"))
                {
                    Fields.Add("Name", value);
                }
                else
                {
                    Fields["Name"] = value;
                }
            }
        }
        public string Path
        {
            get
            {
                string path = string.Empty;
                if (!Fields.ContainsKey("Path"))
                {
                    Fields.Add("Path", string.Empty);
                }
                else
                {
                    path = Fields["Path"]?.ToString();
                }
                return path;
            }
            set
            {
                if (!Fields.ContainsKey("Path"))
                {
                    Fields.Add("Path", value);
                }
                else
                {
                    Fields["Path"] = value;
                }
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
