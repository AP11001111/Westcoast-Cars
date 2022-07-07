using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

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
        public async Task<ActionResult<List<VehicleViewModel>>> ListVehicles(){
            var response = await _context.Vehicles.ToListAsync();
            var vehicleList = new List<VehicleViewModel>();
            
            foreach (var vehicle in response)
            {
                vehicleList.Add(
                    new VehicleViewModel{
                        VehicleId = vehicle.Id,
                        RegNo = vehicle.RegNo,
                        VehicleName = string.Concat(vehicle.Make, " ", vehicle.Model),
                        ModelYear = vehicle.ModelYear,
                        Mileage = vehicle.Mileage
                    }
                );
            }
            return Ok(vehicleList);
            // return StatusCode(200, "{'message':'Det funkar'}");
        }

        //Metod för att hämta fordon med Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById( int id){
            var response = await _context.Vehicles.FindAsync(id);
            return Ok(response);
            // if (id < 10)
            // {
            //     return StatusCode(400, "Du skickar fel saker");
            //     // return BadRequest("Text");
            // }
            // if (id == 10)
            // {
            //     return StatusCode(404, "Hittar inte det du söker");
            //     // return NotFound("Text");
            // }
            // return StatusCode(200, "{'message':'Det funkar också'}");
            // // return Ok("Text");
        }

        //Metod för att hämta fordon med RegNo
        [HttpGet("byregno/{regNo}")]
        public async Task<ActionResult<Vehicle>> GetVehicleByRegNo( string regNo){
            var response = await _context.Vehicles.SingleOrDefaultAsync(v => v.RegNo!.ToLower() == regNo.ToLower());
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med regNo: {regNo}");
            return Ok(response);
        }

        //Metod för att hämta fordon med RegNo
        [HttpGet("bymake/{make}")]
        public async Task<ActionResult<List<Vehicle>>> GetVehicleByMake( string make){
            var response = await _context.Vehicles.Where(v => v.Make!.ToLower() == make.ToLower()).ToListAsync();
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med märke: {make}");
            return Ok(response);
        }

        // Lägger till ett nytt fordon i systemet...
        [HttpPost()]
        public async Task<ActionResult<Vehicle>> AddVehicle(PostVehicleViewModel vehicle){

            var vehicleToAdd = new Vehicle{
                RegNo = vehicle.RegNo,
                Make = vehicle.Make,
                Mileage = vehicle.Mileage,
                Model = vehicle.Model,
                ModelYear = vehicle.ModelYear
            };
            await _context.Vehicles.AddAsync(vehicleToAdd);
            await _context.SaveChangesAsync();
            return StatusCode(201, vehicleToAdd);
        }

        // Uppdaterar ett befintligt fordon...
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, Vehicle model){
            var response = await _context.Vehicles.FindAsync(id);
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med id: {id}");

            response.Make = model.Make;
            response.Model = model.Model;
            response.Mileage = model.Mileage;
            response.ModelYear = model.ModelYear;
            response.RegNo = model.RegNo;

            _context.Vehicles.Update(response);
            await _context.SaveChangesAsync();

            return NoContent(); //Status kod 204
        }

        // Tar bort ett fordon ifrån systemet
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id){
            var response = await _context.Vehicles.FindAsync(id);
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med id: {id}");
            
            _context.Vehicles.Remove(response);
            await _context.SaveChangesAsync();

            return NoContent(); // Status kod 204
        }
    }
}