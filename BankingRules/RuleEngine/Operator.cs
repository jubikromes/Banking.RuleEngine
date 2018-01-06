using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.RuleEngine
{
    public class Operator
    {
        private const string NoResult = "_NO_RESULT";

        public Q RunTest<T, Q>(T LeftVal, T rightVal, Q ExpectedVal, string operand)
        {
            try
            {
                var LeftValue = Expression.Variable(LeftVal.GetType(), "LeftVal");
                var RightValue = Expression.Variable(rightVal.GetType(), "RightVal");
                var operation = BinaryExp(operand);
                var opResult = Expression.MakeBinary(operation, LeftValue, RightValue);
                var lambdaresult = LambdaExpression.Lambda<Func<T, T, Q>>(opResult, LeftValue, RightValue);
                var result = lambdaresult.Compile();
                return result(LeftVal, rightVal);
            }
            catch(Exception ex)
            {
                
            }
            return ExpectedVal;
        }
        public Q RunTestWithoutExpectedResult<T, Q>(T LeftVal, T rightVal, string operand)
        {
            try
            {
                var LeftValue = Expression.Variable(LeftVal.GetType(), "LeftVal");
                var RightValue = Expression.Variable(rightVal.GetType(), "RightVal");
                var operation = BinaryExp(operand);
                var opResult = Expression.MakeBinary(operation, LeftValue, RightValue);
                var lambdaresult = LambdaExpression.Lambda<Func<T, T, Q>>(opResult, LeftValue, RightValue);
                var result = lambdaresult.Compile();
                return result(LeftVal, rightVal);
            }
            catch (Exception ex)
            {
                //log exception
                dynamic dir = NoResult;
                return dir;
            }
        }
        public ExpressionType BinaryExp(string operandString)
        {
            ExpressionType expression = ExpressionType.And;
            switch (operandString)
            {
                case "greaterthan":
                     expression = ExpressionType.GreaterThan;
                     break;
                case "lessthan":
                    expression = ExpressionType.LessThan;
                     break;
                case "equals":
                     expression = ExpressionType.Equal;
                     break;
                case "and":
                     expression = ExpressionType.And;
                     break;
                case "or":  
                    expression = ExpressionType.Or;
                    break;
                case "lessthanorequalto":
                    expression = ExpressionType.LessThanOrEqual;
                    break;
                case "greaterthanorequalto":
                    expression = ExpressionType.GreaterThanOrEqual;
                    break;
                case "add":
                    expression =ExpressionType.Add;
                    break;
                case "subtract":
                    expression = ExpressionType.Subtract;
                    break;
                case "notequal":
                    expression = ExpressionType.NotEqual;
                    break;
            }

            return expression;
        }
    }
}
