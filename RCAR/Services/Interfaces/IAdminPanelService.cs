using RCAR.Models.AdminPanelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IAdminPanelService
    {
        Task<IEnumerable<AdminVM>> GetAllUserAsync();
    }
}
