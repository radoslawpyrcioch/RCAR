using AutoMapper;
using RCAR.Api.DTOs.PaymentRecordDTOs;
using RCAR.Api.Services.Interfaces;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Services
{
    public class PaymentRecordService : IPaymentRecordService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public PaymentRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreatePaymentAsync(PaymentCreateDTO paymentDTO, int serviceId, string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                if (!user.Services.Where(s => s.ServiceId == paymentDTO.ServiceId && !s.IsRemoved).Any())
                {
                    var payment = new PaymentRecord()
                    {
                        Name = paymentDTO.Name,
                        Description = paymentDTO.Description,
                        Tax = paymentDTO.Tax,
                        TotalAmount = paymentDTO.TotalAmount,
                        NetAmount = paymentDTO.NetAmount,
                        Status = paymentDTO.Status,
                        ServiceId = serviceId
                    };
                    _unitOfWork.PaymentRecord.Add(payment);
                    return await _unitOfWork.SaveChangesAsync();
                }               
            }
            return false;
        }
    }
}
