using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.PaymentRecordVM;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentVM>> GetAllPaymentAsync(int serviceId)
        {
            var user = await _unitOfWork.Service.GetByIdAsync(serviceId);
            if(user.PaymentRecords.Count() > 0)
            {
                var payment = user.PaymentRecords.Where(p => !p.IsRemoved);
                var model = _mapper.Map<IEnumerable<PaymentRecord>, IEnumerable<PaymentVM>>(payment);
                return model;
            }
            return new List<PaymentVM>();
        }
        
    }
}
