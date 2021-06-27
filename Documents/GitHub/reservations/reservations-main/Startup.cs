using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using reservation_system.Data;
using reservation_system.Migrations;
using reservation_system.Models;
using reservation_system.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reservation_system
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
           
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseMySql(
                     Configuration.GetConnectionString("Default")));

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddScoped<IReservationTypeService, ReservationTypeService>();
            services.AddIdentity<ReservationUser, IdentityRole>()

                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                
                .AddDefaultTokenProviders();

            services.AddControllersWithViews().AddNToastNotifyNoty(new NToastNotify.NotyOptions()
            {
                ProgressBar = true, // show the progress bar
                Timeout = 5000, // notification will be disapear 
                Theme = "mint" // Notify.Js theme name 
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("readonlypolicy",
                    builder => builder.RequireRole("Admin", "Staff", "Student"));
                options.AddPolicy("ManageReservationpolicy",
                    builder => builder.RequireRole("Admin", "Staff"));
                options.AddPolicy("ManageRolesPolicy",
                    builder => builder.RequireRole("Admin"));
                options.AddPolicy("ReservationPolicy",
                    builder => builder.RequireRole("Student"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseNToastNotify();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
          

                endpoints.MapRazorPages();
            });
        }
    }
}
