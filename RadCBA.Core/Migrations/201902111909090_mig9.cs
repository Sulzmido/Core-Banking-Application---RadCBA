namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TellerPostings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Narration = c.String(),
                        Date = c.DateTime(nullable: false),
                        PostingType = c.Int(nullable: false),
                        CustomerAccountID = c.Int(nullable: false),
                        PostInitiatorId = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CustomerAccounts", t => t.CustomerAccountID, cascadeDelete: true)
                .Index(t => t.CustomerAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TellerPostings", "CustomerAccountID", "dbo.CustomerAccounts");
            DropIndex("dbo.TellerPostings", new[] { "CustomerAccountID" });
            DropTable("dbo.TellerPostings");
        }
    }
}
