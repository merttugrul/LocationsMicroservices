using Locations.APP.Features.Cities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Locations.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new CityQuery());
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody] CityCreateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put([FromBody] CityUpdateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new CityDeleteCommand { Id = id });
        return Ok(result);
    }

    [HttpGet("GetByCountryId/{countryId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCountryId(int countryId)
    {
        var result = await _mediator.Send(new CityByCountryIdQuery { CountryId = countryId });
        return Ok(result);
    }
}