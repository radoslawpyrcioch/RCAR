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

        public async Task<IEnumerable<AdminVM>> GetAllUserAsync()
        {
            var user = await _unitOfWork.User.GetAllAsync();
            var model = _mapper.Map<IEnumerable<User>, IEnumerable<AdminVM>>(user);
            return model;
        }
    }
}
