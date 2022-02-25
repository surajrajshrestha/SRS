using Microsoft.Extensions.DependencyInjection;
using RulesManager.Interfaces;

namespace RulesManager
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRuleEngine(this IServiceCollection services)
        {
            services.AddScoped<IExpressionBuilder, IExpressionBuilder>();
            return services;
        }
    }
}
