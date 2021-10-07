using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.AdminPanelVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPanelService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
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

        public async Task<IEnumerable<AdminVM>> GetAllUserAsync()
        {
            var user = await _unitOfWork.User.GetAllAsync();
            var model = _mapper.Map<IEnumerable<User>, IEnumerable<AdminVM>>(user);
            return model;
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }
    }
}
