using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Helper;
using RCAR.Models.MemberVM;
using RCAR.Models.ServiceVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<byte[]> GenerateActualMemberListAsync(string userId)
        {
            var member = await _unitOfWork.Member.FindAllAsync(m => m.UserId == userId & !m.IsRemoved);
            var memberVM = _mapper.Map<IEnumerable<MemberVM>>(member);
            var pdfHelper = new PdfDocumentMember(memberVM);

            return pdfHelper.Generate();
        }

        public async Task<byte[]> GenerateDoneServiceListAttachmentAsync(string userId)
        {
            var service = await _unitOfWork.Service.FindAllAsync(s => s.UserId == userId & s.IsRemoved);
            var serviceVM = _mapper.Map<IEnumerable<ServiceVM>>(service);
            var pdfHelper = new PdfDocumentServiceRemoved(serviceVM);

            return pdfHelper.Generate();
        }

        public async Task<byte[]> GenerateServiceInvoice(int serviceId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(s => s.Id == userId);
            var service = user.Services.Where(s => s.ServiceId == serviceId && !s.IsRemoved).FirstOrDefault();
            var serviceVM = _mapper.Map<Service, ServiceDetailVM>(service);
            var pdfHelper = new PdfDocumentServiceInvoice(serviceVM);

            return pdfHelper.Generate();
        }
        public async Task<byte[]> GenerateMemberCars(int memberId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(s => s.Id == userId);
            var member = user.Members.Where(s => s.MemberId == memberId).FirstOrDefault();
            var memberVM = _mapper.Map<Member, MemberDetailVM>(member);
            var pdfHelper = new PdfDocumentMemberCars(memberVM);

            return pdfHelper.Generate();
        }
    }
}
