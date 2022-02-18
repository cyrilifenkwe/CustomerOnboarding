using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Core.Dto
{
    public class BankResponse
    {
        public Bank result { get; set; }
        public string errorMessage { get; set; }
        public string[] errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }

    public class Bank
    {
        public string bankName { get; set; }
        public string bankCode { get; set; }
    }

}
