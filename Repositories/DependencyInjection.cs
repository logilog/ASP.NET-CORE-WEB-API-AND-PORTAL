using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;

namespace Repositories
{
    public static class RepositoryDIOperations
    {
        /// <summary>
        /// Repository katmanındaki servisler
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<RepositoryContext>();
            services.AddScoped<ISigortaliRepository, SigortaliRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IKullaniciRepository, KullaniciRepository>();
            services.AddScoped<IPoliceRepository, PoliceRepository>();
            services.AddScoped<ITarifeRepository, TarifeRepository>();
            services.AddScoped<IZeylRepository, ZeylRepository>();
            services.AddScoped<IZeyltipsRepository, ZeyltipsRepository>();
        }
    }
}
