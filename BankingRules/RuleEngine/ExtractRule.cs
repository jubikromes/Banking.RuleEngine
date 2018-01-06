using BankingRules.RuleEngine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.RuleEngine
{
    public class ExtractRule
    {
        private readonly IIncomingParameters _parameters;
        public ExtractRule(IIncomingParameters parameters)
        {
            _parameters = parameters;
        }
        public object ReplaceParameterWithField(string propertyName)
        {
            try
            {
                var typeExpr = Expression.Constant(_parameters);
                var property = Expression.PropertyOrField(typeExpr, propertyName);
                object prop = Expression.Lambda<Func<string>>(property).Compile()();
                return prop;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
