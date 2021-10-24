using AutoMapper;
using RCAR.Api.DTOs.ServiceDTOs;
using RCAR.Api.Services.Interfaces;
using RCAR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllServiceAsync(string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var service = user.Services.Where(s => s.IsRemoved);
                var dto = _mapper.Map<IEnumerable<ServiceDTO>>(service);
                return dto;
            }
            return null;
        }

        public async Task<ServiceOneDTO> GetOneServiceAsync(string email, int serviceId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var service = user.Services.Where(s => s.ServiceId == serviceId).FirstOrDefault();
                var dto = _mapper.Map<ServiceOneDTO>(service);
                return dto;
            }
            return null;
        }
    }
}
