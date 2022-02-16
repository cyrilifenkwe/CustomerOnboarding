using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IOtpService
    {
        bool SendOTP(string phoneNumber);
        bool VerifiedOTP(string phoneNumber);
    }
}
