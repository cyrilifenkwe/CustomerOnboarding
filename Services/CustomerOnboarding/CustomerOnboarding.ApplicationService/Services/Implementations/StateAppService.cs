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
    public class StateAppService : IStateAppService
    {
        private readonly IRepository<State> _stateRepository;

        public StateAppService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public bool AddState(State stateToAdd)
        {
            _stateRepository.Insert(stateToAdd);
            return true;
        }

        public bool DeleteState(State stateToDelete)
        {
            _stateRepository.Delete(stateToDelete);
            return true;
        }

        public State GetStateById(long stateId)
        {
            return _stateRepository.Get(stateId);
        }

        public State GetStateByName(string stateName)
        {
            return _stateRepository.GetAll().FirstOrDefault(x => x.Name == stateName);
        }

        public bool UpdateState(State stateToUpdate)
        {
            _stateRepository.Update(stateToUpdate);
            return true;
        }
    }
}
