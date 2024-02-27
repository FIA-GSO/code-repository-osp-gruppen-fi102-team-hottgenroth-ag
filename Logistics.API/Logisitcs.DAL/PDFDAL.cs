using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Logisitcs.DAL.Interfaces;
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
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Erstellen eines PDF-Dokuments
                PdfDocument pdfDocument = new PdfDocument(new PdfWriter(memoryStream));

                // Erstellen eines Layout-Dokuments
                Document document = new Document(pdfDocument);

                // Hinzufügen von Inhalten zum Layout-Dokument
                document.Add(new Paragraph("Hello World"));
                //document.Add(new Paragraph(data.SomeProperty)); // Beispiel für die Verwendung von Daten aus dem JSON

                // Schließen des Layout-Dokuments
                document.Close();

                // Rückgabe des PDFs als Bytearray
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
}