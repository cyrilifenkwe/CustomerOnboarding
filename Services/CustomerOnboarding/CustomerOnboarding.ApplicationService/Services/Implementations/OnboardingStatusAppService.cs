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
    public class OnboardingStatusAppService : IOnboardingStatusAppService
    {
        private readonly IRepository<OnboardingStatus> _onboardingStatusRepository;

        public OnboardingStatusAppService(IRepository<OnboardingStatus> onboardingStatusRepository)
        {
            _onboardingStatusRepository = onboardingStatusRepository;
        }
        public bool AddOnboardingStatus(OnboardingStatus onboardingStatusToAdd)
        {
            _onboardingStatusRepository.Insert(onboardingStatusToAdd);
            return true;
        }

        public bool DeleteOnboardingStatus(OnboardingStatus onboardingStatusToDelete)
        {
            _onboardingStatusRepository.Delete(onboardingStatusToDelete);
            return true;
        }

        public OnboardingStatus GetOnboardingStatusByDescription(string OnboardingStatusDescription)
        {
            return _onboardingStatusRepository.GetAll()
                .SingleOrDefault(x => x.Description == OnboardingStatusDescription);
        }

        public OnboardingStatus GetOnboardingStatusById(long OnboardingStatusId)
        {
            return _onboardingStatusRepository.Get(OnboardingStatusId);
        }

        public bool UpdateOnboardingStatus(OnboardingStatus onboardingStatusToUpdate)
        {
            _onboardingStatusRepository.Update(onboardingStatusToUpdate);
            return true;
        }
    }
}
