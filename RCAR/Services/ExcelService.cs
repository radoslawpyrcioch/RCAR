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
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExcelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateReportServiceExcel(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);

            MemoryStream stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var service = user.Services.ToList();
                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceExcelVM>>(service);

                var worksheet = package.Workbook.Worksheets.Add("Raport");
                worksheet.Cells.LoadFromCollection(model, PrintHeaders:true);

                package.Save();       
            }
            return stream.GetBuffer();
        }
    }
}
