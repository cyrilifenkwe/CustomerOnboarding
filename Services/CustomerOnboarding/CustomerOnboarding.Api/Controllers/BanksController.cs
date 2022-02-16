using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly ILogger<BanksController> _logger;

        public BanksController(ILogger<BanksController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult GetBanks()
        //{

        //}
    }
}
