using AutoMapper;
using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerOnboarder _customerOnboarder;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMapper mapper,ICustomerOnboarder customerOnboarder,
            ILogger<CustomersController> logger)
        {
            _mapper = mapper;
            _customerOnboarder = customerOnboarder;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public IActionResult OnboardACustomer(CustomerDto newCustomer) 
        {
            try
            {
                var customerHasBeenOnboarded = _customerOnboarder.OnboardCustomer(newCustomer);
                if (customerHasBeenOnboarded)
                {
                    return Ok("Successfully onboarded customer.");
                }
                return StatusCode(500, "Customer onboarding process failed");
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("[action]")]
        public IActionResult GetAllOnboardedCustomers()
        {
            try
            {
                var onBoardedCustomers = _customerOnboarder.GetAllOnboardedCustomers();
                if (onBoardedCustomers == null)
                {
                    return NotFound("Unable to retrieve details for onboarded customers");
                }

                List<OnboardedCustomerDto> mappedToOnboardedCustomerDto = 
                                            new List<OnboardedCustomerDto>();
                foreach (var customer in onBoardedCustomers)
                {
                    mappedToOnboardedCustomerDto.Add(_mapper.Map<OnboardedCustomerDto>(customer));
                }

                return Ok(mappedToOnboardedCustomerDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
