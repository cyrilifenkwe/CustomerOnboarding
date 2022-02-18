using AutoMapper;
using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerOnboarding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalGovernmentAreasController : ControllerBase
    {
        private readonly ILgaAppService _lgaAppService;
        private readonly IMapper _mapper;
        private readonly IStateAppService _stateAppService;
        private readonly ILogger<LocalGovernmentAreasController> _logger;

        public LocalGovernmentAreasController(ILgaAppService lgaAppService,IMapper mapper,
            IStateAppService stateAppService,ILogger<LocalGovernmentAreasController> logger)
        {
            _lgaAppService = lgaAppService;
            _mapper = mapper;
            _stateAppService = stateAppService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _lgaAppService.GetAllLgas());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var result = await _lgaAppService.GetLgaById(id);

                if (result == null)
                {
                    return NotFound($"Local government with id {id} was not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }

        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Invalid local government name");
                }

                var result = await _lgaAppService.GetLgaByLgaName(name);

                if (result == null)
                {
                    return NotFound($"Local government with name {name} was not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LgasDto lgaToAdd)
        {
            try
            {
                return Ok(await _lgaAppService.AddLga(lgaToAdd));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id)
        {
            try
            {
                return Ok(await _lgaAppService.UpdatLga(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                return Ok(await _lgaAppService.DeleteLga(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error occurred while processing request, please contact syatem administrator");
            }
        }
    }
}
