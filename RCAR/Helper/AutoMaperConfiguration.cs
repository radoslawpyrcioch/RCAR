using AutoMapper;
using RCAR.Domain.Entities;
using RCAR.Models.MemberVM;
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
            CreateMap<ServiceCreateVM, Service>();
            CreateMap<Service, ServiceDetailVM>();
            CreateMap<Service, ServiceEditVM>().ForMember(dest => dest.CarModel, opt => opt.MapFrom(src => src.Model)).ReverseMap();

            CreateMap<Member, MemberVM>();
        
        }
    }
}
