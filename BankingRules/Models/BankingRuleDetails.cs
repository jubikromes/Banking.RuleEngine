using BankingRules.RuleEngine.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Models
{
    public class BankingRuleDetails : BaseEntity
    {
        public string LeftOperator { get; set; }
        public string RightOperator { get; set; }
        public string Operand { get; set; }
        public string ExpectedResult { get; set; }
        public string LeftParameterString { get; set; }
        public string RightParamererString { get; set; }
        public bool IsChained { get; set; }
        public Guid BankingRuleId { get; set; }
        public BankingRule BankingRule { get; set; }
        public int Order { get; set; }
        public string RuleType { get; set; }
        public string ExpectedResultType { get; set; }
        public Int32 OperationType { get; set; }
        public bool CanBeTerminated { get; set; }
        public bool ExpectsResult { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
    }
}
