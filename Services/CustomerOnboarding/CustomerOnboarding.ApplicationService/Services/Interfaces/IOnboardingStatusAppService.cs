using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IOnboardingStatusAppService
    {
        OnboardingStatus GetOnboardingStatusById(long OnboardingStatusId);
        OnboardingStatus GetOnboardingStatusByDescription(string OnboardingStatusDescription);
        bool UpdateOnboardingStatus(OnboardingStatus OnboardingStatusToUpdate);
        bool DeleteOnboardingStatus(OnboardingStatus OnboardingStatusToDelete);
        bool AddOnboardingStatus(OnboardingStatus OnboardingStatusToAdd);
    }
}
