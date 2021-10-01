using AutoMapper;
using OfficeOpenXml;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.ReportVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string kowalskiUserId = "7b4d87a6-44be-4ef5-962f-7913bd97039c";

        public ExcelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateReportServiceExcel(string exportService, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);

            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var service = new List<Service>();
                if (exportService == "Wszystkie")
                {
                    service = user.Services.ToList();
                }
                else
                {
                    service = user.Services.Where(s => s.Status.Equals(exportService)).ToList();
                }

                if (userId == kowalskiUserId)
                {
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceExcelKowalskiVM>>(service);
                    var worksheet = package.Workbook.Worksheets.Add("Raport");
                    worksheet.Cells.LoadFromCollection(model, PrintHeaders: true);
                }
                else
                {
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceExcelVM>>(service);
                    var worksheet = package.Workbook.Worksheets.Add("Raport");
                    worksheet.Cells.LoadFromCollection(model, PrintHeaders: true);
               }               
                package.Save();
            }
            return stream.GetBuffer();
        }

        public async Task<byte[]> GenerateReportServiceAdministratorExcel(string exportService, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);

            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var service = new List<Service>();
                if (exportService == "Wszystkie")
                {
                    service = user.Services.ToList();
                }
                else
                {
                    service = user.Services.Where(s => s.Status.Equals(exportService)).ToList();
                }
                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceAdministratorExcelVM>>(service);

                var worksheet = package.Workbook.Worksheets.Add("Raport");
                worksheet.Cells.LoadFromCollection(model, PrintHeaders: true);

                package.Save();
            }
            return stream.GetBuffer();
        }

        public async Task<byte[]> GenerateReportServiceCSV(string exportService, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);

            var memoryStreamcsv = new MemoryStream();
            var streamWritercsv = new StreamWriter(memoryStreamcsv, Encoding.UTF8);

            var emptySpace = ",";

            var service = new List<Service>();
            if (exportService == "Wszystkie")
            {
                service = user.Services.ToList();
            }
            else
            {
                service = user.Services.Where(s => s.Status.Equals(exportService)).ToList();
            }

            if (userId == kowalskiUserId)
            {
                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceExcelKowalskiVM>>(service);
                foreach (var item in model)
                {
                    streamWritercsv.WriteLine(item.ServiceNo + emptySpace + item.FirstName + emptySpace
                                             + item.LastName + emptySpace + item.Phone + emptySpace + item.ServiceSince + emptySpace
                                             + item.ServiceTo + emptySpace + item.Description + emptySpace + item.Status + emptySpace 
                                             + item.TotalNetPayment + emptySpace);
                }
            }
            else
            {
                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceExcelVM>>(service);
                foreach (var item in model)
                {
                    streamWritercsv.WriteLine(item.ServiceNo + emptySpace + item.FirstName + emptySpace
                                             + item.LastName + emptySpace + item.Phone + emptySpace + item.ServiceSince + emptySpace
                                             + item.ServiceTo + emptySpace + item.Description + emptySpace + item.Status + emptySpace
                                             + item.PaymentNumber + emptySpace + item.PaymentNetAmount + emptySpace + item.PaymentStatus + emptySpace);
                }
            }
            streamWritercsv.Close();
            memoryStreamcsv.Close();

            return memoryStreamcsv.GetBuffer();
        }
    }
}
