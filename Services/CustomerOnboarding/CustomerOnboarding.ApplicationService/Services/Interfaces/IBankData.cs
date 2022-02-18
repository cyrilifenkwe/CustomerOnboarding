using CustomerOnboarding.Core.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IBankData
    {
        [Get("/")]
        Task<IEnumerable<BankResponse>> Get();
    }
}
