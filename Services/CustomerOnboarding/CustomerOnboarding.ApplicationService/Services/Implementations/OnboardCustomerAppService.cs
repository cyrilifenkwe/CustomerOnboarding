using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Exceptions;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class OnboardCustomerAppService : ICustomerOnboarder
    {
        private readonly IOtpService _otpService;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<OnboardingStatus> _OnboardingStatusRepository;
        private readonly IRepository<LocalGovernmentArea> _lgaRepository;
        private readonly IRepository<State> _stateRepository;
        private readonly IPasswordHasher _passwordHasher;

        public OnboardCustomerAppService(IOtpService otpService,
            IRepository<Customer> customerRepository,
            IRepository<OnboardingStatus> OnboardingStatusRepository,
            IRepository<LocalGovernmentArea> lgaRepository,
            IRepository<State> stateRepository,
            IPasswordHasher passwordHasher)
        {
            _otpService = otpService;
            _customerRepository = customerRepository;
            _OnboardingStatusRepository = OnboardingStatusRepository;
            _lgaRepository = lgaRepository;
            _stateRepository = stateRepository;
            _passwordHasher = passwordHasher;
        }
        public bool OnboardCustomer(CustomerDto customer) 
        {
            var otpSent = _otpService.SendOTP(customer.PhoneNumber);
            var otpVerified = _otpService.VerifiedOTP(customer.PhoneNumber);

            var stateId = _stateRepository.GetAll()
                .FirstOrDefault(s => s.Name == 
                customer.StateOfResidence).Id;

            var lgaMappedToState = _lgaRepository.GetAll().Where(x => x.Lga == 
                            customer.LGA && x.StateId == stateId).Any();

            if (otpSent && otpVerified && lgaMappedToState)
            {
                var newCustomer = new Customer 
                {
                    PhoneNumber = customer.PhoneNumber,
                    DateOnboarded = DateTime.Now,
                    Email = customer.Email,
                    OnboardingStatusId = GetOnboardingStatusId("Completed"),
                    Password = _passwordHasher.HashPassword(customer.Password)
                };
                _customerRepository.Insert(newCustomer);
                return true;
            }
            else
            {
                var newCustomer = new Customer
                {
                    PhoneNumber = customer.PhoneNumber,
                    DateOnboarded = DateTime.Now,
                    Email = customer.Email,
                    OnboardingStatusId = GetOnboardingStatusId("Pending"),
                    Password = _passwordHasher.HashPassword(customer.Password)
                };
                _customerRepository.Insert(newCustomer);
                return false;
            }
        }

        public IEnumerable<Customer> GetAllOnboardedCustomers()
        {
            var onboardingStatusId = GetOnboardingStatusId("Completed");
            var allOnboardedCustomers = _customerRepository.GetAll()
                    .Where(x => x.OnboardingStatusId == onboardingStatusId);

            return allOnboardedCustomers;
        }
        private long GetOnboardingStatusId(string statusDescription)
        {
            long newOnboardingStatusId = 0;
            long pendingOnboardingStatusId = 0;
            var allOnboardingStatus = _OnboardingStatusRepository.GetAll();

            foreach (var status in allOnboardingStatus)
            {
                if (status.Description == statusDescription)
                {
                    newOnboardingStatusId = status.Id;
                }
                if (status.Description == "Pending")
                {
                    pendingOnboardingStatusId = status.Id;
                }
            }

            return newOnboardingStatusId == 0 ? pendingOnboardingStatusId : newOnboardingStatusId;
        }

        public Customer GetOnboardedCustomerById(long customerId)
        {
            return _customerRepository.Get(customerId);
        }

        public bool UpdateOnboardedCustomer(Customer customerToUpdate)
        {
            _customerRepository.Update(customerToUpdate);
            return true;
        }

        public bool DeleteOnboardedCustomer(Customer customerToDelete)
        {
            _customerRepository.Update(customerToDelete);
            return true;
        }

        public Customer GetOnboardedCustomerByEmail(string customerEmail)
        {
           return  _customerRepository.GetAll()
                .FirstOrDefault(x => x.Email == customerEmail);
        }

        public Customer GetOnboardedCustomerByPhoneNumber(string customerPhoneNumber)
        {
            return _customerRepository.GetAll()
                .FirstOrDefault(x => x.PhoneNumber == customerPhoneNumber);
        }
    }
}
