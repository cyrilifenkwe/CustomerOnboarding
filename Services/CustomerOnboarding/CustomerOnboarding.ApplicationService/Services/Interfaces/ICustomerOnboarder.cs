using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.Core.Entities;
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
        IEnumerable<Customer> GetAllOnboardedCustomers();

        Customer GetOnboardedCustomerById(long customerId);
        bool UpdateOnboardedCustomer(Customer customerToUpdate);
        bool DeleteOnboardedCustomer(Customer customerToDelet);
        Customer GetOnboardedCustomerByEmail(string customerEmail);
        Customer GetOnboardedCustomerByPhoneNumber(string customerPhoneNumber);
    }
}
