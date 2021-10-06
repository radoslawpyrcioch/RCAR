using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Helper.Constans;
using RCAR.Models.UserVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserManageVM> GetUserEmailByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<IdentityUser, UserManageVM>(user);
            return model;
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        }

        public async Task<IdentityResult> DeleteUserAsync(string email, string userId)
        {
            var user = (User)await _userManager.FindByEmailAsync(email);
            if (user.Id == userId)
            {
                if (user.Services.Count > 0)
                {
                    user.Services.ToList().ForEach(s => _unitOfWork.PaymentRecord.RemoveRange(s.PaymentRecords));
                    _unitOfWork.Service.RemoveRange(user.Services);
                    await _unitOfWork.SaveChangesAsync();
                }
                if (user.Members.Count > 0)
                {
                    user.Cars.ToList().ForEach(c => _unitOfWork.PaymentRecord.RemoveRange(c.PaymentRecords));
                    user.Members.ToList().ForEach(m => _unitOfWork.Car.RemoveRange(m.Cars));
                    _unitOfWork.Member.RemoveRange(user.Members);
                    await _unitOfWork.SaveChangesAsync();
                }
                var role = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, role.First());
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError() { Description = IdentityResultErrorsConstans.DELETE_USER_ERROR_FROM_USER_SERVICE });
        }
    }
}
