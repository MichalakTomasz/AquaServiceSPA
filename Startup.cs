using AquaServiceSPA.DataModel;
using AquaServiceSPA.Models;
using AquaServiceSPA.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AquaServiceSPA
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
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<AquaServiceSPADBContext>(o =>
            o.UseSqlite(Configuration["ConnectionStrings:AquaDB"]));

            services.AddScoped<IAquaCalcService, AquaCalcService>();
            services.AddSingleton(new AquaMacroDefaultSettings());
            services.AddScoped<IEncryptedDataStoreService, EncryptedKeyStoreService>();
            services.AddScoped<IEncryptedDataStoreService, EncryptedEmailSettingsStoreService>();
            services.AddScoped<IGenericCryptographicService, GenericCryptographicService>();
            services.AddScoped<ICryptographicKeyService, CryptographicKeyService>();
            services.AddScoped<IEmailSettingsService, EmailSettingsService>();
            services.AddScoped<IEmailSettingsConverter, EmailSettingsConverter>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailMessageLayoutService, EmailMessageLayoutService>();
            services.AddDataProtection();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IVisitService, VisitService>();
            services.AddTransient<ILoggerService, FileLoggerService>();

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
