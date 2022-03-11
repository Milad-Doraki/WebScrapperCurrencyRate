
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebScrapperCurrencyRate.Application.CurrencyRates.Queries.GetCurrencyRateByDate;
using Microsoft.Extensions.Logging;

namespace c2.CrudTest.Presentation.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CurrencyRateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Average")]
        public async Task<IActionResult> GetCurrencyAverageRateByDate([FromBody] GetCurrencyAverageRateByDateQuery currencyAverageRateByDateQuery)
        { 
            return Ok(await _mediator.Send(currencyAverageRateByDateQuery));
        } 
    }
}
