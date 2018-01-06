using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankingRules.Data.Message
{
    public class RuleMessage<T>
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }
        public T Result { get; set; }
        public dynamic AdditionalResult { get; set; }
    }
}
