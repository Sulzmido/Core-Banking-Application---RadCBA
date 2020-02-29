namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TillToUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        GlAccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GlAccounts", t => t.GlAccountID, cascadeDelete: true)
                .Index(t => t.GlAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TillToUsers", "GlAccountID", "dbo.GlAccounts");
            DropIndex("dbo.TillToUsers", new[] { "GlAccountID" });
            DropTable("dbo.TillToUsers");
        }
    }
}
