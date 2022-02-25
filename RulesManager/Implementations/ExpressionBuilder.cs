using RulesManager.Interfaces;
using RulesManager.Models;
using System.Linq.Expressions;

namespace RulesManager.Implementations
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        public Expression BuildExpression<T>(Rule rule, ParameterExpression param)
        {
            var left = Expression.Property(param, rule.MemberName);
            var tProp = typeof(T).GetProperty(rule.MemberName).PropertyType;
            ExpressionType tBinary;

            if (Enum.TryParse(rule.Operator, out tBinary))
            {
                var right = Expression.Constant(Convert.ChangeType(rule.TargetValue, tProp));
                return Expression.MakeBinary(tBinary, left, right); 
            }
            else
            {
                var method = tProp.GetMethod(rule.Operator);
                var tParam = method.GetParameters()[0].ParameterType;
                var right = Expression.Constant(Convert.ChangeType(rule.TargetValue, tParam));
                return Expression.Call(left, method, right);
            }
        }

        public Func<T, bool> CompileRule<T>(Rule r)
        {
            var param = Expression.Parameter(typeof(T));
            Expression expr = BuildExpression<T>(r, param);
            return Expression.Lambda<Func<T, bool>>(expr, param).Compile();
        }
    }
}
