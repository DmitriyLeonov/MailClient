using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MadMail
{
    public class Mail : INotifyPropertyChanged
    {
        private string sender;
        private string recieve;
        private string subject;
        private string text;
        private DateTime sentDate;

        private bool isRead;
        private string attacmentLink;

        public int id { get; set; }
        public string Sender { get => sender;
            set { 
                sender = value;
                NotifyPropertyChanged();
            } }
        public string Recieve
        {
            get => recieve;
            set
            {
                recieve = value;
                NotifyPropertyChanged();
            }
        }
        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                NotifyPropertyChanged();
            }
        }
        public string Text {
            get => text;
            set
            {
                text = value;
                NotifyPropertyChanged();
            }
        }
        public DateTime SentDate {
            get => sentDate;
            set
            {
                sentDate = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsRead {
            get => isRead;
            set
            {
                isRead = value;
                NotifyPropertyChanged();
            }
        }
        public string AttacmentLink {
            get => attacmentLink;
            set
            {
                attacmentLink = value;
                NotifyPropertyChanged();
            }
        }

        public Mail(string sender, string recieve, string subject, string text, string attachment)
        {           
            this.Sender = sender;
            this.Recieve = recieve;
            this.Subject = subject;
            this.Text = text;
            this.AttacmentLink = attachment;
            this.IsRead = false;
            this.SentDate = DateTime.Now;
        }

        public Mail() { }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
