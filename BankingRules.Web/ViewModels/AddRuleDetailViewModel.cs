using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingRules.Web.ViewModels
{
    public class AddRuleDetailViewModel
    {
        public string LeftOperator { get; set; }
        public string RightOperator { get; set; }
        public string Operand { get; set; }
        public string ExpectedResult { get; set; }
        public string LeftParameterString { get; set; }
        public string RightParamererString { get; set; }
        public bool IsChained { get; set; }
        public Guid BankingRuleId { get; set; }
        public int Order { get; set; }
        public string RuleType { get; set; }
        public string ExpectedResultType { get; set; }
        public Int32 OperationType { get; set; }
    }
}