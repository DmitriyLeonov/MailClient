using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace MadMail
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        Settings settings = new Settings();
        AppContext db;

        public SettingsPage()
        {
            InitializeComponent();
            db = new AppContext();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void InitSettings()
        {
            if (File.Exists("settings.xml"))
            {
                settings = XmlFileManager.XmlUserDataReader("settings.xml");
                UsernameTB.Text = settings.Username.Trim();
                PasswordTB.Text = settings.Password.Trim();
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = UsernameTB.Text.Trim();
            string password = PasswordTB.Text.Trim();
            string pass2 = Password2TB.Text.Trim();
            if (login.Length < 5)
            {
                UsernameTB.ToolTip = "Это поле введено некорректно!";
                UsernameTB.Background = Brushes.DarkRed;
            } else if (password.Length < 5)
            {
                PasswordTB.ToolTip = "Это поле введено некорректно!";
                PasswordTB.Background = Brushes.DarkRed;
                MessageBox.Show("Минимальная длинна пароля 5 символов");
            } else if (password != pass2)
            {
                Password2TB.ToolTip = "Это поле введено некорректно!";
                Password2TB.Background = Brushes.DarkRed;
            } else if (login.Length < 5 || !login.Contains("@mymail.com") || login.Contains(","))
            {
                UsernameTB.ToolTip = "Это поле введено некорректно!";
                UsernameTB.Background = Brushes.DarkRed;
            }
            else
            {
                User user = null;
                user = db.Users.Where(b => b.MailAddres == login).FirstOrDefault();
                if (user != null)
                {
                    MessageBox.Show("Пользователь уже существует");
                }
                else
                {
                    UsernameTB.ToolTip = "";
                    UsernameTB.Background = Brushes.Beige;
                    PasswordTB.ToolTip = "";
                    PasswordTB.Background = Brushes.Beige;
                    Password2TB.ToolTip = "";
                    Password2TB.Background = Brushes.Beige;
                    db.Users.Add(new User(login, password));
                    db.SaveChanges();
                    App.Current.Properties["CurrentUser"] = login;
                    UserPage userPage = new UserPage();
                    this.NavigationService.Navigate(userPage);
                }
            }
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginWindow = new LoginPage();
            this.NavigationService.Navigate(loginWindow);
        }
    }
}
