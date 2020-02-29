namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustId = c.String(),
                        FullName = c.String(nullable: false, maxLength: 40),
                        Address = c.String(nullable: false, maxLength: 100),
                        Email = c.String(maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 16),
                        Gender = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
