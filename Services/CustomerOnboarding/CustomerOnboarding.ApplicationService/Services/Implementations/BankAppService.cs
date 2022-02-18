using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class BankAppService : IBankAppService
    {
        private readonly IBankData _bankData;

        public BankAppService(IBankData bankData)
        {
            _bankData = bankData;
        }
        public async Task<IEnumerable<BankResponse>> GetBanks()
        {
            return await _bankData.Get();
        }
    }
}
