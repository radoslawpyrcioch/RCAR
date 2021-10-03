using Microsoft.AspNetCore.Identity;
using RCAR.Models.UserVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserManageVM> GetUserEmailByIdAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId);
    }
}
