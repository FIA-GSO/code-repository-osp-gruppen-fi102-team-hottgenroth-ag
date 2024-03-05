using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Logisitcs.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

using System.IO;
using System.Threading.Tasks;

namespace Logisitcs.DAL
{
    public class PDFDAL : IPDFDAL
    {
        public async Task<string> Create(object jsonData)
        {
            string pdfFileName = "example.pdf";

            return pdfFileName;
        }

        public async Task<byte[]> Create(string jsonData)
        {
            // JSON-Daten deserialisieren
            //var data = JsonConvert.DeserializeObject<MyData>(jsonData);

            // Erstellen eines MemoryStreams
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    // Erstellen eines PDF-Dokuments
            //    PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));

            //    // Erstellen eines Layout-Dokuments
            //    Document document = new Document(pdfDocument);

            //    // Hinzufügen von Inhalten zum Layout-Dokument
            //    document.Add(new Paragraph("Hello World"));
            //    //document.Add(new Paragraph(data.SomeProperty)); // Beispiel für die Verwendung von Daten aus dem JSON

            //    // Schließen des Layout-Dokuments
            //    document.Close();

            //    // Rückgabe des PDFs als Bytearray
            //    return memoryStream.ToArray();
            //}

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(memoryStream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        // Setzen des Seitenrandes auf 2 cm
                        Document document = new Document(pdf, PageSize.A4);
                        document.SetMargins(20, 20, 20, 20);

                        // Register the event handler for headers and footers
                        pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new PdfHeaderFooterHandler());

                        // Erstellen der Tabelle mit 5 Spalten
                        iText.Layout.Element.Table table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(new float[] { 1, 1, 1, 1, 1 })).UseAllAvailableWidth();

                        table.SetMarginTop(56.7f);

                        // Hinzufügen von 10 Zeilen mit Beispielinhalt in jede Zelle
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                table.AddCell(new Cell().Add(new Paragraph($"Zelle {i + 1},{j + 1}")));
                            }
                        }

                        // Hinzufügen der Tabelle zum Dokument
                        document.Add(table);

                        // Dein PDF-Inhalt
                        document.Add(new Paragraph("Hello World"));

                        document.Close();
                    }
                }

                return memoryStream.ToArray();
            }
        }

        // Hilfsmethode zum Generieren von zufälligem Text für Paragraphen
        private string GenerateRandomText()
        {
            string[] words = { "Lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipiscing", "elit" };
            Random random = new Random();
            int numberOfWords = random.Next(5, 20); // Zufällige Anzahl von Wörtern pro Absatz
            string paragraphText = "";

            for (int i = 0; i < numberOfWords; i++)
            {
                int index = random.Next(words.Length);
                paragraphText += words[index] + " ";
            }

            return paragraphText;
        }
    }

    public class MyData
    {
        public string SomeProperty { get; set; }
        // Weitere Eigenschaften nach Bedarf
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

            // Header
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetWidth() / 2 - 60, pageSize.GetTop() - 30)
                .ShowText("Name des Projektes - Transport Liste")
                .EndText();
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetTop() - 35).LineTo(pageSize.GetRight() - 20, pageSize.GetTop() - 35).Stroke();

            // Footer
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetWidth() / 2 - 15, pageSize.GetBottom() + 20)
                .ShowText("Seite " + pdf.GetPageNumber(page))
                .EndText();
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetBottom() + 25).LineTo(pageSize.GetRight() - 20, pageSize.GetBottom() + 25).Stroke();

            pdfCanvas.Release();
        }
    }
}