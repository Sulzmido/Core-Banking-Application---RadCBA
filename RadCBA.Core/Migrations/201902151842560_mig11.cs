namespace RadCBA.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccountConfigurations", "SavingsCreditInterestRate", c => c.Double(nullable: false));
            AlterColumn("dbo.AccountConfigurations", "SavingsMinimumBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "CurrentCreditInterestRate", c => c.Double(nullable: false));
            AlterColumn("dbo.AccountConfigurations", "CurrentMinimumBalance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "CurrentCot", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "LoanDebitInterestRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AccountConfigurations", "LoanDebitInterestRate", c => c.Double());
            AlterColumn("dbo.AccountConfigurations", "CurrentCot", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "CurrentMinimumBalance", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "CurrentCreditInterestRate", c => c.Double());
            AlterColumn("dbo.AccountConfigurations", "SavingsMinimumBalance", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.AccountConfigurations", "SavingsCreditInterestRate", c => c.Double());
        }
    }
}
