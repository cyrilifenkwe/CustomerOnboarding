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
    public class LgaAppService : ILgaAppService
    {
        private readonly IRepository<LocalGovernmentArea> _lgaRepository;

        public LgaAppService(IRepository<LocalGovernmentArea> lgaRepository)
        {
            _lgaRepository = lgaRepository;
        }


        public bool AddLga(LocalGovernmentArea lgaToBeAdded)
        {
            _lgaRepository.Insert(lgaToBeAdded);
            return true;
        }

        public bool DeleteLga(LocalGovernmentArea lgaToBeDeleted)
        {
            _lgaRepository.Delete(lgaToBeDeleted);
            return true;
        }

        public LocalGovernmentArea GetLgaById(long lgaId)
        {
            return _lgaRepository.Get(lgaId);
        }

        public IEnumerable<LocalGovernmentArea> GetStateId(long stateId)
        {
            return _lgaRepository.GetAll().Where(x => x.StateId == stateId);
        }

        public bool UpdatLga(LocalGovernmentArea lgaToBeUpdated)
        {
            _lgaRepository.Update(lgaToBeUpdated);
            return true;
        }
    }
}
