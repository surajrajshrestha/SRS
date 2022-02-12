using HtmlToPdfConverter.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Drawing;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;

namespace HtmlToPdfConverter.Implementations
{
    public class PdfGenerator : IPdfGenerator
    {
        private readonly IWebHostEnvironment _environment;
        public PdfGenerator(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public FileResult GeneratePdf(string html)
        {
            Syncfusion.HtmlConverter.HtmlToPdfConverter htmlConverter = new Syncfusion.HtmlConverter.HtmlToPdfConverter(HtmlRenderingEngine.WebKit);
            WebKitConverterSettings settings = new WebKitConverterSettings();

            settings.WebKitPath = Path.Combine(_environment.ContentRootPath, "QtBinariesWindows");
            settings.EnableRepeatTableHeader = true;
            settings.EnableRepeatTableFooter = true;

            htmlConverter.ConverterSettings= settings;
            htmlConverter.ConverterSettings.Margin.Left = 20;
            htmlConverter.ConverterSettings.Margin.Right = 20;
            htmlConverter.ConverterSettings.Margin.Top = 30;
            htmlConverter.ConverterSettings.Margin.Bottom = 7;
            htmlConverter.ConverterSettings.EnableJavaScript = true;
            htmlConverter.ConverterSettings.SplitImages = true;

            PdfDocument doc = htmlConverter.Convert(html, "");
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 7, PdfFontStyle.Bold);
            PdfSolidBrush brush = new PdfSolidBrush(Color.Black);

            RectangleF footer = new RectangleF(0, 0, doc.Pages[0].GetClientSize().Width, 50);
            PdfPageTemplateElement footerContainer = new PdfPageTemplateElement(footer);

            PdfPageNumberField pageNumber = new PdfPageNumberField(font, brush);
            PdfPageCountField pageCount = new PdfPageCountField(font, brush);
            PdfCompositeField compositeField = new PdfCompositeField(font, brush, "Page {0} of {1}", pageNumber, pageCount);
            compositeField.Bounds = footerContainer.Bounds;

            compositeField.Draw(footerContainer.Graphics, new PointF(0, 40));
            doc.Template.Bottom = footerContainer;

            MemoryStream memoryStream = new();
            doc.Save(memoryStream);
            memoryStream.Position = 0;
            doc.Close(true);

            FileResult fileResult = new FileContentResult(memoryStream.ToArray(), "application/pdf");
            fileResult.FileDownloadName = $"{DateTime.Now.Ticks}.pdf";
            return fileResult;
        }
    }
}
