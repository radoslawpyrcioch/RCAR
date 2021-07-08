using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Models.ServiceVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCarManager.Helper
{
    public class AutoMaperConfiguration : Profile
    {
        public AutoMaperConfiguration()
        {
            CreateMap<Service, ServiceVM>();
        }
    }
}
