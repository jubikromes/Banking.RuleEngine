namespace BankingRules.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestchanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankingRuleDetails", "CanBeTerminated", c => c.Boolean(nullable: false));
            AddColumn("dbo.BankingRuleDetails", "ExpectsResult", c => c.Boolean(nullable: false));
            AddColumn("dbo.BankingRuleDetails", "IsFirst", c => c.Boolean(nullable: false));
            AddColumn("dbo.BankingRuleDetails", "IsLast", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankingRuleDetails", "IsLast");
            DropColumn("dbo.BankingRuleDetails", "IsFirst");
            DropColumn("dbo.BankingRuleDetails", "ExpectsResult");
            DropColumn("dbo.BankingRuleDetails", "CanBeTerminated");
        }
    }
}
