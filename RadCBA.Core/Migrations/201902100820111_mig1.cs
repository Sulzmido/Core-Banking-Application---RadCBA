namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.GlCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Description = c.String(nullable: false, maxLength: 150),
                        MainCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            
            
           
            
        }
        
        public override void Down()
        {


        }
    }
}
