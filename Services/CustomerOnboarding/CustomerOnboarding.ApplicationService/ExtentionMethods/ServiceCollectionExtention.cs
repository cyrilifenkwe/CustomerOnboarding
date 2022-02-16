using CustomerOnboarding.ApplicationService.Services.Implementations;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.ExtentionMethods
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddCustomerOnboardingApplicationService(
            this IServiceCollection services)
        {
            services.AddTransient<ICustomerOnboarder,OnboardCustomerAppService>();
            services.AddTransient<IOtpService,OTPAppService>();
            services.AddTransient<IPasswordHasher,PasswordHasher>();
            services.AddTransient<IStateAppService, StateAppService>();
            services.AddTransient<ILgaAppService, LgaAppService>();
            services.AddTransient<IOnboardingStatusAppService, OnboardingStatusAppService>();
            return services;
        }
    }
}
