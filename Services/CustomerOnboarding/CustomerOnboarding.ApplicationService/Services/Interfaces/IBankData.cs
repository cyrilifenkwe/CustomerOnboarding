using CustomerOnboarding.Core.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IBankData
    {
        [Get("/GetAllBanks")]
        Task<IEnumerable<BankResponse>> Get();
    }
}
