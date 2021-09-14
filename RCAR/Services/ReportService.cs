using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.ReportVM;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportServiceVM>> GetAllServiceWithLastPaymentAsync(string sortOrder, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user != null)
            {
                if (user.Services.Count() > 0)
                {
                    switch (sortOrder)
                    {
                        case "Zakończone":
                            {
                                var service = user.Services.Where(s => s.Status == "Zakończony");
                                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                                return model;
                            }
                        case "Rozpoczęty":
                            {
                                var service = user.Services.Where(s => s.Status == "Rozpoczęty");
                                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                                return model;
                            }
                        case "Cofnięty":
                            {
                                var service = user.Services.Where(s => s.Status == "Cofnięty");
                                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                                return model;
                            }
                        case "Wszystkie":
                            {
                                var service = user.Services.ToList();
                                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                                return model;
                            }

                        default:
                            {
                                var service = user.Services.ToList();
                                var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                                return model;
                            }
                    }

                }
            }
            return new List<ReportServiceVM>();
        }  
    }
}
