using RepoSync.WPFApp.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RepoSync.WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Code.ReposyncWPFAppSettings Settings = null;
        public MainWindow()
        {
            InitializeComponent();
            //listBox.DataContext = new ProviderFactory();
            Settings = Code.ReposyncWPFAppSettings.LoadSetting();
            var allDrives = Settings.AllDrives;
            foreach (var drive in allDrives)
            {
                int i = allDrives.IndexOf(drive);
                DriveButton b = new DriveButton();
                b.Drive = drive;
                b.Name = "LeftButton_" + i.ToString();
                b.MouseDown += B_Click;
                b.Height = 48;

                DriveButton b1 = new DriveButton();
                b1.Drive = drive;
                b1.Name = "RightButton_" + i.ToString();
                b1.MouseDown += B_Click;
                b1.Height = 48;
                LeftButtonStripPanel.Children.Add(b);
                RightButtonStripPanel.Children.Add(b1);
            }

            AppProperties.SelectedObject = Settings;           
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            if(sender is DriveButton)
            {
                var button = (sender as DriveButton);
                Code.Settings.RepoSyncDrive drive = button.Drive;
                if (button.Name.ToLower().StartsWith("left"))
                {
                    Code.ReposyncWPFAppSettings.LeftDrive = drive;
                    RerfreshTree(LeftTreeView, Code.ReposyncWPFAppSettings.LeftDrive);
                }
                else
                {
                    Code.ReposyncWPFAppSettings.RightDrive = drive;
                    RerfreshTree(RightTreeView, Code.ReposyncWPFAppSettings.LeftDrive);

                }

                //change state
                if (button.Parent !=null && 
                    button.Parent is Grid && 
                    (button.Parent as Grid).Parent !=null && 
                    (button.Parent as Grid).Parent is DriveButton)
                {
                    DriveButton db = (button.Parent as Grid).Parent as DriveButton;
                    db.Connected = !db.Connected;
                }
                

            }
        }
        private void Ti_Click(object sender, MouseButtonEventArgs e)
        {
            if(sender is SyncContentBasedTreeViewItem)
            {
                var twi = sender as SyncContentBasedTreeViewItem;
                if (twi.syncContent == null)
                {
                    Code.ReposyncWPFAppSettings.LeftDrive.ProviderArguments["Path"] = ""; //Todo get parent
                    RerfreshTree(LeftTreeView, Code.ReposyncWPFAppSettings.LeftDrive);
                }
                else
                {
                    var name = twi.Header.ToString().Split(new char[] { ' ' })[1];

                    //  Code.ReposyncWPFAppSettings.LeftDrive.ProviderArguments["Path"] = Code.ReposyncWPFAppSettings.LeftDrive.ProviderArguments["Path"]+"\\"+ name; //Todo get path
                    RerfreshTree(LeftTreeView, Code.ReposyncWPFAppSettings.LeftDrive, Code.ReposyncWPFAppSettings.LeftDrive.ProviderArguments["Path"] + twi.syncContent.Path);// Code.ReposyncWPFAppSettings.LeftDrive.ProviderArguments["Path"] + "\\" + name);
                }
            }
        }
        private async void RerfreshTree(TreeView tv, Code.Settings.RepoSyncDrive drive, string contextPath = null)
        {
            var providerPath = drive.ProviderArguments["Path"].ToString();
            var provider = RepoSync.ProviderFactory.Create(drive.Provider, drive.ProviderArguments );
            if (contextPath != null)
            {
                provider.Settings["Path"]=contextPath;
            }
            List<ContentExtensions.SyncContent> t = await provider.ReadAsync();
            tv.Items.Clear();
            if(contextPath!= null &&  providerPath != contextPath)
            {
                SyncContentBasedTreeViewItem upperTi = new SyncContentBasedTreeViewItem();
                upperTi.Header = "...";
                upperTi.MouseDoubleClick += Ti_Click;
                tv.Items.Add(upperTi);
            }
         
            
            foreach (var item in t.OrderByDescending(n => n.Name).OrderByDescending(n=>n.Fields["ContentType"]))
            {
                SyncContentBasedTreeViewItem ti = new SyncContentBasedTreeViewItem();
                ti.Header ="["+item.Fields["ContentType"].ToString()+"] "+ item.Name;
                ti.MouseDoubleClick += Ti_Click;
                ti.syncContent = item;
                tv.Items.Add(ti);
            }

        }


        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
