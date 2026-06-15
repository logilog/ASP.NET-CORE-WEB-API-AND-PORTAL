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
            // Dünya Kupası 2026 Skor Tahmin Ligi
            services.AddScoped<ITakimService, TakimService>();
            services.AddScoped<ITahminService, TahminService>();
            services.AddScoped<IMacService, MacService>();
            services.AddScoped<ILiderlikService, LiderlikService>();
            services.AddScoped<IUyelikService, UyelikService>();
            // Maçkolik tarzı modüller
            services.AddScoped<IPuanDurumuService, PuanDurumuService>();
            services.AddScoped<IOyuncuService, OyuncuService>();
            services.AddScoped<IGolService, GolService>();
            services.AddScoped<IOranService, OranService>();
            services.AddScoped<IHaberService, HaberService>();
            services.AddScoped<IIstatistikService, IstatistikService>();
        }
    }
}
