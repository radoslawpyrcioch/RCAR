using RCAR.Models.PaymentRecordVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IPaymentRecordService
    {
        Task<IEnumerable<PaymentVM>> GetAllPaymentAsync(int serviceId);
        Task<bool> CreatePaymentAsync(PaymentCreateVM paymentVM, int serviceId, string userId);
        decimal CalculateNetAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateDiscount(decimal totalDiscount, decimal totalAmount);
    }
}
