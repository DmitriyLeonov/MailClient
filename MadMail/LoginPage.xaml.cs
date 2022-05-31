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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            SettingsPage registerWindow = new SettingsPage();
            this.NavigationService.Navigate(registerWindow);
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = UsernameTB.Text.Trim();
            string password = PasswordTB.Text.Trim();
            if (login.Length < 5)
            {
                UsernameTB.ToolTip = "Это поле введено некорректно!";
                UsernameTB.Background = Brushes.DarkRed;
            }
            else if (password.Length < 5)
            {
                PasswordTB.ToolTip = "Это поле введено некорректно!";
                PasswordTB.Background = Brushes.DarkRed;
                MessageBox.Show("Минимальная длинна пароля 5 символов");
            }
            else if (login.Length < 5 || !login.Contains("@mymail.com") || login.Contains(","))
            {
                UsernameTB.ToolTip = "Это поле введено некорректно!";
                UsernameTB.Background = Brushes.DarkRed;
            }
            else
            {
                UsernameTB.ToolTip = "";
                UsernameTB.Background = Brushes.Transparent;
                PasswordTB.ToolTip = "";
                PasswordTB.Background = Brushes.Transparent;

                User authUser = null;
                using (AppContext appContext = new AppContext())
                {
                    authUser = appContext.Users.Where(b => b.MailAddres == login && b.Password == password).FirstOrDefault();
                }

                if (authUser != null)
                {
                    App.Current.Properties["CurrentUser"] = login;
                    UserPage userPage = new UserPage();
                    this.NavigationService.Navigate(userPage);
                }
                else
                {
                    MessageBox.Show("НЕПРАВИЛЬНО ВВЕДЁН ЛОГИН ИЛИ ПАРОЛЬ");
                }
            }
        }
    }
}
