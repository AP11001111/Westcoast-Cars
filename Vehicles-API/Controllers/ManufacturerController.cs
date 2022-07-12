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
    public async Task<ActionResult> UpdateManufacturer(int id){
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteManufacturer(int id){
        return NoContent();
    }
}