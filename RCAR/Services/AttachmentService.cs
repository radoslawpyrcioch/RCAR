using AutoMapper;
using RCAR.Domain.Interfaces;
using RCAR.Helper;
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

        public async Task<byte[]> GenerateDoneServiceListAttachmentAsync(string userId)
        {
            var service = await _unitOfWork.Service.FindAllAsync(s => s.UserId == userId & s.IsRemoved);
            var serviceVM = _mapper.Map<IEnumerable<ServiceVM>>(service);
            var pdfHelper = new PdfDocumentServiceRemoved(serviceVM);

            return pdfHelper.Generate();
        }
    }
}
