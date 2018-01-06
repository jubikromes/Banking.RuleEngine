using BankingRules.Data.Message;
using BankingRules.Data.Repository;
using BankingRules.Models;
using BankingRules.RuleEngine.Data;
using BankingRules.RuleEngine.Data.Enum;
using BankingRules.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.RuleEngine.Rules
{
    public class RuleService
    {
        private readonly IRepository<BankingRule> _ruleRepository;
        public RuleService(IRepository<BankingRule> ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }
        /// <summary>
        /// This would get all the active in the database
        /// </summary>
        /// <returns>RuleMessage<List<BankingRule>></returns>
        public RuleMessage<List<BankingRule>> GetRules()
        {
            var message = new RuleMessage<List<BankingRule>> {
                Result = new List<BankingRule> { },
                HasError = false,
                Message = ""
            };
            try
            {
                var rules = _ruleRepository.GetAll().Where(p => !p.IsDeleted).ToList();
                message.Result = rules;
                message.HasError = false;
                return message;
            }
            catch (Exception ex)
            {
                message.HasError = true;
                return message;
            }
        }

        /// <summary>
        /// This would get all the active in the database
        /// </summary>
        /// <returns>List<BankingRule></returns>
        private List<BankingRule> GetActiveRules()
        {
            try
            {
                return _ruleRepository.GetAll().Where(p => !p.IsDeleted).ToList();
            }
            catch(Exception ex)
            {
                return new List<BankingRule> { };
            }
        }

        /// <summary>
        /// This method is validates incoming transactions with all active rules and returns a score
        /// </summary>
        /// <returns>double</returns>
        public double RunRules(TransactionParameters incoming)
        {
            double ruleScore = 0;
            dynamic rightParameter = null;
            dynamic leftParameter = null;
            dynamic expectedResult = null;
            dynamic result = null;
            try
            {
                foreach (var rule in GetActiveRules())
                {
                    if (rule.RuleDetails == null)
                    {
                        continue;
                    }
                    var ruleDetails = rule.RuleDetails.OrderBy(p => p.Order).ToList();
                    foreach( var detail in ruleDetails)
                    {
                        if (!string.IsNullOrEmpty(detail.LeftOperator) || !detail.IsChained)
                        {
                            leftParameter = TypeConversion.ConvertToType(detail.LeftOperator, detail.RuleType);
                        }
                        else if (string.IsNullOrEmpty(detail.LeftOperator) || !detail.IsChained)
                        {
                            if (string.IsNullOrEmpty(detail.RightParamererString))
                                throw new Exception("");
                            var type = Type.GetType(detail.RuleType);
                            PropertyInfo info = type.GetProperty(detail.LeftParameterString);
                            ConstantExpression constantExpr = Expression.Constant(incoming);
                            Expression getExpr = Expression.Property(constantExpr, detail.LeftParameterString);
                            leftParameter = Expression.Lambda<Func<dynamic>>(getExpr).Compile()();
                        }
                        else if (detail.IsChained)
                        {
                            leftParameter = TypeConversion.ConvertToType(result, detail.RuleType);
                        }
                        if (!string.IsNullOrEmpty(detail.RightOperator))
                        {
                            rightParameter = TypeConversion.ConvertToType(detail.RightOperator, detail.RuleType);
                        }
                        else if (string.IsNullOrEmpty(detail.RightOperator))
                        {
                            if (string.IsNullOrEmpty(detail.RightParamererString))
                                throw new Exception("");
                            var type = Type.GetType(detail.RuleType);
                            PropertyInfo info = type.GetProperty(detail.LeftParameterString);
                            ConstantExpression constantExpr = Expression.Constant(incoming);
                            Expression getExpr = Expression.Property(constantExpr, detail.RightParamererString);
                            rightParameter = Expression.Lambda<Func<dynamic>>(getExpr).Compile()();
                        }
                        expectedResult = TypeConversion.ConvertToType(detail.ExpectedResult, detail.ExpectedResultType);
                        result = new Operator().RunTest(leftParameter, rightParameter, expectedResult, detail.Operand);
                        if (result != expectedResult && detail.CanBeTerminated && detail.ExpectsResult)
                        {
                            break;
                        }
                        else if (result == expectedResult && detail.IsLast)
                        {
                            ruleScore += rule.RuleScore;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                return ruleScore;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
