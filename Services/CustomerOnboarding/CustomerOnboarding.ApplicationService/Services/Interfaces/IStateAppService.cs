using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IStateAppService
    {
        State GetStateById(long stateId);
        State GetStateByName(string stateName);
        bool UpdateState(State stateToUpdate);
        bool DeleteState(State stateToDelete);
        bool AddState(State stateToAdd);
    }
}
