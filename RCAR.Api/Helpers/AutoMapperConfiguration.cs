using AutoMapper;
using RCAR.Api.DTOs.ServiceDTOs;
using RCAR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Helpers
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Service, ServiceDTO>();
            CreateMap<Service, ServiceOneDTO>().ReverseMap();
            CreateMap<ServiceCreateDTO, Service>();
        }
    }
}
