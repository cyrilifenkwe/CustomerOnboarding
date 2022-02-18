using CustomerOnboarding.ApplicationService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly ILogger<BanksController> _logger;
        private readonly IBankAppService _bankAppService;

        public BanksController(ILogger<BanksController> logger, IBankAppService bankAppService)
        {
            _logger = logger;
            _bankAppService = bankAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBanks()
        {
            try
            {
                var result = await _bankAppService.GetBanks();
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound("Details not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Failed to retrieve bank details");
            }
        }
    }
}
