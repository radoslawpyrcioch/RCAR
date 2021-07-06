using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Helper.Constans;
using RCAR.Models.Account;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LogInAsync(LogInVM model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
        }

        public async Task<bool> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM model)
        {
            var user = new User() { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Customer");
            return result;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.User.FindOneAsync(u => u.Email == email);
        }

        public async Task<string> GenerateConfirmTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<string> GenerateResetPasswordTokenAsync(ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return await _userManager.GeneratePasswordResetTokenAsync(user);
            return null;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
                return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return IdentityResult.Failed(new IdentityError() { Description = IdentityResultErrorsConstans.RESET_PASSWORD_USER_NOT_FOUND });
        }
    }
}
