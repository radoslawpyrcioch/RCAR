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

        public async Task<IEnumerable<ReportServiceVM>> GetAllServiceWithLastPaymentAsync(string filterService, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user != null)
            {
                if (user.Services.Count() > 0)
                {
                    var service = new List<Service>();
                    if (filterService == "Wszystkie")
                    {                                              
                        service = user.Services.ToList();
                    }
                    else
                    {
                        service = user.Services.Where(s => s.Status.Equals(filterService)).ToList();
                    }
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceVM>>(service);
                    return model;
                }
            }
            return new List<ReportServiceVM>();
        }

        public async Task<IEnumerable<ReportServiceAdministratorVM>> GetAllServiceWithTotalPaymentNetAmount(string filterService, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user != null)
            {
                if (user.Services.Count() > 0)
                {
                    var service = new List<Service>();
                    if (filterService == "Wszystkie")
                    {
                        service = user.Services.ToList();
                    }
                    else
                    {
                        service = user.Services.Where(s => s.Status.Equals(filterService)).ToList();
                    }
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ReportServiceAdministratorVM>>(service);
                    return model;
                }
            }
            return new List<ReportServiceAdministratorVM>();
        }
    }
}
