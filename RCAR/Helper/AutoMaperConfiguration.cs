using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RCAR.Domain.Entities;
using RCAR.Models.AdminPanelVM;
using RCAR.Models.CarVM;
using RCAR.Models.MemberVM;
using RCAR.Models.PaymentRecordVM;
using RCAR.Models.ReportVM;
using RCAR.Models.ServiceVM;
using RCAR.Models.UserVM;
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
            CreateMap<MemberCreateVM, Member>();
            CreateMap<Member, MemberDetailVM>();
            CreateMap<Member, MemberEditVM>().ReverseMap();

            CreateMap<PaymentRecord, PaymentVM>();
            CreateMap<PaymentRecord, PaymentCreateVM>().ReverseMap();
            CreateMap<PaymentRecord, PaymentCarCreateVM>().ReverseMap();

            CreateMap<Car, CarsVM>();
            CreateMap<Car, CarDetailVM>();

            CreateMap<PaymentRecord, ReportPaymentVM>();
            CreateMap<Service, ReportVM>();
            CreateMap<Service, ReportServiceVM>().ForMember(dest => dest.PaymentNetAmount, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().NetAmount))
                                                 .ForMember(dest => dest.PaymentNumber, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().Name))
                                                 .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().Status))
                                                 .ForMember(dest => dest.TotalNetPayment, opt => opt.MapFrom(src => src.PaymentRecords.Sum(n => n.NetAmount)));
            CreateMap<Service, ReportServiceExcelVM>().ForMember(dest => dest.PaymentNetAmount, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().NetAmount))
                                                      .ForMember(dest => dest.PaymentNumber, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().Name))
                                                      .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentRecords.LastOrDefault().Status));
            CreateMap<Service, ReportServiceAdministratorVM>().ForMember(dest => dest.PaymentNetAmount, opt => opt.MapFrom(src => src.PaymentRecords.Sum(x => x.NetAmount)));
            CreateMap<Service, ReportServiceAdministratorExcelVM>().ForMember(dest => dest.PaymentNetAmount, opt => opt.MapFrom(src => src.PaymentRecords.Sum(x => x.NetAmount)));
            CreateMap<Service, ReportServiceExcelKowalskiVM>().ForMember(dest => dest.TotalNetPayment, opt => opt.MapFrom(src => src.PaymentRecords.Sum(x => x.NetAmount)));

            CreateMap<IdentityUser, UserManageVM>();
            CreateMap<User, AdminVM>();
        }
    }
}
