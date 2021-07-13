using RCAR.Models.MemberVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberVM>> GetAllMemberAsync(string userId);
    }
}
