using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Helper
{
    public class PdfDocumentServiceRemoved
    {
        private object _model { get; set; }
        protected Document _pdfDocument { get; set; }
        protected Font _titleFont { get; set; }
        protected Font _itemFont { get; set; }
        protected Font _boldFont { get; set; }
        protected BaseColor _tableHeaderWhiteColor { get; set; }
        protected BaseColor _tableHeaderGrayColor { get; set; }
        protected int numberService = 0;

        public PdfDocumentServiceRemoved(object model)
        {
            _model = model;
            _titleFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 16);
            _itemFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12);
            _boldFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.BOLD);
            _tableHeaderWhiteColor = WebColors.GetRGBColor("#f9fffb");
            _tableHeaderGrayColor = WebColors.GetRGBColor("#808080");
        }

        private PdfPTable TitleTable(string title)
        {
            var titleTable = new PdfPTable(1);
            titleTable.SpacingAfter = 10f;
            titleTable.DefaultCell.Border = Rectangle.NO_BORDER;

            var phrase = new Phrase(title, _titleFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.BackgroundColor = _tableHeaderWhiteColor;
            cell.PaddingBottom = 20f;

            titleTable.AddCell(cell);

            return titleTable;
        }

        private PdfPCell GetHeaderTableCell(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 10f;

            return cell;
        }

        private PdfPCell HeaderName(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 2;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell HeaderNumber(string text)
        {
            var phrase = new Phrase(text);
            var cell = new PdfPCell(phrase);
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Colspan = 8;
            cell.Padding = 5f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;

            return cell;
        }



        public byte[] Generate()
        {
            var model = (IEnumerable<ServiceVM>)_model;
            using (MemoryStream ms = new MemoryStream())
            {
                _pdfDocument = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, ms);

                Rectangle pageSize = writer.PageSize;

                // Open the Document for writing
                _pdfDocument.Open();

                _pdfDocument.Add(TitleTable(string.Format("Spis zakoczonych serwisów | Data: {0}", DateTime.Today.ToShortDateString())));

                var itemTable = new PdfPTable(8);
                itemTable.WidthPercentage = 100;


                foreach (var item in model)
                {
                    numberService++;
                    itemTable.AddCell(HeaderNumber(string.Format("Serwis numer: {0}", numberService)));

                    itemTable.AddCell(GetHeaderTableCell("Numer: "));
                    itemTable.AddCell(HeaderName(item.ServiceNo));

                    itemTable.AddCell(GetHeaderTableCell("Imię i Nazwisko"));
                    itemTable.AddCell(HeaderName(item.FullName));

                    itemTable.AddCell(GetHeaderTableCell("Telefon"));
                    itemTable.AddCell(HeaderName(item.Phone));

                    itemTable.AddCell(GetHeaderTableCell("Data przyjęcia"));
                    itemTable.AddCell(HeaderName(item.ServiceSince.ToShortDateString()));

                    itemTable.AddCell(GetHeaderTableCell("Data zakończenia"));
                    itemTable.AddCell(HeaderName(item.ServiceTo.ToShortDateString()));

                    itemTable.AddCell(GetHeaderTableCell("Marka samochodu"));
                    itemTable.AddCell(HeaderName(item.Brand));

                    itemTable.AddCell(GetHeaderTableCell("Model samochodu"));
                    itemTable.AddCell(HeaderName(item.Model));

                    itemTable.AddCell(GetHeaderTableCell("Opis"));
                    itemTable.AddCell(HeaderName(item.Description));

                    itemTable.AddCell(GetHeaderTableCell("Status"));
                    itemTable.AddCell(HeaderName(item.Status));

                   
                }
                _pdfDocument.Add(itemTable);

                _pdfDocument.Close();

                writer.Close();

                byte[] bytes = ms.ToArray();
                ms.Close();

                return bytes;
            }
        }
    }
}
