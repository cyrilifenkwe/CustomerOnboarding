﻿using CustomerOnboarding.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repository.ExtentionMethods
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddCustomerOnboardingRepositoryServices(
            this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<CustomerOnboardingContext>(options =>            
            options.UseSqlServer(configuration["CustomerOnboardingConnectionString"])) ;


            return services;
        }
    }
}
