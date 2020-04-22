using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ddd_assessment.DataContracts;
using ddd_assessment.DataManager;
using ddd_assessment.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ddd_assessment.Utils;
using ddd_assessment.Domain;

namespace ddd_assessment
{
    public class CustomerManagement
    {
        public CustomerManagement(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IUserDatamanager, IUserDataManager>();
            services.AddScoped<IBalance, Balance>();
            services.AddScoped<IMoney, Money>();
            services.InitializeDapperConnectionString(Configuration.GetConnectionString("DefaultConnection"));
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
