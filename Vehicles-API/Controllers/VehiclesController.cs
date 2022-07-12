using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        public VehiclesController(IVehicleRepository vehicleRepo, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;          
        }

        //Metod för att hämta alla fordon
        [HttpGet()]
        public async Task<ActionResult<List<VehicleViewModel>>> ListVehicles(){
            // var response = await _context.Vehicles.ToListAsync();
            // return await _vehicleRepo.ListAllVehiclesAsync();
            // var vehicleList = new List<VehicleViewModel>();
            
            // foreach (var vehicle in response)
            // {
            //     vehicleList.Add(
            //         new VehicleViewModel{
            //             VehicleId = vehicle.Id,
            //             RegNo = vehicle.RegNo,
            //             VehicleName = string.Concat(vehicle.Make, " ", vehicle.Model),
            //             ModelYear = vehicle.ModelYear,
            //             Mileage = vehicle.Mileage
            //         }
            //     );
            // }

            // var vehicleList = _mapper.Map<List<VehicleViewModel>>(response);
            var vehicleList = await _vehicleRepo.ListAllVehiclesAsync();
            return Ok(vehicleList);
            // return StatusCode(200, "{'message':'Det funkar'}");
        }

        //Metod för att hämta fordon med Id
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicleById( int id){
            var response = await _vehicleRepo.GetVehicleAsync(id);
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med id: {id}");            
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
        public async Task<ActionResult<VehicleViewModel>> GetVehicleByRegNo( string regNo){
            var response = await _vehicleRepo.GetVehicleAsync(regNo);
            if(response is null) return NotFound($"Vi kunde inte hitta någon bil med regNo: {regNo}");
            return Ok(response);
        }

        //Metod för att hämta fordon med RegNo
        // [HttpGet("bymake/{make}")]
        // public async Task<ActionResult<List<VehicleViewModel>>> GetVehicleByMake( string make){
        //     var response = await _vehicleRepo.GetVehicleByMakeAsync(make);
        //     // var response = await _context.Vehicles.Where(v => v.Make!.ToLower() == make.ToLower()).ToListAsync();
        //     if(response is null) return NotFound($"Vi kunde inte hitta någon bil med märke: {make}");
        //     return Ok(response);
        // }

        // Lägger till ett nytt fordon i systemet...
        [HttpPost()]
        public async Task<ActionResult> AddVehicle(PostVehicleViewModel model){

            // var vehicleToAdd = new Vehicle{
            //     RegNo = vehicle.RegNo,
            //     Make = vehicle.Make,
            //     Mileage = vehicle.Mileage,
            //     Model = vehicle.Model,
            //     ModelYear = vehicle.ModelYear
            // };

            // var vehicleToAdd = _mapper.Map<Vehicle>(vehicle);
            try
            {
                if (await _vehicleRepo.GetVehicleAsync(model.RegNo!.ToLower()) is not null)
                {
                    return BadRequest($"Registreringsnummer {model.RegNo} finns redan i systemet");
                }
                await _vehicleRepo.AddVehicleAsync(model);
                if(await _vehicleRepo.SaveAllAsync()) return StatusCode(201);
                return StatusCode(500, "Det gick inte att spara fordonet");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            
        }

        // Uppdaterar ett befintligt fordon...
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, PostVehicleViewModel model){
            try
            {
                await _vehicleRepo.UpdateVehicleAsync(id, model);
                if (await _vehicleRepo.SaveAllAsync())
                {
                    return NoContent();
                }
                return StatusCode(500, "Ett fel inträffade när fordonet skulle uppdateras");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, PatchVehicleViewModel model){
            try
            {
                await _vehicleRepo.UpdateVehicleAsync(id, model);
                if(await _vehicleRepo.SaveAllAsync()) return NoContent();
                return StatusCode(500,"Ett fel inträffade när fordonet skulle uppdateras");
            }
            catch (Exception e)
            {                
                return StatusCode(500, e.Message);
            }
        }

        // Tar bort ett fordon ifrån systemet
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id){
            await _vehicleRepo.DeleteVehicleAsync(id);

            if (await _vehicleRepo.SaveAllAsync()) return NoContent(); // Status kod 204

            return StatusCode(500, "Hoppsan något gick fel"); 
        }
        
    }
}