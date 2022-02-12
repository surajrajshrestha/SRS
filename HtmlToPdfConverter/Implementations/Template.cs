using HtmlToPdfConverter.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HtmlToPdfConverter.Implementations
{
    public class Template : ITemplate
    {
        private IRazorViewEngine _razorViewEngine;
        private IServiceProvider _serviceProvider;
        private ITempDataProvider _tempDataProvider;

        public Template(IRazorViewEngine razorViewEngine,
            IServiceProvider serviceProvider,
            ITempDataProvider tempDataProvider)
        {
            _razorViewEngine = razorViewEngine;
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
        }
        public async Task<string> ConvertHtmlToStringAsync<T>(string viewName, T model) where T : class, new()
        {
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = _serviceProvider
            };

            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            using StringWriter stringWriter = new StringWriter();

            var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);
            if (viewResult.View == null)
                return string.Empty;

            var metaDataProvider = new EmptyModelMetadataProvider();
            var modelStateDictionary = new ModelStateDictionary();
            var viewDataDictionary = new ViewDataDictionary(metaDataProvider, modelStateDictionary);

            var tempDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);

            var viewContext = new ViewContext(actionContext, viewResult.View, viewDataDictionary, tempDictionary, stringWriter, new HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return stringWriter.ToString();

        }
    }
}
