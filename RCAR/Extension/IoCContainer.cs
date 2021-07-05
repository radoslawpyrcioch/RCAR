using Microsoft.Extensions.DependencyInjection;
using RCAR.Domain.Interfaces;
using RCAR.Domain.Repositories;
using RCAR.Services;
using RCAR.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Extension
{
    public static class IoCContainer
    {
        public static IServiceCollection RepositoryInjector(this IServiceCollection services)
        {
            //inject repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection ServiceInjector(this IServiceCollection services)
        {
            //inject services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IPaymentRecordService, PaymentRecordService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
