using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apis.Data;
using Apis.Infrastructure.Bookings;
using Apis.Infrastructure.Chat;
using Apis.Infrastructure.Client;
using Apis.Infrastructure.Preference;
using Apis.Infrastructure.Vehicles;
using Apis.Repos.Bookings;
using Apis.Repos.Chat;
using Apis.Repos.Client;
using Apis.Repos.Preference;
using Apis.Repos.Vehicles;
using DataAcessLayer.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Apis
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
            services.AddControllers();
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarPoolCN")));
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarPoolCN")));
            services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDBContext>();

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Adding Dependencies For Repos
            services.AddTransient<IVehcileBrand, VehicleBrandRepo>();
            services.AddTransient<IVehicleType_repo, VehicleType_Repo>();
            services.AddTransient<IVehicleColor_repo, VehicleColor_Repo>();
            services.AddTransient<IVehicle_repo, Vehicle_Repo>();

            services.AddTransient<IPreferenceType_Repo, PreferenceType_Repo>();
            services.AddTransient<IPreferenceSubType_Repo, PreferenceSubType_Repo>();

            services.AddTransient<IClient_Repo, Client_Repo>();
            services.AddTransient<IClientVehicle_Repo, ClientVehicle_Repo>();


            services.AddTransient<IBooking_Repo, Booking_Repo>();

            services.AddTransient<IRideApproval_Repo, RideApproval_Repo>();

            services.AddTransient<IBookingCancellation_Repo, BookingCancellation_Repo>();

            //Chat
            services.AddTransient<IChatRoom_Repo, ChatRoom_Repo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
