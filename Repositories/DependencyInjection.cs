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
            // Dünya Kupası 2026 Skor Tahmin Ligi
            services.AddScoped<ITakimRepository, TakimRepository>();
            services.AddScoped<IMacRepository, MacRepository>();
            services.AddScoped<ITahminRepository, TahminRepository>();
            services.AddScoped<ILiderlikRepository, LiderlikRepository>();
            services.AddScoped<IUyelikRepository, UyelikRepository>();
            // Maçkolik tarzı modüller
            services.AddScoped<IPuanDurumuRepository, PuanDurumuRepository>();
            services.AddScoped<IOyuncuRepository, OyuncuRepository>();
            services.AddScoped<IGolRepository, GolRepository>();
            services.AddScoped<IOranRepository, OranRepository>();
            services.AddScoped<IHaberRepository, HaberRepository>();
            services.AddScoped<IIstatistikRepository, IstatistikRepository>();
        }
    }
}
