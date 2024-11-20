using EagleRockService.Features;
using EagleRockService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace EagleRockService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficController : MediatorControllerBase
    {

        public TrafficController(IMediator mediator) : base(mediator)
        {
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Route("Data")]
        public async Task<IActionResult> Data([FromBody] PostDataRequest request)
        {
            var response = await Mediator.Send(request);
            return response.Match(success => NoContentAndLog(), error => ErrorAndLog(),
            notFound => BadRequestAndLog());

        }

        [Route("aggregate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [ProducesResponseType(typeof(List<GetDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromBody] GetDataRequest model)
        {
            var response = await Mediator.Send(model);
            return OkAndLog(response.AsT0);
        }

    }
}