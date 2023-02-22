using Business.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

namespace Business.DependencyInjection
{
    public static class BusinessDIOperations
    {
        /// <summary>
        /// Business katmanındaki servisler
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddRepositoryServices();
            services.AddScoped<ISigortaliService, SigortaliService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IKullaniciService, KullaniciService>();
            services.AddScoped<IPoliceService, PoliceService>();
            services.AddScoped<ITarifeService, TarifeService>();
            services.AddScoped<IZeylService, ZeylService>();
            services.AddScoped<IZeyltipsService, ZeyltipsService>();
        }
    }
}
