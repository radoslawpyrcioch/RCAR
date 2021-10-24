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
        Task<ServiceOneDTO> GetOneServiceAsync(string email, int serviceId);
        Task<ServiceOneDTO> CreateServiceAsync(ServiceCreateDTO dto, string email);
    }
}
