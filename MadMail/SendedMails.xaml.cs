using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SendedMails.xaml
    /// </summary>
    
    public partial class SendedMails : Page
    {
        AppContext db;
        private ObservableCollection<Mail> mails = new ObservableCollection<Mail>();
        public SendedMails()
        {
            InitializeComponent();
            db = new AppContext();
            string currentUser = (string)App.Current.Properties["CurrentUser"];
            List<Mail> messages = new List<Mail>();
            messages = db.Mails.Where(x => x.Sender == currentUser).ToList();
            foreach (var mail in messages)
            {
                mails.Add(mail);
            }
            SortOrder.ItemsSource = new string[] { "Сначала старые", "Сначала новые" };
            readedListView.ItemsSource = mails;
            SortOrder.SelectionChanged += SelectionChanged;
            readedListView.Items.SortDescriptions.Add(new SortDescription("SentDate", ListSortDirection.Ascending));
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortList();
        }

        public void SortList()
        {
            var SortDirection = SortOrder.SelectedItem.ToString() == "Сначала старые" ?
                ListSortDirection.Ascending : ListSortDirection.Descending;
            readedListView.Items.SortDescriptions[0] = new SortDescription("SentDate", SortDirection);
        }

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Mail mail = (Mail)readedListView.SelectedItem;
            MailWindow mailWindow = new MailWindow(mail);
            mailWindow.Show();
        }
    }
}
