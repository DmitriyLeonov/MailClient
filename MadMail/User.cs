using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadMail
{
    internal class User
    {
        public int id { get; set; }
        public string MailAddres { get; set; }
        public string Password { get; set; }


        public User(string adress, string password)
        {
            this.MailAddres = adress;
            this.Password = password;
        }

        public User() { }
    }
}
