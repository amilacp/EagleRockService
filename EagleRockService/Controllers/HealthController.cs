using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EagleRockService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : MediatorControllerBase
    {
        public HealthController(IMediator mediator) : base(mediator)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetHealthRequest());
            return OkAndLog(response);
        }
    }
}
