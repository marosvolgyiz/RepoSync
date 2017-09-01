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
        [JsonIgnore]
        public List<RepoSyncDrive> LocalDrives
        {
            get
            {
                List<RepoSyncDrive> result = new List<RepoSyncDrive>();
                var systemDrives = DriveInfo.GetDrives();
                foreach (var item in systemDrives)
                {
                    var drive = new RepoSyncDrive();
                    drive.FA_Icon = "f0a0";
                    drive.Name = item.Name;
                 
                    drive.Provider = "RepoSync.Providers.FileSystemProvider";
                    drive.ProviderArguments.Add("Path", item.RootDirectory.FullName);
                    drive.ProviderArguments.Add("AllDirectories", "False");
                    result.Add(drive);
                }
                return result;
            }
        }
        [JsonIgnore]
        public List<RepoSyncDrive> AllDrives
        {
            get
            {
                var a = new List<RepoSyncDrive>();
                a.AddRange(LocalDrives);
                a.AddRange(Drives);
                return a;
            }
        }


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
