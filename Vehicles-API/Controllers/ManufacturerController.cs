using Microsoft.AspNetCore.Mvc;
using Vehicles_API.Interfaces;
using Vehicles_API.ViewModels.Manufacturer;

namespace Vehicles_API.Controllers;
[ApiController]
[Route("api/v1/manufacturers")]
public class ManufacturerController : ControllerBase
{
        private readonly IManufacturerRepository _repo;
    public ManufacturerController(IManufacturerRepository repo)
    {
        _repo = repo;
    }

    [HttpGet()]
    public async Task<ActionResult> ListAllManufacturers(){
        return Ok(await _repo.ListManufacturersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetManufacturerById(int id){
        var response = await _repo.GetManufacturerByIdAsync(id);
        if(response is null) return NotFound($"Vi kunde inte hitta någon manufacturer med id: {id}");            
        return Ok(response);
    }

    [HttpGet("vehicles")]
    public async Task<ActionResult> ListManufacturersAndVehicles(){
        return Ok(await _repo.ListManufacturersVehicles());
    }

    [HttpGet("{id}/vehicles")]
    public async Task<ActionResult> ListManufacturerVehicles(int id){
        var result = await _repo.ListManufacturersVehicles(id);
        if(result is null)
            return NotFound($"Vi kunde inte hitta någon tillverkare med id {id}");
        return Ok();
    }

    [HttpPost()]
    public async Task<ActionResult> AddManufacturer( PostManufacturerViewModel model){
        await _repo.AddManufacturerAsync(model);
        if (await _repo.SaveAllAsync())
        {
            return StatusCode(201);
        }
        return StatusCode(500, "Något gick fel");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateManufacturer(int id, PostManufacturerViewModel model){
        try
        {
            await _repo.UpdateManufacturerAsync(id, model);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            return StatusCode(500,"Något gick fel");
        }
        catch (Exception e)
        {
            
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManufacturer(int id){
        try
        {
            await _repo.DeleteManufacturerAsync(id);
            if (await _repo.SaveAllAsync())
            {
                return NoContent();
            }
            return StatusCode(500,"Något gick fel");
        }
        catch (Exception e)
        {
            
            return StatusCode(500, e.Message);
        }
    }
}