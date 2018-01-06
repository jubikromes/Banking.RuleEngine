using BankingRules.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Map
{
    public class BankingRuleDetailsMap : EntityTypeConfiguration<BankingRuleDetails>
    {
        public BankingRuleDetailsMap()
        {
            MapToStoredProcedures();
        }
    }
}
