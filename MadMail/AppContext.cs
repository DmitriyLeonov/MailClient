using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadMail
{
    internal class AppContext:DbContext
    {
        public AppContext() : base("AppDataBase") 
        {
            Database.SetInitializer<AppContext>(new CreateDatabaseIfNotExists<AppContext>());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Mail> Mails { get; set; }
    }
}
