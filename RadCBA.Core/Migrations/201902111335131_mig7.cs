namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig7 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CustomerAccounts", new[] { "ServicingAccountID" });
            AlterColumn("dbo.CustomerAccounts", "ServicingAccountID", c => c.Int());
            CreateIndex("dbo.CustomerAccounts", "ServicingAccountID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CustomerAccounts", new[] { "ServicingAccountID" });
            AlterColumn("dbo.CustomerAccounts", "ServicingAccountID", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerAccounts", "ServicingAccountID");
        }
    }
}
