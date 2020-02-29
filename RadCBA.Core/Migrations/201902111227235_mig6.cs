namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAccounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Long(nullable: false),
                        AccountName = c.String(nullable: false, maxLength: 40),
                        BranchID = c.Int(nullable: false),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DaysCount = c.Int(nullable: false),
                        dailyInterestAccrued = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanInterestRatePerMonth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountType = c.Int(nullable: false),
                        AccountStatus = c.Int(nullable: false),
                        SavingsWithdrawalCount = c.Int(nullable: false),
                        CurrentLien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerID = c.Int(nullable: false),
                        LoanAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanMonthlyRepay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanMonthlyInterestRepay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanMonthlyPrincipalRepay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanPrincipalRemaining = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TermsOfLoan = c.Int(),
                        ServicingAccountID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.CustomerAccounts", t => t.ServicingAccountID)
                .Index(t => t.BranchID)
                .Index(t => t.CustomerID)
                .Index(t => t.ServicingAccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccounts", "ServicingAccountID", "dbo.CustomerAccounts");
            DropForeignKey("dbo.CustomerAccounts", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.CustomerAccounts", "BranchID", "dbo.Branches");
            DropIndex("dbo.CustomerAccounts", new[] { "ServicingAccountID" });
            DropIndex("dbo.CustomerAccounts", new[] { "CustomerID" });
            DropIndex("dbo.CustomerAccounts", new[] { "BranchID" });
            DropTable("dbo.CustomerAccounts");
        }
    }
}
