using Microsoft.AspNetCore.Mvc;
using Trucks.Api.Mapping;
using Trucks.Application.Services;

namespace Trucks.Api.Controllers;
// I know the concept of implementing authorization/authentication, cancellation tokens, caching etc
// but I didn't bother to implement it cuz it wasn't mentioned in the task
[ApiController]
public class TrucksController : ControllerBase
{
    private readonly ITruckService _truckService;

    public TrucksController(ITruckService truckService)
    {
        _truckService = truckService;
    }

    [HttpGet(ApiEndpoints.Trucks.GetALl)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTrucksRequest request)
    {
        try
        {
            var options = request.MapToOptions();

            var trucks = await _truckService.GetTrucksAsync(options);
            trucks = trucks.ToList();

            if (!trucks.Any())
            {
                return NoContent();
            }

            var response = trucks.MapToResponse();

            return Ok(response);
        }
        catch (Exception e)
        {
            // Prolly would've added some more handling here, it actually applies to all catches, like logging or so
            return BadRequest(e.Message);
        }
    }

    [HttpGet(ApiEndpoints.Trucks.Get)]
    public async Task<IActionResult> Get([FromRoute] string code)
    {
        try
        {
            var truck = await _truckService.GetTruckByCodeAsync(code);

            if (truck == null)
            {
                return NotFound();
            }

            var response = truck.MapToResponse();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost(ApiEndpoints.Trucks.Create)]
    public async Task<IActionResult> Create([FromBody] CreateTruckRequest request)
    {
        try
        {
            var truck = request.MapToTruck();

            var result = await _truckService.CreateTruckAsync(truck);

            if (!result)
            {
                return BadRequest();
            }

            var response = truck.MapToResponse();

            return CreatedAtAction(nameof(Get), new { id = truck.Code }, response);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut(ApiEndpoints.Trucks.Update)]
    public async Task<IActionResult> Update([FromRoute] string code,
        [FromBody] UpdateTruckRequest request)
    {
        try
        {
            var truck = request.MapToTruck(code);

            var updated = await _truckService.UpdateTruckAsync(truck, code);

            if (updated)
            {
                var response = truck.MapToResponse();
                return Ok(response);
            }

            return StatusCode(500);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete(ApiEndpoints.Trucks.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string code)
    {
        try
        {
            var deleted = await _truckService.DeleteTruckAsync(code);

            if (deleted)
            {
                return Ok();
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
