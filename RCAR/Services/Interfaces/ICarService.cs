using RCAR.Models.CarVM;
using RCAR.Models.PaymentRecordVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface ICarService
    {
        Task<bool> CreateCarAsync(CarCreateVM carVM, int memberId, string userId);
        Task<bool> RemoveCarAsync(int carId);
        Task<bool> DoneCarSeriveAsync(int carId);
        Task<CarDetailVM> DetailCarAsync(int carId);
        Task<bool> InProgressCarStatusAsync(int carId);
        void CountPayment(IEnumerable<PaymentVM> model, ref int count, ref decimal totalPayment);
    }
}
