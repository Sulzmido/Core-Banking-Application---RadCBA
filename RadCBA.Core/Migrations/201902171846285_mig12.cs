namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GlPostings", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.TellerPostings", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TellerPostings", "Status");
            DropColumn("dbo.GlPostings", "Status");
        }
    }
}
