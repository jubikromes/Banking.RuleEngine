namespace BankingRules.Migrations
{
    using BankingRules.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BankingRules.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BankingRules.Models.ApplicationDbContext context)
        {
        //    This method will be called after migrating to the latest version.
        //    You can use the DbSet<T>.AddOrUpdate() helper extension method
        //    to avoid creating duplicate seed data.
            //context.Rules.Add(new BankingRule
            //{
            //    CreatedDate = DateTime.Now,
            //    Description = "Less than rule",
            //    Name = "Greater than Rule",
            //    Id = Guid.NewGuid(),
            //    IsDeleted = false,
            //    RuleScore = 67,
            //    RuleDetails = new List<BankingRuleDetails>
            //    {
            //        new BankingRuleDetails
            //        {
            //            IsDeleted = false,
            //            CreatedDate = DateTime.Now,
            //            ExpectedResult = "false",
            //            ExpectedResultType = "System.Boolean",
            //            LeftOperator = "90",
            //            RightOperator = "78",
            //            IsChained = true,
            //            Operand = "greaterthan",
            //            OperationType = 1,
            //            Order = 1,
            //            RuleType = "System.Double",
            //            Id = Guid.NewGuid()
            //        }
            //    }
            //});
        }
    }
}
