using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingRules.Web.ViewModels
{
    public class EditRuleDetailViewModel
    {
        public string LeftOperator { get; set; }
        public string RightOperator { get; set; }
        public string Operand { get; set; }
        public string ExpectedResult { get; set; }
        public string LeftParameterString { get; set; }
        public string RightParamererString { get; set; }
        public bool IsChained { get; set; }
        public int Order { get; set; }
        public string RuleType { get; set; }
        public string ExpectedResultType { get; set; }
        public string OperationType { get; set; }
        public Guid Id { get; set; }
    }
}