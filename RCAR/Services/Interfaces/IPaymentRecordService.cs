using RCAR.Models.PaymentRecordVM;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IPaymentRecordService
    {
        Task<bool> CreatePaymentAsync(PaymentCreateVM paymentVM, int serviceId, string userId);
        decimal CalculateNetAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateDiscount(decimal totalDiscount, decimal totalAmount);
        Task<bool> RemovePaymentAsync(int paymentId);
    }
}
