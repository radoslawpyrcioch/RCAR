using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using RCAR.Models.MemberVM;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Helper
{
    public class PdfDocumentMember
    {
        private object _model { get; set; }
        protected Document _pdfDocument { get; set; }
        protected Font _titleFont { get; set; }
        protected Font _footerFont { get; set; }
        protected Font _itemFont { get; set; }
        protected Font _boldFont { get; set; }
        protected Font _underlineFont { get; set; }

        public PdfDocumentMember(object model)
        {
            _model = model;
            _titleFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 16);
            _footerFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 25);
            _itemFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12);
            _underlineFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.UNDERLINE);
            _boldFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.BOLD);
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
            cell.PaddingBottom = 15f;

            titleTable.AddCell(cell);

            return titleTable;
        }

        private PdfPTable FooterTable(string footer)
        {
            var footerTable = new PdfPTable(1);
            footerTable.SpacingAfter = 10f;
            footerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            var phrase = new Phrase(footer, _footerFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 30f;
            cell.Border = Rectangle.NO_BORDER;

            footerTable.AddCell(cell);

            return footerTable;
        }

        private PdfPCell RecordName(string text)
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
            cell.Colspan = 0;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell HeaderTableName(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Colspan = 8;
            cell.Padding = 5f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell EmptyCell(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 2;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell StatusCell(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 3;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 15f;

            return cell;
        }

        private PdfPCell GetTableNumberHeader(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 2;
            cell.BackgroundColor = CMYKColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 15f;
            cell.Border = Rectangle.NO_BORDER;
            return cell;    
        }

        private PdfPCell GetTableRecordHeader(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 4;
            cell.BackgroundColor = CMYKColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 15f;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell UnderlineAfterOneMember(string text)
        {
            var phrase = new Phrase(text, _underlineFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 6;

            return cell;
        }


        public byte[] Generate()
        {
            var model = (IEnumerable<MemberVM>)_model;
            using (MemoryStream ms = new MemoryStream())
            {
                _pdfDocument = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, ms);

                Rectangle pageSize = writer.PageSize;

                // Open the Document for writing
                _pdfDocument.Open();

                _pdfDocument.Add(TitleTable(string.Format("Spis wszystkich członków | Data: {0}", DateTime.Today.ToShortDateString())));        

                var itemTable = new PdfPTable(6);
                itemTable.WidthPercentage = 100;

                itemTable.AddCell(GetTableNumberHeader("Number"));
                itemTable.AddCell(GetTableRecordHeader("Informacje"));
                
                
                foreach (var item in model)
                {
                    itemTable.AddCell(RecordName("Numer"));
                    itemTable.AddCell(HeaderName(item.MemberNo));
                  
                    itemTable.AddCell(RecordName("Imię i Nazwisko"));
                    itemTable.AddCell(HeaderName(item.FullName));

                    itemTable.AddCell(RecordName("Telefon"));
                    itemTable.AddCell(HeaderName(item.Phone));

                    itemTable.AddCell(EmptyCell(""));

                    itemTable.AddCell(RecordName("Email"));
                    itemTable.AddCell(HeaderName(item.Email));

                    itemTable.AddCell(RecordName("Data przyjęcia"));
                    itemTable.AddCell(HeaderName(item.DateJoined.ToShortDateString()));

                    itemTable.AddCell(EmptyCell(""));

                    itemTable.AddCell(RecordName("Data odejścia"));
                    itemTable.AddCell(HeaderName(item.DateLeaves.ToShortDateString()));
                  
                    itemTable.AddCell(RecordName("Adres"));
                    itemTable.AddCell(HeaderName(item.Address));

                    itemTable.AddCell(EmptyCell(""));

                    itemTable.AddCell(RecordName("Miasto"));
                    itemTable.AddCell(HeaderName(item.City));

                    itemTable.AddCell(RecordName("Kod pocztowy"));
                    itemTable.AddCell(HeaderName(item.PostCode));

                    itemTable.AddCell(EmptyCell(""));

                    itemTable.AddCell(RecordName("Status"));
                    itemTable.AddCell(StatusCell(item.Status));

                    itemTable.AddCell(UnderlineAfterOneMember(""));

                }
                _pdfDocument.Add(itemTable);

                _pdfDocument.Add(FooterTable(string.Format("RCAR")));

                _pdfDocument.Close();

                writer.Close();

                byte[] bytes = ms.ToArray();
                ms.Close();

                return bytes;
            }
        }
    }
}
