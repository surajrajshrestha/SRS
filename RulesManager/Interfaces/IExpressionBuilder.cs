
using RulesManager.Models;
using System.Linq.Expressions;

namespace RulesManager.Interfaces
{
    public interface IExpressionBuilder
    {
        Expression BuildExpression<T>(Rule rule, ParameterExpression param);
        Func<T, bool> CompileRule<T>(Rule r);
    }
}
