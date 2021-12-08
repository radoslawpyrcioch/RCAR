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
        Task<bool> CreateCarPaymentAsync(PaymentCarCreateVM paymentVM, int carId, string userId);
        decimal CalculateTaxAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateNetAmount(decimal totalDiscount, decimal totalAmount);
        decimal CalculateDiscount(decimal totalDiscount, decimal totalAmount);
        Task<bool> RemovePaymentAsync(int paymentId, string userId);
        Task<bool> PaidPaymentAsync(int paymentId, string userId);
    }
}
