using RCAR.Api.DTOs.PaymentRecordDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Services.Interfaces
{
    public interface IPaymentRecordService
    {
        Task<bool> CreatePaymentAsync(PaymentCreateDTO paymentDTO, int serviceId, string email);
        decimal CalculateTaxAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateNetAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateDiscount(decimal totalDiscount, decimal totalAmount);
    }
}
