namespace BankingRules.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sp_GetRuleDetail : DbMigration
    {
        public override void Up()
        {
            var sql = @"select * , 
                        (select Name from dbo.BankingRules) RuleName,
                        (select Id from dbo.BankingRules) RuleId
                        from dbo.BankingRuleDetails
                        where BankingRuleDetails.Id = @id;";
            CreateStoredProcedure("Sp_GetRuleDetail",  p => new  {
              id = p.Guid()
            }, sql);
        }
        
        public override void Down()
        {
            DropStoredProcedure("Sp_GetRuleDetail");
        }
    }
}
