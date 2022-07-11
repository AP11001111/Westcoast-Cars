using Microsoft.AspNetCore.Mvc;

namespace Vehicles_API.Controllers;
[ApiController]
[Route("api/v1/manufacturers")]
public class ManufacturerController : ControllerBase
{
    [HttpGet()]
    public async Task<ActionResult> ListAllManufacturers(){
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetManufacturerById(int id){
        return Ok();
    }

    [HttpPost()]
    public async Task<ActionResult> AddManufacturer(){
        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateManufacturer(int id){
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManufacturer(int id){
        return NoContent();
    }
}