using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using RCAR.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM model);
        Task<SignInResult> LogInAsync(LogInVM model);
        Task<bool> LogOutAsync();
        Task<User> GetUserByEmailAsync(string model);
        Task<string> GenerateConfirmTokenAsync(User user);
        Task<IdentityResult> ConfirmEmailAsync(string token, string userId);
        Task<string> GenerateResetPasswordTokenAsync(ForgotPasswordVM model);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model);
    }
}
