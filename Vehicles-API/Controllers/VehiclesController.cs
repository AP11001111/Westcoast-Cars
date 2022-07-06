using Microsoft.AspNetCore.Mvc;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {
        //Metod för att hämta alla fordon
        [HttpGet()]
        public ActionResult ListVehicles(){
            return StatusCode(200, "{'message':'Det funkar'}");
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
    }
}