namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TellerPostings", "TillAccountID", c => c.Int());
            CreateIndex("dbo.TellerPostings", "TillAccountID");
            AddForeignKey("dbo.TellerPostings", "TillAccountID", "dbo.GlAccounts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPostings", "TillAccountID", "dbo.GlAccounts");
            DropIndex("dbo.TellerPostings", new[] { "TillAccountID" });
            DropColumn("dbo.TellerPostings", "TillAccountID");
        }
    }
}
