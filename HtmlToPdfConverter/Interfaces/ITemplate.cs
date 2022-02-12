namespace HtmlToPdfConverter.Interfaces
{
    public interface ITemplate
    {
        Task<string> ConvertHtmlToStringAsync<T>(string viewName, T model) where T : class, new();
    }
}
