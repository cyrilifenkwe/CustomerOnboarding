using CustomerOnboarding.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IBankAppService
    {
        Task<IEnumerable<BankResponse>> GetBanks();
    }
}
