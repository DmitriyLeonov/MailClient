namespace MadMail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "SentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "SentDate");
        }
    }
}
