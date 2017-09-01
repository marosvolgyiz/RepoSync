using System;
using System.Collections.Generic;
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

namespace RepoSync.WPFApp.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DriveButton : UserControl
    {
        Code.Settings.RepoSyncDrive _Drive;
        public Code.Settings.RepoSyncDrive Drive {
            get
            { 
                return _Drive;
            }
            set
            {
                _Drive = value;
                DriveCaption.Content = value.ToString();
                tbFontAwesome.Text = Convert.ToChar(int.Parse(value.FA_Icon, System.Globalization.NumberStyles.HexNumber)).ToString();
            }
        }
        private bool _Connected;
        public bool Connected { get
            {
                return _Connected;
            }
            set
            {
                _Connected = value;
                if (_Connected)
                {
                    //2ECC71
                    StatusLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2ECC71"));
                    StatusLabel.ToolTip = "Connected";
                    StatusLabel.Content = "Connected";
                }
                else
                {
                    // D91E18
                    StatusLabel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D91E18"));
                    StatusLabel.ToolTip = "Disconnected";
                    StatusLabel.Content = "Disconnected";
                }
            }
        }
       
        public DriveButton()

        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5522A7F0"));
            this.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3498DB"));
            //this.BorderThickness = new Thickness(1);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0022A7F0"));
            this.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#003498DB"));
            //this.BorderThickness = new Thickness(0);
             
        }
    }
}
