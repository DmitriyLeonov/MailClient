using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace MadMail
{
    /// <summary>
    /// Interaction logic for MailWindow.xaml
    /// </summary>
    public partial class MailWindow : Window
    {
        private Mail windowMail;
        AppContext context;
        public MailWindow(Mail mail)
        {
            InitializeComponent();
            if(mail.AttacmentLink.Length < 3)
            {
                Btn.IsEnabled = false;
            }
            else
            {
                Btn.IsEnabled = true;
            }
            windowMail = mail;
            Sender.Content = windowMail.Sender;
            Subject.Content = windowMail.Subject;
            Text.Text = windowMail.Text;
            context = new AppContext();
            var reader = context.Mails.Where(c => c.id == mail.id).FirstOrDefault();
            reader.IsRead = true;
            context.SaveChanges();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(windowMail.AttacmentLink);
        }
    }
}
