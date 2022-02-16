﻿using CustomerOnboarding.Core.Dto;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repository.ExtentionMethods
{
    public static class CustomerOnboardingContextExtention
    {
        public static void EnsureDataBaseSeeded(this CustomerOnboardingContext context,
            List<State> states, List<LgaDto> lgas)
        {
            if (!context.States.Any())
            {
                context.AddRange(states);
            }
            context.SaveChanges();

            if (!context.LocalGovernmentAreas.Any())
            {
                var allStates = context.States.ToList();

                foreach (var lga in lgas)
                {
                    var stateExist = allStates.Find(x => x.Name == lga.StateName);
                    if (stateExist != null)
                    {
                        lga.StateCode = stateExist.Id;
                    } 
                }

                List<LocalGovernmentArea> allLgas = new List<LocalGovernmentArea>();
                foreach (var lga in lgas)
                {
                    allLgas.Add(
                        new LocalGovernmentArea
                        {
                            StateId = lga.StateCode,
                            Lga = lga.Lga
                        });
                }
                context.AddRange(allLgas);
            }

            if (!context.OnboardingStatus.Any())
            {
                context.AddRange(new OnboardingStatus { Description = "Pending" },
                                new OnboardingStatus { Description = "Completed" }) ;
            }

            if (!context.Customers.Any())
            {
                context.Add(new Customer
                { 
                    PhoneNumber ="07037808286",
                    DateOnboarded = DateTime.Now,
                    OnboardingStatusId = 2,
                    Email = "cyrilifenkwe@gmail.com",
                    Password = "Passw0r@1Test"
                });
            }
            context.SaveChanges();
        }
    }
}
