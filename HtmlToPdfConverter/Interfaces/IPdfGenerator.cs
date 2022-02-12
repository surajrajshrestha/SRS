using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdfConverter.Interfaces
{
    public interface IPdfGenerator
    {
        FileResult GeneratePdf(string html);
    }
}
