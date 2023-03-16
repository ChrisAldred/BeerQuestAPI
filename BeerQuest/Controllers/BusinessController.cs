using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerQuest.Models;
using BeerQuest.Services;

namespace BeerQuest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService ?? throw new ArgumentNullException(nameof(businessService));
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Businesses), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public async Task<ActionResult<Businesses>> Get([FromBody] BusinessRequest request)
        {
            try
            {
                var response = await _businessService.GetBusinessesAsync(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}