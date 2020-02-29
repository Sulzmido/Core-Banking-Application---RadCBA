namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountConfigurations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsBusinessOpen = c.Boolean(nullable: false),
                        FinancialDate = c.DateTime(nullable: false),
                        SavingsCreditInterestRate = c.Double(),
                        SavingsMinimumBalance = c.Decimal(precision: 18, scale: 2),
                        SavingsInterestExpenseGlID = c.Int(),
                        SavingsInterestPayableGlID = c.Int(),
                        CurrentCreditInterestRate = c.Double(),
                        CurrentMinimumBalance = c.Decimal(precision: 18, scale: 2),
                        CurrentCot = c.Decimal(precision: 18, scale: 2),
                        CurrentInterestExpenseGlID = c.Int(),
                        CurrentCotIncomeGlID = c.Int(),
                        LoanDebitInterestRate = c.Double(),
                        LoanInterestIncomeGlID = c.Int(),
                        LoanInterestExpenseGLID = c.Int(),
                        LoanInterestReceivableGlID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.GlAccounts", t => t.CurrentCotIncomeGlID)
                .ForeignKey("dbo.GlAccounts", t => t.CurrentInterestExpenseGlID)
                .ForeignKey("dbo.GlAccounts", t => t.LoanInterestExpenseGLID)
                .ForeignKey("dbo.GlAccounts", t => t.LoanInterestIncomeGlID)
                .ForeignKey("dbo.GlAccounts", t => t.LoanInterestReceivableGlID)
                .ForeignKey("dbo.GlAccounts", t => t.SavingsInterestExpenseGlID)
                .ForeignKey("dbo.GlAccounts", t => t.SavingsInterestPayableGlID)
                .Index(t => t.SavingsInterestExpenseGlID)
                .Index(t => t.SavingsInterestPayableGlID)
                .Index(t => t.CurrentInterestExpenseGlID)
                .Index(t => t.CurrentCotIncomeGlID)
                .Index(t => t.LoanInterestIncomeGlID)
                .Index(t => t.LoanInterestExpenseGLID)
                .Index(t => t.LoanInterestReceivableGlID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountConfigurations", "SavingsInterestPayableGlID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "SavingsInterestExpenseGlID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "LoanInterestReceivableGlID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "LoanInterestIncomeGlID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "LoanInterestExpenseGLID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "CurrentInterestExpenseGlID", "dbo.GlAccounts");
            DropForeignKey("dbo.AccountConfigurations", "CurrentCotIncomeGlID", "dbo.GlAccounts");
            DropIndex("dbo.AccountConfigurations", new[] { "LoanInterestReceivableGlID" });
            DropIndex("dbo.AccountConfigurations", new[] { "LoanInterestExpenseGLID" });
            DropIndex("dbo.AccountConfigurations", new[] { "LoanInterestIncomeGlID" });
            DropIndex("dbo.AccountConfigurations", new[] { "CurrentCotIncomeGlID" });
            DropIndex("dbo.AccountConfigurations", new[] { "CurrentInterestExpenseGlID" });
            DropIndex("dbo.AccountConfigurations", new[] { "SavingsInterestPayableGlID" });
            DropIndex("dbo.AccountConfigurations", new[] { "SavingsInterestExpenseGlID" });
            DropTable("dbo.AccountConfigurations");
        }
    }
}
