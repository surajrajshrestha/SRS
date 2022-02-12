using ImageCompressor.Implementations;
using ImageCompressor.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace ImageCompressor
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddImageCompressor(this IServiceCollection services)
        {
            services.AddScoped<ICompressor, Compressor>();
            return services;
        }
    }
}
