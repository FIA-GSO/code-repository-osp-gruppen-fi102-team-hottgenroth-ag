using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using System.IO;
using System.Threading.Tasks;
using System;
using iText.Layout;
using System.Collections.Generic;

namespace Logisitcs.BLL.Helper
{
    public class PDFHelper
    {
        private int y_Achse = 0;

        public async Task<string> Create(object jsonData)
        {
            string pdfFileName = "Table.pdf";

            return pdfFileName;
        }

        public async Task<byte[]> Create(List<ITransportBoxData> box)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        // Setzen des Seitenrandes auf 2 cm
                        Document document = new Document(pdf, PageSize.A4);
                        document.SetMargins(20, 20, 20, 20);

                        var pageSize = pdf.GetNumberOfPages();

                        // Register the event handler for headers and footers
                        pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new PdfHeaderFooterHandler());

                        // Box hinzufügen
                        // AddBox(document, pdf, pageSize);
                        PdfPage page = AddBox(document, pdf, pdf.GetNumberOfPages(), y_Achse);

                        pdf.AddNewPage();

                        document.Close();
                    }
                }

                return memoryStream.ToArray();
            }
        }

        private PdfPage AddBox(Document document, PdfDocument pdf, int pageNumber, int y_Achse)
        {
            PdfPage pdfPage;

            if (pdf.GetNumberOfPages() > 0)
            {
                pdfPage = pdf.GetLastPage();
            }
            else
            {
                pdfPage = pdf.AddNewPage();
            }

            PdfCanvas pdfCanvas = new PdfCanvas(pdfPage);
            Rectangle pageSize = pdfPage.GetPageSize();

            // Schrift und Größe setzen
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            float fontSize = 12;

            // Texte für Boxnummer und Boxkategorie
            string boxNumber = "Box (number)";
            string boxCategory = "Type: (BoxCategory)";

            // Textbreite berechnen
            float textWidth = font.GetWidth(boxCategory, fontSize);

            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
            .MoveText(pageSize.GetLeft() + 20, pageSize.GetTop() - 70).SetColor(new DeviceRgb(0, 0, 0), true)
            .ShowText(boxNumber + " - " + "ProjectnameVariable" + boxCategory)
            .EndText();

            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
            .MoveText(pageSize.GetRight() - 20 - textWidth, pageSize.GetTop() - 70).SetColor(new DeviceRgb(0, 0, 0), true)
            .ShowText(boxCategory)
            .EndText();

            // Zeichnen der Linie unter Boxnummer und Boxkategorie
            // pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetBottom() + 25).LineTo(pageSize.GetRight() - 20, pageSize.GetBottom() + 25).Stroke();

            return pdfPage;
        }
    }

    public class PdfHeaderFooterHandler : IEventHandler
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdf = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();
            Rectangle pageSize = page.GetPageSize();
            PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

            // Aktuelles Datum erhalten und formatieren
            DateTime today = DateTime.Today;
            string formattedDate = today.ToString("dd.MM.yyyy");
            Console.WriteLine(formattedDate);

            // Schrift und Größe setzen
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            float fontSize = 12;

            // Textbreite berechnen
            float textWidth = font.GetWidth(formattedDate, fontSize);

            // Position für den Text berechnen, so dass er am rechten Rand endet
            float textX = pageSize.GetRight() - 20 - textWidth; // 20 ist der Rand, den Sie auch für die Linie verwendet haben
            float textY = pageSize.GetTop() - 30;

            // Aktuelles Datum hinzufügen
            pdfCanvas.BeginText()
                .SetFontAndSize(font, fontSize)
                .MoveText(textX, textY)
                .ShowText(formattedDate)
                .EndText();

            // Header
            // Zeile Projektname
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetLeft() + 20, pageSize.GetTop() - 30)
                .ShowText("Projectname - " + "ProjectnameVariable")
                .EndText();

            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
            .MoveText(pageSize.GetRight() - 20, pageSize.GetTop() - 30)
            .ShowText("")
            .EndText();

            // Zeile Erstelldatum
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 9)
            .MoveText(pageSize.GetLeft() + 20, pageSize.GetTop() - 40)
            .SetColor(new DeviceRgb(128, 128, 128), true)
            .ShowText("Creation Date: " + "creationDateVariable")
            .EndText();

            // Erste Linie
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetTop() - 45).LineTo(pageSize.GetRight() - 20, pageSize.GetTop() - 45).Stroke();

            // Footer
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetWidth() / 2 - 15, pageSize.GetBottom() + 10)
                .ShowText("Seite " + pdf.GetPageNumber(page))
                .EndText();
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetBottom() + 25).LineTo(pageSize.GetRight() - 20, pageSize.GetBottom() + 25).Stroke();

            pdfCanvas.Release();
        }
    }
}