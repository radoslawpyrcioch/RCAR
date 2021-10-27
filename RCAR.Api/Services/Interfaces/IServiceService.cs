using RCAR.Api.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetAllServiceAsync(string email);
        Task<IEnumerable<ServiceDTO>> GetAllRemovedServiceAsync(string email);
        Task<ServiceOneDTO> GetOneServiceAsync(string email, int serviceId);
        Task<ServiceOneDTO> CreateServiceAsync(ServiceCreateDTO dto, string email);
        Task<bool> EditServiceAsync(ServiceOneDTO dto, string email);
        Task<bool> RemoveServiceAsync(string email, int serviceId);
    }
}
