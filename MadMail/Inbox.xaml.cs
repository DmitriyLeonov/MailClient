using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Page
    {
        AppContext db;
        private ObservableCollection<Mail> mails = new ObservableCollection<Mail>();
        private int messagesCount = 0;
        public Inbox()
        {
            InitializeComponent();
            db = new AppContext();
            
            string currentUser = (string)App.Current.Properties["CurrentUser"];
            List<Mail> messages = new List<Mail>();
            messages = db.Mails.Where(x => x.Recieve == currentUser).ToList();
            foreach (var mail in messages)
            {
                mails.Add(mail);
                if (messagesCount < messages.Count && mail.IsRead == false)
                {
                    NotificationShow(mail.Sender);
                }
            }
            messagesCount = mails.Count;
            Sort.ItemsSource = new string[] { "Сначала старые", "Сначала новые" };
            myListView.ItemsSource = mails;
            Sort.SelectionChanged += SelectionChanged;
            myListView.Items.SortDescriptions.Add(new SortDescription("SentDate", ListSortDirection.Ascending));
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortList();
        }

        public void SortList()
        {
            var SortDirection = Sort.SelectedItem.ToString() == "Сначала старые" ?
                ListSortDirection.Ascending : ListSortDirection.Descending;
            myListView.Items.SortDescriptions[0] = new SortDescription("SentDate", SortDirection);
        }
        private static void NotificationShow(string sender)
        {
            var notify = new ToastContentBuilder();
            notify.AddText("Получео письмо от " + sender);
            notify.Show();
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mail mail = (Mail)myListView.SelectedItem;
            MailWindow mailWindow = new MailWindow(mail);
            mailWindow.Show();
        }
    }
}
