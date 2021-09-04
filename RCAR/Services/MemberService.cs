using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.CarVM;
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

        public async Task<IEnumerable<MemberVM>> GetAllMemberRemovedAsync(string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (user != null)
            {
                if (user.Members.Count() > 0)
                {
                    var member = user.Members.Where(s => s.IsRemoved);
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

        public async Task<MemberDetailVM> DetailMemberAsync(int memberId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var member = user.Members.Where(m => m.MemberId == memberId).FirstOrDefault();
            var model = _mapper.Map<Member, MemberDetailVM>(member);
            return model;
        }

        public async Task<MemberEditVM> GetMemberForEditAsync(int memberId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            var member = await _unitOfWork.Member.FindOneAsync(u => u.UserId == user.Id && u.MemberId == memberId);
            var model = _mapper.Map<Member, MemberEditVM>(member);
            return model;
        }

        public async Task<bool> EditMemberAsync(MemberEditVM model)
        {
            var member = await _unitOfWork.Member.GetByIdAsync(model.MemberId);
            if (model.Status == "Aktywny")
            {
                model.Status = "Aktywny";
                model.IsRemoved = false;
            }
            if (model.Status == "Nieaktywny")
            {
                model.Status = "Nieaktywny";
                model.IsRemoved = true;
            }
            _mapper.Map(model, member);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> RemoveServiceAsync(int memberId)
        {
            var member = await _unitOfWork.Member.GetByIdAsync(memberId);
            member.IsRemoved = true;
            member.Status = "Nieaktywny";
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> BackToDraftAsync(int memberId)
        {
            var member = await _unitOfWork.Member.GetByIdAsync(memberId);
            member.IsRemoved = false;
            member.Status = "Cofnięty";
            return await _unitOfWork.SaveChangesAsync();
        }

        public void CountCars(IEnumerable<CarsVM> model, ref int count)
        {
            count = model.Count();
        }
    }
}
