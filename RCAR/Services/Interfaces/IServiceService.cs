using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceVM>> GetAllServiceAsync(string userId);
        Task<IEnumerable<ServiceVM>> GetAllServiceRemovedAsync(string userId);
        Task<bool> CreateServiceAsync(ServiceCreateVM model, string userId);
        Task<ServiceDetailVM> DetailServiceAsync(int serviceId, string userId);
        Task<bool> RemoveServiceAsync(int serviceId);
        

    }
}
