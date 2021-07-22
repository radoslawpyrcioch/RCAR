﻿using AutoMapper;
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
            if (user.PaymentRecords.Count() > 0)
            {
                var payment = user.PaymentRecords.Where(p => !p.IsRemoved);
                var model = _mapper.Map<IEnumerable<PaymentRecord>, IEnumerable<PaymentVM>>(payment);
                return model;
            }
            return new List<PaymentVM>();
        }


        public async Task<bool> CreatePaymentAsync(PaymentCreateVM paymentVM, int serviceId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (paymentVM != null)
            {
                // var payment = _mapper.Map<PaymentCreateVM, PaymentRecord>(paymentVM);
                if (!user.Services.Where(s => s.ServiceId == paymentVM.ServiceId && !s.IsRemoved).Any())
                {
                    var payment = new PaymentRecord()
                    {
                        Name = paymentVM.Name,
                        Description = paymentVM.Description,
                        Tax = paymentVM.Tax,
                        NetAmount = paymentVM.NetAmount,
                        TotalAmount = paymentVM.TotalAmount,
                        Discount = paymentVM.Discount,
                        Status = paymentVM.Status,
                        ServiceId = serviceId
                    };
                    _unitOfWork.PaymentRecord.Add(payment);
                }
   
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }
    }
}
