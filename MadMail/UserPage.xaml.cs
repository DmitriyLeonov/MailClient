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

namespace MadMail
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            user.Content = (string)App.Current.Properties["CurrentUser"];
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Properties["CurrentUser"] = "";
            SettingsPage registerWindow = new SettingsPage();
            this.NavigationService.Navigate(registerWindow);
        }
    }
}
