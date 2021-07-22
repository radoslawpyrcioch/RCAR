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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceVM>> GetAllServiceAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if(user != null)
            {
                if(user.Services.Count() > 0)
                {
                    var service = user.Services.Where(s => !s.IsRemoved);
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceVM>>(service);
                    return model;
                }  
            }
            return new List<ServiceVM>();
        }

        public async Task<IEnumerable<ServiceVM>> GetAllServiceRemovedAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user != null)
            {
                if (user.Services.Count() > 0)
                {
                    var service = user.Services.Where(s => s.IsRemoved);
                    var model = _mapper.Map<IEnumerable<Service>, IEnumerable<ServiceVM>>(service);
                    return model;
                }
            }
            return new List<ServiceVM>();
        }

        public async Task<bool> CreateServiceAsync(ServiceCreateVM model, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (!user.Services.Where(s => s.ServiceNo == model.ServiceNo && !s.IsRemoved).Any())
            {
                var service = _mapper.Map<ServiceCreateVM, Service>(model);
                service.UserId = userId;
                _unitOfWork.Service.Add(service);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false; 
        }

        public async Task<ServiceDetailVM> DetailServiceAsync(int serviceId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var service = user.Services.Where(s => s.ServiceId == serviceId && !s.IsRemoved).FirstOrDefault();
            var model = _mapper.Map<Service, ServiceDetailVM>(service);
            return model;
        }

        public async Task<bool> RemoveServiceAsync(int serviceId)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(serviceId);
            service.IsRemoved = true;
            service.Status = "Zakończony";
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> BackServiceAsync(int serviceId)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(serviceId);
            service.IsRemoved = false;
            service.Status = "Cofnięty";
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ServiceEditVM> GetServiceForEditAsync(int serviceId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var service = await _unitOfWork.Service.FindOneAsync(u => u.UserId == user.Id && u.ServiceId == serviceId);
            var model = _mapper.Map<Service, ServiceEditVM>(service);
            return model;
        }

        public async Task<bool> EditServiceAsync(ServiceEditVM model)
        {
            var service = await _unitOfWork.Service.GetByIdAsync(model.ServiceId);
            if (model.Status == "Przyjęty")
            {
                model.Status = "Przyjęty";
            }
            if (model.Status == "Rozpoczęty")
            {
                model.Status = "Rozpoczęty";
            }
            _mapper.Map(model, service);
            return await _unitOfWork.SaveChangesAsync();



        }
    }
}
