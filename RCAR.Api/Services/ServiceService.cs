using AutoMapper;
using RCAR.Api.DTOs.ServiceDTOs;
using RCAR.Api.Services.Interfaces;
using RCAR.Domain.Entities;
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
        public async Task<ServiceOneDTO> CreateServiceAsync(ServiceCreateDTO dto, string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                if (!user.Services.Any(s => s.ServiceNo == dto.ServiceNo))
                {
                    var service = _mapper.Map<Service>(dto);
                    service.UserId = user.Id;
                    _unitOfWork.Service.Add(service);
                    return await GetOneServiceAsync(email, service.ServiceId);
                }
            }
            return null;
        }

        public async Task<bool> EditServiceAsync(ServiceOneDTO dto, string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var service = user.Services.Where(s => s.ServiceId == dto.ServiceId).FirstOrDefault();
                _mapper.Map(dto, service);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<bool> RemoveServiceAsync(string email, int serviceId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var service = user.Services.Where(s => s.ServiceId == serviceId).FirstOrDefault();
                if (service != null)
                {
                    service.IsRemoved = false;
                    return await _unitOfWork.SaveChangesAsync();
                }
            }
            return false;
        }
    }
}
