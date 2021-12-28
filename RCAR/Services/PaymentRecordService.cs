using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Domain.Interfaces;
using RCAR.Models.PaymentRecordVM;
using RCAR.Models.ServiceVM;
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


        public async Task<bool> CreatePaymentAsync(PaymentCreateVM paymentVM, int serviceId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if (paymentVM != null)
            {
                if (!user.Services.Where(s => s.ServiceId == paymentVM.ServiceId && !s.IsRemoved).Any())
                {
                    var payment = _mapper.Map<PaymentCreateVM, PaymentRecord>(paymentVM);
                    payment.ServiceId = serviceId;
                    payment.Tax = CalculateTaxAmount(paymentVM.Discount, paymentVM.TotalAmount);
                    payment.TotalAmount = CalculateDiscount(paymentVM.Discount, paymentVM.TotalAmount);
                    payment.NetAmount = CalculateNetAmount(paymentVM.Discount, paymentVM.TotalAmount);
                    _unitOfWork.PaymentRecord.Add(payment);
                } 
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public async Task<bool> CreateCarPaymentAsync(PaymentCarCreateVM paymentVM, int carId, string userId)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Id == userId);
            if(paymentVM != null)
            {
                if (!user.Cars.Where(c => c.CarId == paymentVM.CarId && !c.IsRemoved).Any())
                {
                    var payment = _mapper.Map<PaymentCarCreateVM, PaymentRecord>(paymentVM);
                    payment.Tax = CalculateTaxAmount(paymentVM.Discount, paymentVM.TotalAmount);
                    payment.TotalAmount = CalculateDiscount(paymentVM.Discount, paymentVM.TotalAmount);
                    payment.NetAmount = CalculateNetAmount(paymentVM.Discount, paymentVM.TotalAmount);
                    payment.CarId = carId;

                    //var payment = new PaymentRecord()
                    //{
                    //    Name = paymentVM.Name,
                    //    Description = paymentVM.Description,
                    //    Tax = CalculateTaxAmount(paymentVM.Discount, paymentVM.TotalAmount),
                    //    Discount = paymentVM.Discount,
                    //    TotalAmount = CalculateDiscount(paymentVM.Discount, paymentVM.TotalAmount),
                    //    NetAmount = CalculateNetAmount(paymentVM.Discount, paymentVM.TotalAmount),
                    //    Status = paymentVM.Status,
                    //    CarId = carId
                    //};

                    _unitOfWork.PaymentRecord.Add(payment);
                }
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
        }

        public decimal CalculateNetAmount(decimal totalDiscount, decimal totalAmount)
        {
            return (totalAmount - totalDiscount) / 1.23m;  
        }

        public decimal CalculateDiscount(decimal totalDiscount, decimal totalAmount)
        {
            return totalAmount - totalDiscount;
        }

        public decimal CalculateTaxAmount(decimal totalDiscount, decimal totalAmount)
        {
            return totalAmount - CalculateNetAmount(totalDiscount, totalAmount) - totalDiscount;
        }

        public async Task<bool> RemovePaymentAsync(int paymentId, string userId)
        {
            if (userId != null)
            {
                var payment = await _unitOfWork.PaymentRecord.GetByIdAsync(paymentId);
                _unitOfWork.PaymentRecord.Remove(payment);
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;
            
        }

        public async Task<bool> PaidPaymentAsync(int paymentId, string userId)
        {
            if (userId != null)
            {
                var payment = await _unitOfWork.PaymentRecord.GetByIdAsync(paymentId);
                payment.Status = "Zapłacone";
                return await _unitOfWork.SaveChangesAsync();
            }
            return false;        
        }   
    }
}
