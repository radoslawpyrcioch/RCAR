using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using RCAR.Domain.Entities;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Helper
{
    public class PdfDocumentServiceInvoice
    {
        private object _model { get; set; }
        protected Document _pdfDocument { get; set; }
        protected Font _companyNameFont { get; set; }
        protected Font _placeHeaderFont { get; set; }
        protected Font _boldFont { get; set; }
        protected Font _titleFont { get; set; }
        protected Font _signatureFont { get; set; }
        protected int numberProduct = 0;
        protected decimal totalAmount = 0;
        protected decimal netAmount = 0;
        protected decimal discount = 0;
        protected decimal tax = 0;


        public PdfDocumentServiceInvoice(object model)
        {
            _model = model;
            _companyNameFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 30, Font.BOLD, BaseColor.LIGHT_GRAY);
            _placeHeaderFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12);
            _signatureFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 7);
            _boldFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 12, Font.BOLD);
            _titleFont = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.CACHED), 18, Font.BOLD);
        }

        private PdfPCell CompanyName(string text)
        {
            var phrase = new Phrase(text, _companyNameFont);
            var cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 5;
            cell.PaddingLeft = 30;
            cell.PaddingTop = 28;
            cell.Rowspan = 6;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell PlaceTableHeader(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 5;
            cell.Border = Rectangle.TOP_BORDER;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 4;

            return cell;
        }

        private PdfPCell PlaceTableRecord(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 5;
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 10;

            return cell;
        }

        private PdfPCell PlaceSellerBuyerHeader(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 4;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.PaddingBottom = 10;

            return cell;
        }

        private PdfPCell EmptyPlace()
        {
            var cell = new PdfPCell();
            cell.Colspan = 2;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell SellerInformation(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 4;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell BuyerInformation(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 4;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell InvoiceNumberHeader(string text)
        {
            var phrase = new Phrase(text, _titleFont);
            var cell = new PdfPCell(phrase);
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.PaddingTop = 25;
            cell.PaddingBottom = 8;
            cell.Colspan = 10;

            return cell;
        }

        private PdfPCell TableHeaderNumber(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 1;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell TableHeaderDescription(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 5;
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell TableRowNumber(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell TableRowDescription(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell TableTotalAmountHeader(string text)
        {
            var phrase = new Phrase(text, _boldFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 6;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;

            return cell;
        }

        private PdfPCell TableTotalAmount(string text)
        {
            var phrase = new Phrase(text, _placeHeaderFont);
            var cell = new PdfPCell(phrase);
            cell.Colspan = 1;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;

            return cell;
        }

        private PdfPCell PlaceForSignature(string text)
        {
            var phrase = new Phrase(text, _signatureFont);
            var cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 4;

            return cell;
        }

        private PdfPCell EmptyPlaceAfterTable(string text)
        {
            var phrase = new Phrase(text);
            var cell = new PdfPCell(phrase);
            cell.PaddingTop = 50;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 10;

            return cell;
        }




        public byte[] Generate()
        {
            var model = (ServiceDetailVM)_model;
            using (MemoryStream ms = new MemoryStream())
            {
                _pdfDocument = new Document(PageSize.A4, 30, 30, 50, 10);
                PdfWriter writer = PdfWriter.GetInstance(_pdfDocument, ms);

                Rectangle pageSize = writer.PageSize;

                _pdfDocument.Open();

                var itemTable = new PdfPTable(10);
                itemTable.WidthPercentage = 100;



                itemTable.AddCell(CompanyName("RCar Serwis"));
                itemTable.AddCell(PlaceTableHeader("Miesce wystawienia"));
                itemTable.AddCell(PlaceTableRecord("Katowice")); // possible to edit in the future
                itemTable.AddCell(PlaceTableHeader("Data wystawienia"));
                itemTable.AddCell(PlaceTableRecord(string.Format("{0}", DateTime.Today.ToShortDateString())));
                itemTable.AddCell(PlaceTableHeader("Data zakończenia usługi"));
                itemTable.AddCell(PlaceTableRecord(model.ServiceTo.ToShortDateString()));
                itemTable.AddCell(PlaceTableRecord(" "));
                itemTable.AddCell(PlaceTableRecord(" "));
                itemTable.AddCell(PlaceSellerBuyerHeader("Sprzedawca"));
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(PlaceSellerBuyerHeader("Nabywca"));
                itemTable.AddCell(SellerInformation("Serwis samochodowy Jan Nowak")); // possible to edit in the future
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(BuyerInformation(model.FullName));
                itemTable.AddCell(SellerInformation("NIP: 12345678")); // possible to edit in the future
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(BuyerInformation(model.Phone));
                itemTable.AddCell(SellerInformation("Konopnicka 17/2")); // possible to edit in the future
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(BuyerInformation(" "));
                itemTable.AddCell(SellerInformation("00-000 Katowice")); // possible to edit in the future
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(BuyerInformation(" "));
                itemTable.AddCell(InvoiceNumberHeader("Faktura VAT "));

                // Products Table
                itemTable.AddCell(TableHeaderNumber("Numer"));
                itemTable.AddCell(TableHeaderDescription("Opis"));
                itemTable.AddCell(TableHeaderNumber("Kwota netto"));
                itemTable.AddCell(TableHeaderNumber("Podatek"));
                itemTable.AddCell(TableHeaderNumber("Rabat"));
                itemTable.AddCell(TableHeaderNumber("Kwota brutto"));

                foreach (var item in model.PaymentRecords)
                {
                    numberProduct++;
                    itemTable.AddCell(TableRowNumber(string.Format("{0}", numberProduct)));
                    itemTable.AddCell(TableRowDescription(item.Description));
                    itemTable.AddCell(TableRowNumber(item.NetAmount.ToString()));
                    itemTable.AddCell(TableRowNumber(item.Tax.ToString()));
                    itemTable.AddCell(TableRowNumber(item.Discount.ToString()));
                    itemTable.AddCell(TableRowNumber(item.TotalAmount.ToString()));
                    totalAmount = totalAmount + item.TotalAmount;
                    netAmount = netAmount + item.NetAmount;
                    discount = discount + item.Discount;
                    tax = tax + item.Tax;
                }

                itemTable.AddCell(TableTotalAmountHeader("Razem"));
                itemTable.AddCell(TableTotalAmount(string.Format("{0}", netAmount)));
                itemTable.AddCell(TableTotalAmount(string.Format("{0}", tax)));
                itemTable.AddCell(TableTotalAmount(string.Format("{0}", discount)));
                itemTable.AddCell(TableTotalAmount(string.Format("{0}", totalAmount)));

                itemTable.AddCell(EmptyPlaceAfterTable(" "));

                itemTable.AddCell(PlaceForSignature("Podpis osoby upoważnionej do wystawiania"));
                itemTable.AddCell(EmptyPlace());
                itemTable.AddCell(PlaceForSignature("Podpis osoby upoważnionej do odbioru"));

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
