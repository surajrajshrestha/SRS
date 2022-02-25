using Microsoft.Extensions.DependencyInjection;

namespace EnumToListHelper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEnumHelper(this IServiceCollection services)
        {
            return services;
        }
    }
}
