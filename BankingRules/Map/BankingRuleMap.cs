using BankingRules.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Map
{
    public class BankingRuleMap : EntityTypeConfiguration<BankingRule>
    {
        public BankingRuleMap()
        {
            MapToStoredProcedures();
        }
    }
}
