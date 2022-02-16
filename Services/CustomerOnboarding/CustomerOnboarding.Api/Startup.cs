using CsvHelper;
using CustomerOnboarding.Api.Mappers;
using CustomerOnboarding.Core.Dto;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repository.DbContexts;
using CustomerOnboarding.Repository.ExtentionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api
{
    public class Startup
    {
        private readonly IHostEnvironment _hostEnvironment;
        private List<State> statesRecords = new List<State>();
        private List<LgaDto> lgaRecords = new List<LgaDto>();

        public Startup(IConfiguration configuration,IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerOnboarding.Api", Version = "v1" });
            });
            
            var stateFile = Path.Combine(_hostEnvironment.ContentRootPath,"Files\\state.csv");
            
            using (var reader = new StreamReader(stateFile, Encoding.Default))
            using (var csv = new CsvReader(reader))
            {
                object p = csv.Configuration.RegisterClassMap<StateCsvMap>();
                statesRecords = csv.GetRecords<State>().ToList();
                statesRecords.OrderBy(x => x.Name);
            }

            var lgaFile = Path.Combine(_hostEnvironment.ContentRootPath, "Files\\lga.csv");
            using (var reader = new StreamReader(lgaFile, Encoding.Default))
            using (var csv = new CsvReader(reader))
            {
                object p = csv.Configuration.RegisterClassMap<LgaCsvMap>();
                lgaRecords = csv.GetRecords<LgaDto>().ToList();
                lgaRecords.OrderBy(x => x.Lga);
            }

            services.AddCustomerOnboardingRepositoryServices(Configuration);
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

            app.UseAuthorization();

            using(var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<CustomerOnboardingContext>();
                context.Database.Migrate();
                context.EnsureDataBaseSeeded(statesRecords, lgaRecords);
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerOnboarding.Api v1"));


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
