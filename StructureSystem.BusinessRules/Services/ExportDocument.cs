using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Pdfa;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font;

namespace StructureSystem.BusinessRules.Services
{
   public class ExportDocument
    {

        public bool Export()
        {
            bool result = false;

            try
            {
                var exportFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var exportFile = System.IO.Path.Combine(exportFolder, "Test.pdf");

                using(var writer = new PdfWriter(exportFile))
                {
                    using(var pdf = new PdfDocument(writer))
                    {
                        var doc = new Document(pdf, iText.Kernel.Geom.PageSize.LETTER);

                        Table table = new Table(1);

                        Cell cell = new Cell().Add(new Paragraph("Analisis de estructura").SetFontSize(14));
                        cell = new Cell().Add(new Paragraph("Generales"));


                        doc.Add(new Paragraph("Holaaa"));
                    }
                }


                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }


    }
}
