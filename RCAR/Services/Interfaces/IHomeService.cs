using RCAR.Models.MemberVM;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<ServiceVM>> GetAllServiceAsync(string userId);
        void CountService(IEnumerable<ServiceVM> model, ref int allService, ref int acceptedService, ref int startedService, ref int doneService, ref int backService);

        Task<IEnumerable<MemberVM>> GetAllMeberAsync(string userId);
        void CountMember(IEnumerable<MemberVM> model, ref int allMember, ref int activeMember, ref int inactiveMember, ref int backMember);
    }
}
