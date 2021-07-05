using RCAR.Domain.Interfaces;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services
{
    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentRecordService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
