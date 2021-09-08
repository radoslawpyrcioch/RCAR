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
    public class PdfDocumentMemberCars
    {
        private object _model { get; set; }
        protected Document _pdfDocument { get; set; }
        protected Font _titleFont { get; set; }
        protected Font _itemFont { get; set; }
        protected Font _boldFont { get; set; }
        protected Font _headerDetailMemberFont { get; set; }

        public PdfDocumentMemberCars(object model)
        {
            _model = model;
            _titleFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 16);
            _itemFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12);
            _boldFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.BOLD);
            _headerDetailMemberFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 16, Font.BOLD);
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
            cell.PaddingBottom = 20f;

            titleTable.AddCell(cell);

            return titleTable;
        }

        private PdfPCell GetHeaderTableCell(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Colspan = 2;

            return cell;
        }

        private PdfPCell RecordTable(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 2;

            return cell;
        }

        private PdfPCell HeaderMemberDetail(string text)
        {
            var phrase = new Phrase(text, _headerDetailMemberFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 12;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 10;

            return cell;
        }

        private PdfPCell MemberDetailName(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 2;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell MemberDetail(string text)
        {
            var phrase = new Phrase(text);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 4;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell EmptyCellAfterDetailMember(string text)
        {
            var phrase = new Phrase(text);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 6;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell EmptySpaceBeforeTable(string empty)
        {
            var phrase = new Phrase(empty);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 12;
            cell.Padding = 6;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }



        public byte[] Generate()
        {
            var model = (MemberDetailVM)_model;
            using (MemoryStream ms = new MemoryStream())
            {
                _pdfDocument = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, ms);

                Rectangle pageSize = writer.PageSize;

                _pdfDocument.Open();

                _pdfDocument.Add(TitleTable(string.Format("Lista wszystkich samochodów | Data: {0}", DateTime.Today.ToShortDateString())));

                var itemTable = new PdfPTable(12);
                itemTable.WidthPercentage = 100;

                itemTable.AddCell(HeaderMemberDetail("Dane kontaktowe"));
                itemTable.AddCell(MemberDetailName("Imię i nazwisko: "));
                itemTable.AddCell(MemberDetail(model.FullName));
                itemTable.AddCell(MemberDetailName("Adres: "));
                itemTable.AddCell(MemberDetail(model.Address));
                itemTable.AddCell(MemberDetailName("Data przyjęcia: "));
                itemTable.AddCell(MemberDetail(model.DateJoined.ToShortDateString().ToString()));
                itemTable.AddCell(MemberDetailName("Miasto: "));
                itemTable.AddCell(MemberDetail(model.City));
                itemTable.AddCell(MemberDetailName("Data odejścia: "));
                itemTable.AddCell(MemberDetail(model.DateLeaves.ToShortDateString().ToString()));
                itemTable.AddCell(MemberDetailName("Kod pocztowy: "));
                itemTable.AddCell(MemberDetail(model.PostCode));
                itemTable.AddCell(MemberDetailName("Status serwisu: "));
                itemTable.AddCell(MemberDetail(model.Status));

                itemTable.AddCell(EmptyCellAfterDetailMember(" "));

                itemTable.AddCell(EmptySpaceBeforeTable(" "));

                itemTable.AddCell(GetHeaderTableCell("Marka"));
                itemTable.AddCell(GetHeaderTableCell("Model"));
                itemTable.AddCell(GetHeaderTableCell("Opis"));
                itemTable.AddCell(GetHeaderTableCell("Data przyjęcia"));
                itemTable.AddCell(GetHeaderTableCell("Data zakończenia"));
                itemTable.AddCell(GetHeaderTableCell("Status"));

                foreach (var item in model.Cars)
                {
                    itemTable.AddCell(RecordTable(item.Brand));
                    itemTable.AddCell(RecordTable(item.Model));
                    itemTable.AddCell(RecordTable(item.Description));
                    itemTable.AddCell(RecordTable(item.ServiceSince.ToShortDateString()));
                    itemTable.AddCell(RecordTable(item.ServiceTo.ToShortDateString()));                   
                    itemTable.AddCell(RecordTable(item.Status));
                   
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
