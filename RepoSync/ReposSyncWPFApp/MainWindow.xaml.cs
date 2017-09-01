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
            
            foreach (var drive in Settings.Drives)
            {
                int i = Settings.Drives.IndexOf(drive);
                DriveButton b = new DriveButton();
                b.Drive = drive;
                b.Name = "LeftButton_" + i.ToString();
                b.MouseDown += B_Click;

                DriveButton b1 = new DriveButton();
                b1.Drive = drive;
                b1.Name = "RightButton_" + i.ToString();
                b1.MouseDown += B_Click;

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
                Code.Settings.RepoSyncDrive drive = button.Content as Code.Settings.RepoSyncDrive;
                if (button.Name.ToLower().StartsWith("left"))
                {
                    Code.ReposyncWPFAppSettings.LeftDrive = drive;
                }
                else
                {
                    Code.ReposyncWPFAppSettings.RightDrive = drive;
                }

                //change state
                if(button.Parent !=null && 
                    button.Parent is Grid && 
                    (button.Parent as Grid).Parent !=null && 
                    (button.Parent as Grid).Parent is DriveButton)
                {
                    DriveButton db = (button.Parent as Grid).Parent as DriveButton;
                    db.Connected = !db.Connected;
                }
                

            }
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
