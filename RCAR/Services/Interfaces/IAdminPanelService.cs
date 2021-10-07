using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
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
        Task<IdentityUser> GetUserByEmailAsync(string userEmail);
        Task<IdentityResult> DeleteUserAsync(User user);
    }
}
