using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Models
{
    public class BankingRule : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double RuleScore { get; set; }

        public virtual List<BankingRuleDetails> RuleDetails { get; set; }
    }
}
