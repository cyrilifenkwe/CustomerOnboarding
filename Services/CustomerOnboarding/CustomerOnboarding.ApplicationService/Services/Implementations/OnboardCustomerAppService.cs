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
        private readonly IRepository<OnboardingStatus> _OnboardingStatusFromDb;
        private readonly IRepository<LocalGovenmentArea> _lga;
        private readonly IRepository<State> state;
        private readonly IPasswordHasher _passwordHasher;

        public OnboardCustomerAppService(IOtpService otpService,
            IRepository<Customer> customerRepository,
            IRepository<OnboardingStatus> OnboardingStatusFromDb,
            IRepository<LocalGovenmentArea> lga,
            IRepository<State> state,
            IPasswordHasher passwordHasher)
        {
            _otpService = otpService;
            _customerRepository = customerRepository;
            _OnboardingStatusFromDb = OnboardingStatusFromDb;
            _lga = lga;
            this.state = state;
            _passwordHasher = passwordHasher;
        }
        public bool OnboardCustomer(CustomerDto customer) 
        {
            var otpSent = _otpService.SendOTP(customer.PhoneNumber);
            var otpVerified = _otpService.VerifiedOTP(customer.PhoneNumber);

            var stateId = state.GetAll()
                .FirstOrDefault(s => s.Name == 
                customer.StateOfResidence).Id;

            var lgaMappedToState = _lga.GetAll().Where(x => x.Lga == 
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

        private long GetOnboardingStatusId(string statusDescription)
        {
            long newOnboardingStatusId = 0;
            long pendingOnboardingStatusId = 0;
            var allOnboardingStatus = _OnboardingStatusFromDb.GetAll();

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
    }
}
