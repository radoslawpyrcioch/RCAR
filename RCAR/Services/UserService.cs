using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Interfaces;
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

    }
}
