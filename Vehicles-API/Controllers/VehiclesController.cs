using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Models;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleContext _context;
        public VehiclesController(VehicleContext context)
        {
            _context = context;            
        }

        //Metod för att hämta alla fordon
        [HttpGet()]
        public async Task<ActionResult<List<Vehicle>>> ListVehicles(){
            var response = await _context.Vehicles.ToListAsync();
            return Ok(response);
            // return StatusCode(200, "{'message':'Det funkar'}");
        }

        //Metod för att hämta fordon med Id
        [HttpGet("{id}")]
        public ActionResult GetVehicleById( int id){
            if (id < 10)
            {
                return StatusCode(400, "Du skickar fel saker");
                // return BadRequest("Text");
            }
            if (id == 10)
            {
                return StatusCode(404, "Hittar inte det du söker");
                // return NotFound("Text");
            }
            return StatusCode(200, "{'message':'Det funkar också'}");
            // return Ok("Text");
        }

        // Lägger till ett nytt fordon i systemet...
        [HttpPost()]
        public async Task<ActionResult<Vehicle>> AddVehicle(Vehicle vehicle){
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return StatusCode(201, vehicle);
        }

        // Uppdaterar ett befintligt fordon...
        [HttpPut("{id}")]
        public ActionResult UpdateVehicle(int id){
            return NoContent(); //Status kod 204
        }

        // Tar bort ett fordon ifrån systemet
        [HttpDelete("{id}")]
        public ActionResult DeleteVehicle(int id){
            return NoContent(); // Status kod 204
        }
    }
}