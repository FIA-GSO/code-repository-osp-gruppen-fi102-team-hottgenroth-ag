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
using System.Linq;

namespace Logisitcs.BLL.Helper
{
    public class PdfHelper
    {
        public double y_Achse = 0;
        public string projectName = "Testprojekt";

        int _marginAllSites = 20;

        public async Task<byte[]> Create(List<ITransportBoxData> box, IProjectData project, List<IArticleData> articles)
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
                        pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new PdfHeaderFooterHandler(project));

                        // Box hinzufügen
                        PdfPage page = AddBox(document, pdf, box, articles);

                        document.Close();
                    }
                }

                return memoryStream.ToArray();
            }
        }

        private PdfPage AddBox(Document document, PdfDocument pdf, 
            List<ITransportBoxData> box, List<IArticleData> articles)
        {
            PdfPage pdfPage;

            pdfPage = pdf.AddNewPage();

            PdfCanvas pdfCanvas = new PdfCanvas(pdfPage);
            Rectangle pageSize = pdfPage.GetPageSize();

            // Schrift und Größe setzen
            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            float fontSize = 12;
            y_Achse = pageSize.GetTop() - 70;

            foreach (var b in box)
            {
                // Prüfen, ob genügend Platz für die aktuelle Box vorhanden ist
                float lineHeight = 20 + 25; // Höhe einer BoxNumber + BoxCategory + Description
                if (y_Achse < pageSize.GetBottom() + lineHeight)
                {
                    // Wenn nicht genügend Platz vorhanden ist, fügen Sie eine neue Seite hinzu
                    pdfPage = pdf.AddNewPage();
                    pdfCanvas = new PdfCanvas(pdfPage);
                    pageSize = pdfPage.GetPageSize();
                    y_Achse = pageSize.GetTop() - 70; // Setzen Sie die Y-Position für den Anfang der neuen Seite
                }

                // Texte für Boxnummer und Boxkategorie
                string boxNumber = "Box " + b.Number;
                string boxCategory = "Type: " + b.BoxCategory;

                // Textbreite berechnen
                float textWidth = font.GetWidth(boxCategory, fontSize);

                // BoxNumber
                pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetLeft() + 20, y_Achse).SetColor(new DeviceRgb(0, 0, 0), true)
                .ShowText(boxNumber)
                .EndText();

                // BoxCategory
                pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetRight() - 20 - textWidth, y_Achse).SetColor(new DeviceRgb(0, 0, 0), true)
                .ShowText(boxCategory)
                .EndText();

                y_Achse -= 20;

                // Description
                pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetLeft() + 30, y_Achse).SetColor(new DeviceRgb(128, 128, 128), true)
                .ShowText(b.Description)
                .EndText();

                y_Achse -= 15;

                // Artikel für diese Box erhalten
                var boxArticles = GetArticlesForBox(articles, b.BoxGuid.ToString());

                var positionRight = pageSize.GetLeft() + 40;

                foreach (var article in boxArticles)
                {
                    if ((double)(int)article.Position == article.Position)
                    {         
                        positionRight = pageSize.GetLeft() + 40;
                    } else
                    {
                        positionRight = pageSize.GetLeft() + 50;
                    }

                    // Maximale Breite des Beschreibungstextes basierend auf 3/4 der Seitenbreite
                    float maxDescriptionWidth = 80; //(pageSize.GetRight() - pageSize.GetLeft()) * 0.75f;

                    // Kürzen und '...' hinzufügen, falls erforderlich
                    string truncatedDescription = article.Description.Length > maxDescriptionWidth ?
                        article.Description.Substring(0, (int)maxDescriptionWidth - 3) + "..." :
                        article.Description;

                    // Article Linke Seite
                    pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                    .MoveText(positionRight, y_Achse).SetColor(new DeviceRgb(0, 0, 0), true)
                    .ShowText(article.Position.ToString() + " " + truncatedDescription)
                    .EndText();

                    textWidth = font.GetWidth(article.Quantity + " " + article.Unit, fontSize);

                    // Article Rechte Seite
                    pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                    .MoveText(pageSize.GetRight() - 20 - textWidth, y_Achse).SetColor(new DeviceRgb(0, 0, 0), true)
                    .ShowText(article.Quantity + " " + article.Unit)
                    .EndText();

                    y_Achse -= 12;

                    // Status
                    pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 10)
                    .MoveText(positionRight + 10, y_Achse).SetColor(new DeviceRgb(0, 0, 0), true)
                    .ShowText("Status: " + article.Status)
                    .EndText();

                    y_Achse -= 18;

                    // Überprüfen, ob genügend Platz für den nächsten Artikel auf der Seite vorhanden ist
                    if (y_Achse < pageSize.GetBottom() + lineHeight)
                    {
                        // Wenn nicht genügend Platz vorhanden ist, füge eine neue Seite hinzu
                        pdfPage = pdf.AddNewPage();
                        pdfCanvas = new PdfCanvas(pdfPage);
                        pageSize = pdfPage.GetPageSize();
                        y_Achse = pageSize.GetTop() - 70; // Setze die Y-Position für den Anfang der neuen Seite
                    }
                }

                pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 40, y_Achse).LineTo(pageSize.GetRight() - 40, y_Achse).Stroke();

                y_Achse -= 20; // Aktualisiere y_Achse für den nächsten Absatz

            }

            return pdfPage;
        }

        private List<IArticleData> GetArticlesForBox(List<IArticleData> articles, string boxGuid)
        {
            // Filter die Artikel nach der gegebenen boxGuid
            return articles.Where(article => article.BoxGuid.ToString() == boxGuid).ToList();
        }

        private Color GetStatusColor(int status)
        {
            switch (status)
            {
                case 0:
                    return new DeviceRgb(255, 0, 0); // Rot
                case 1:
                    return new DeviceRgb(128, 128, 128); // Grau
                case 2:
                    return new DeviceRgb(255, 165, 0); // Orange
                case 3:
                    return new DeviceRgb(0, 128, 0); // Grün
                case 4:
                    return new DeviceRgb(0, 0, 255); // Blau
                case 5:
                    return new DeviceRgb(0, 255, 255); // Cyan
                default:
                    return new DeviceRgb(0, 0, 0); // Standard: Schwarz
            }
        }
    }

    public class PdfHeaderFooterHandler : IEventHandler
    {
        private readonly IProjectData _projectData;

        public PdfHeaderFooterHandler(IProjectData projectData)
        {
            _projectData = projectData;
        }

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
            float textWidth = font.GetWidth("Print Date: " + formattedDate, fontSize);

            // Position für den Text berechnen, so dass er am rechten Rand endet
            float textX = pageSize.GetRight() - 20 - textWidth; // 20 ist der Rand, der auch für die Linie verwendet wird
            float textY = pageSize.GetTop() - 30;

            // Aktuelles Datum hinzufügen
            pdfCanvas.BeginText()
                .SetFontAndSize(font, fontSize)
                .MoveText(textX, textY)
                .ShowText("Print Date: " + formattedDate)
                .EndText();

            // Header
            // Zeile Projektname
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetLeft() + 20, pageSize.GetTop() - 30)
                .ShowText("Projectname - " + _projectData.ProjectName)
                .EndText();

            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
            .MoveText(pageSize.GetRight() - 20, pageSize.GetTop() - 30)
            .ShowText("")
            .EndText();

            // Zeile Erstelldatum
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 9)
            .MoveText(pageSize.GetLeft() + 20, pageSize.GetTop() - 40)
            .SetColor(new DeviceRgb(128, 128, 128), true)
            .ShowText("Creation Date: " + _projectData.CreationDate)
            .EndText();

            // Erste Linie
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetTop() - 45).LineTo(pageSize.GetRight() - 20, pageSize.GetTop() - 45).Stroke();

            // Footer
            pdfCanvas.BeginText().SetFontAndSize(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA), 12)
                .MoveText(pageSize.GetWidth() / 2 - 15, pageSize.GetBottom() + 10)
                .ShowText("Page " + pdf.GetPageNumber(page))
                .EndText();
            pdfCanvas.SetLineWidth(0.5f).MoveTo(pageSize.GetLeft() + 20, pageSize.GetBottom() + 25).LineTo(pageSize.GetRight() - 20, pageSize.GetBottom() + 25).Stroke();

            pdfCanvas.Release();
        }
    }
}