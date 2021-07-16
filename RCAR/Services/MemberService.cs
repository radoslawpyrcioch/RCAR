﻿using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.MemberVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<MemberVM>> GetAllMemberAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if(user != null)
            {
                if (user.Members.Count() > 0)
                {
                    var member = user.Members.Where(s => !s.IsRemoved);
                    var model = _mapper.Map<IEnumerable<Member>, IEnumerable<MemberVM>>(member);
                    return model;
                }
            }
            return new List<MemberVM>();
        }

        public async Task<bool> CreateMemberAsync(MemberCreateVM model, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (!user.Members.Where(m => m.MemberNo == model.MemberNo && !m.IsRemoved).Any())
            {
                var member = _mapper.Map<MemberCreateVM, Member>(model);
                member.UserId = userId;
                member.Status = "Aktywny";
                _unitOfWork.Member.Add(member);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }
    }
}
