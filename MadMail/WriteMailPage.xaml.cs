using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MadMail
{
    /// <summary>
    /// Interaction logic for WriteMailPage.xaml
    /// </summary>
    public partial class WriteMailPage : Page
    {
        bool receiverTB_has_focus = false;
        bool subjectTB_has_focus = false;
        bool messageTB_has_focus = false;

        string attachmentPath = string.Empty;
        Settings settings = new Settings();
        AppContext db;

        public WriteMailPage()
        {
            InitializeComponent();
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("settings.xml"))
            {
                settings = XmlFileManager.XmlUserDataReader("settings.xml");
            }
        }

        //Textbox focus handler
        private void ReceiverTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if(!receiverTB_has_focus)
            {
                ReceiverTB.Text = string.Empty;
                receiverTB_has_focus = true;
            }
        }

        private void SubjectTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!subjectTB_has_focus)
            {
                SubjectTB.Text = string.Empty;
                subjectTB_has_focus = true;
            }
        }

        private void MessageTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!messageTB_has_focus)
            {
                MessageTB.Text = string.Empty;
                messageTB_has_focus = true;
            }
        }


        //Button event handler
        private void AttachBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            attachmentPath = System.IO.Path.GetFullPath(openFileDialog.FileName);
            AttachFileTB.Text = openFileDialog.SafeFileName;
        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            string reciever = ReceiverTB.Text.Trim();
            try
            {
                if (reciever.Length < 5 || !reciever.Contains("@") || reciever.Contains(","))
                {
                    ReceiverTB.ToolTip = "Это поле введено некорректно!";
                    ReceiverTB.Background = Brushes.DarkRed;
                }
                string currentUser = (string)App.Current.Properties["CurrentUser"];
                if (currentUser.Length == 0)
                {
                    throw new Exception();
                }
                Mail mail = new Mail(currentUser,ReceiverTB.Text.Trim(), SubjectTB.Text.Trim(), MessageTB.Text.Trim(), attachmentPath);
                db = new AppContext();
                db.Mails.Add(mail);
                db.SaveChanges();
                MessageBox.Show("Сообщение отправлено!");
                InitPage();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Сообщение не отправлено" + ex.Message);
            }
        }

        private void InitPage()
        {
            receiverTB_has_focus = false;
            subjectTB_has_focus = false;
            ReceiverTB.Text = "Email@example.com...";
            SubjectTB.Text = "Тема...";
            AttachFileTB.Text = "Выберите файл...";
            MessageTB.Text = "Текст...";
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            InitPage();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            attachmentPath = string.Empty;
            AttachFileTB.Text = "Выберите файл...";
        }

        
    }
}
