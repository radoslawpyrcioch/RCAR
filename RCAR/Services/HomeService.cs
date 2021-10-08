using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class HomeService : IHomeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceVM>> GetAllServiceAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
                if (user.Services.Count() > 0)
                {
                    var service = user.Services.ToList();
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceVM>>(service);
                    return model;
                }
            return new List<ServiceVM>();
        }

        public void CountService(IEnumerable<ServiceVM> model, ref int allService, ref int acceptedService, ref int startedService, ref int doneService)
        {
            allService = model.Count();
            acceptedService = model.Where(s => s.Status == "Przyjęty").Count();
            startedService = model.Where(s => s.Status == "Rozpoczęty").Count();
            doneService = model.Where(s => s.Status == "Zakończony").Count();
        }
    }
}
