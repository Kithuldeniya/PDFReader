using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;

namespace PDFReader.Helpers
{
    public static class PdfHelper
    {
        public static string ManipulatePdf(string filePath)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(filePath));

            //CustomFontFilter fontFilter = new CustomFontFilter(rect);
            FilteredEventListener listener = new FilteredEventListener();

            // Create a text extraction renderer
            LocationTextExtractionStrategy extractionStrategy = listener
                .AttachEventListener(new LocationTextExtractionStrategy());

            // Note: If you want to re-use the PdfCanvasProcessor, you must call PdfCanvasProcessor.reset()
            new PdfCanvasProcessor(listener).ProcessPageContent(pdfDoc.GetFirstPage());

            // Get the resultant text after applying the custom filter
            String actualText = extractionStrategy.GetResultantText();

            pdfDoc.Close();

            return actualText;

        }
    }
}
