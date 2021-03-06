using Microsoft.Extensions.DependencyInjection;
using RCAR.Api.Services;
using RCAR.Api.Services.Interfaces;
using RCAR.Domain.Interfaces;
using RCAR.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCAR.Api.Extension
{
    public static class IoCContainer
    {
        public static IServiceCollection RepositoryInjector(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection ServiceInjector(this IServiceCollection services)
        {
            services.AddScoped<IJwTokenService, JwTokenService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IPaymentRecordService, PaymentRecordService>();

            return services;
        }
    }
}
