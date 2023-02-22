using Entities.API;
using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;

namespace TheCase2WebPortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                  .AddCookie(config =>
                  {
                      config.Cookie.Name = "TheCase2WebAuth";
                      config.LoginPath = "/Login";
                      config.LogoutPath = "/Login/LogOut";
                      config.Cookie.HttpOnly = true;
                      config.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                      config.AccessDeniedPath = "/Login";
                  });
            services.AddAuthorization();
            services.AddControllersWithViews();
            services.AddHttpClient();
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.Configure<JwtOptions>(Configuration.GetSection("JwtOptions"));
            services.AddSingleton<IHttpClientFactoryService<LoginResult>, HttpClientFactoryService<LoginResult>>();
            services.AddSingleton<IHttpClientFactoryService<List<Police>>, HttpClientFactoryService<List<Police>>>();
            services.AddSingleton<IHttpClientFactoryService<Police>, HttpClientFactoryService<Police>>();
            services.AddSingleton<IHttpClientFactoryService<object>, HttpClientFactoryService<object>>();
            services.AddSingleton<IHttpClientFactoryService<List<person>>, HttpClientFactoryService<List<person>>>();
            services.AddSingleton<IHttpClientFactoryService<person>, HttpClientFactoryService<person>>();
            services.AddSingleton<IHttpClientFactoryService<List<sigortali>>, HttpClientFactoryService<List<sigortali>>>();
            services.AddSingleton<IHttpClientFactoryService<sigortali>, HttpClientFactoryService<sigortali>>();
            services.AddSingleton<IHttpClientFactoryService<List<Tarife>>, HttpClientFactoryService<List<Tarife>>>();
            services.AddSingleton<IHttpClientFactoryService<Tarife>, HttpClientFactoryService<Tarife>>();
            services.AddSingleton<IHttpClientFactoryService<List<Zeyl>>, HttpClientFactoryService<List<Zeyl>>>();
            services.AddSingleton<IHttpClientFactoryService<Zeyl>, HttpClientFactoryService<Zeyl>>();
            services.AddSingleton<IHttpClientFactoryService<List<Zeyltips>>, HttpClientFactoryService<List<Zeyltips>>>();
            services.AddSingleton<IHttpClientFactoryService<Zeyltips>, HttpClientFactoryService<Zeyltips>>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
