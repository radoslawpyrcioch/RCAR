using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.MemberVM;
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

        public void CountService(IEnumerable<ServiceVM> model, ref int allService, ref int acceptedService, ref int startedService, ref int doneService, ref int backService)
        {
            allService = model.Count();
            acceptedService = model.Where(s => s.Status == "Przyjęty").Count();
            startedService = model.Where(s => s.Status == "Rozpoczęty").Count();
            doneService = model.Where(s => s.Status == "Zakończony").Count();
            backService = model.Where(s => s.Status == "Cofnięty").Count();
        }

        public async Task<IEnumerable<MemberVM>> GetAllMeberAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user.Members.Count > 0)
            {
                var member = user.Members.ToList();
                var model = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberVM>>(member);
                return model;
            }
            return new List<MemberVM>();
        }

        public void CountMember(IEnumerable<MemberVM> model, ref int allMember, ref int activeMember, ref int inactiveMember, ref int backMember)
        {
            allMember = model.Count();
            activeMember = model.Where(m => m.Status == "Aktywny").Count();
            inactiveMember = model.Where(m => m.Status == "Nieaktywny").Count();
            backMember = model.Where(m => m.Status == "Cofnięty").Count();
        }
    }
}
