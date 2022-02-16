using CustomerOnboarding.ApplicationService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface ICustomerOnboarder
    {
        bool OnboardCustomer(CustomerDto customer);
    }
}
