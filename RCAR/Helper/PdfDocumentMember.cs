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
        protected Font _itemFont { get; set; }
        protected Font _boldFont { get; set; }
        protected BaseColor _tableHeaderWhiteColor { get; set; }
        protected BaseColor _tableHeaderGrayColor { get; set; }

        public PdfDocumentMember(object model)
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

            titleTable.AddCell(cell);

            return titleTable;
        }

        private PdfPCell GetHeaderTableCell(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = _tableHeaderGrayColor;
            cell.PaddingBottom = 5f;

            return cell;
        }

        private PdfPCell GetCellWithBorderAlignCenter(string text)
        {
            var phrase = new Phrase(text, _itemFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 5f;

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

                var itemTable = new PdfPTable(10);
                itemTable.WidthPercentage = 100;

                itemTable.AddCell(GetHeaderTableCell("Numer"));
                itemTable.AddCell(GetHeaderTableCell("Imię i Nazwisko"));
                itemTable.AddCell(GetHeaderTableCell("Telefon"));
                itemTable.AddCell(GetHeaderTableCell("Email"));
                itemTable.AddCell(GetHeaderTableCell("Data przyjęcia"));
                itemTable.AddCell(GetHeaderTableCell("Data odejścia"));
                itemTable.AddCell(GetHeaderTableCell("Adres"));
                itemTable.AddCell(GetHeaderTableCell("Miasto"));
                itemTable.AddCell(GetHeaderTableCell("Kod pocztowy"));
                itemTable.AddCell(GetHeaderTableCell("Status"));


                foreach (var item in model)
                {
                    var cell = GetCellWithBorderAlignCenter(item.MemberNo);
                    cell.BackgroundColor = _tableHeaderGrayColor;
                    itemTable.AddCell(cell);
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.FullName));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.Phone));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.Email));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateJoined.ToShortDateString()));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.DateLeaves.ToShortDateString()));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.Address));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.City));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.PostCode));
                    itemTable.AddCell(GetCellWithBorderAlignCenter(item.Status));

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
