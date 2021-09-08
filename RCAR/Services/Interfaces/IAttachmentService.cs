using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<byte[]> GenerateDoneServiceListAttachmentAsync(string userId);
        Task<byte[]> GenerateActualMemberListAsync(string userId);
        Task<byte[]> GenerateServiceInvoice(int serviceId, string userId);
        Task<byte[]> GenerateMemberCars(int memberId, string userId);
    }
}
