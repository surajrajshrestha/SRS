using HtmlToPdfConverter.Implementations;
using HtmlToPdfConverter.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlToPdfConverter
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHtmlToPdfConverter(this IServiceCollection services)
        {
            services.AddScoped<ITemplate, Template>();
            services.AddScoped<IPdfGenerator, PdfGenerator>();
            
            return services;
        }
    }
}
