namespace MadMail.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "IsRead", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "IsRead");
        }
    }
}
