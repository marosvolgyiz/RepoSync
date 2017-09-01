using RepoSync.WPFApp.Code.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace RepoSync.WPFApp.Code
{
    class ReposyncWPFAppSettings
    {
 
        public List<RepoSyncDrive> Drives { get; set; }
        public void SaveSetting()
        {
            string jsonSetting = JsonConvert.SerializeObject(this);
            File.WriteAllText(jsonSetting, ".\\ReposyncWPFAppSettings.json");
        }
        public static ReposyncWPFAppSettings LoadSetting()
        {
            string settingsJson = File.ReadAllText(".\\ReposyncWPFAppSettings.json");
            return JsonConvert.DeserializeObject<ReposyncWPFAppSettings>(settingsJson);
        }
       
        public static RepoSyncDrive LeftDrive { get; set; }
        public static RepoSyncDrive RightDrive { get; set; }
    }
}
