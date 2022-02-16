using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface ILgaAppService
    {
        LocalGovernmentArea GetLgaById(long lgaId);
        IEnumerable<LocalGovernmentArea> GetStateId(long stateId);
        bool DeleteLga(LocalGovernmentArea lgaToBeDeleted);
        bool UpdatLga(LocalGovernmentArea lgaToBeUpdated);
        bool AddLga(LocalGovernmentArea lgaToBeAdded);
    }
}
