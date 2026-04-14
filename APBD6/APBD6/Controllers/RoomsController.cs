using APBD6.Models;
using APBD6.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        
        //GETs
        
        [HttpGet]
        public ActionResult<Dictionary<int,Room>> getAllRooms()
        {
            return Ok(DB.Rooms);

        }

        [HttpGet("{id}")]
        public ActionResult<Room> GetRoom(int id)
        {
            var room = DB.Rooms.FirstOrDefault(r=> r.Value.Id == id);
            if (room.Value == null)
            {
                return NotFound("Room was not found"); 
            }
            return Ok(room);
        }
        
        [HttpGet("buildings/{buildingCode}")]
        public ActionResult<Room> getByBuildingCode(int buildingCode)
        {
            var room = DB.Rooms.Where(r =>
                r.Value.BuildingCode == buildingCode).First();

            if (room.Value == null)
            {
                return NotFound("Room was not found");
            }
            return Ok(room);

        }

        [HttpGet("?minCapacity = 20 & hasProjector=true & activeOnly=true")]
        public ActionResult<Room> getByCapacity()
        {
            var room = DB.Rooms.Values.Where(r => r.Capacity >= 20 && 
                                                                r.IsActive == true && 
                                                                r.HasProjector == true).First();
            if (room == null)
            {
                return NotFound("Room was not found");
            }
            return Ok(room);

        }
        
        


        // POST
        [HttpPost]
        public ActionResult<Room> Create(Room room)
        {
            if (String.IsNullOrEmpty(room.Name) || 
                String.IsNullOrEmpty(room.BuildingCode.ToString()) || 
                String.IsNullOrEmpty(room.Capacity.ToString()) || 
                String.IsNullOrEmpty(room.Floor.ToString()) || 
                String.IsNullOrEmpty(room.Id.ToString()) || 
                String.IsNullOrEmpty(room.HasProjector.ToString()) || 
                String.IsNullOrEmpty(room.IsActive.ToString()))
            {
                return BadRequest($"Attribute is required to be filled");
            }
            if (!DB.Rooms.ContainsValue(room))
            {
                return BadRequest("Such room already exists");
            }
            DB.Rooms.Add(DB.Rooms.Last().Value.Id+1,room);
            
            return Ok("Room was added");
        }
        
        //PUT
        [HttpPut("{id}")]
        public ActionResult<Room> Update(int id, Room room)
        {
            if (!DB.Rooms.Any(r => r.Value.Id == id))
            {
                return NotFound("Room was not found");
            }
            DB.Rooms[id] =  room;
            return Ok("Room was updated");
            
        }
        
        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Room> Delete(int id)
        {
            if (!DB.Rooms.Any(r => r.Value.Id == id))
            {
                return NotFound("Room was not found");
            }

            DB.Rooms.Remove(DB.Rooms.First(r => r.Value.Id == id).Key);
            return Ok("Room was deleted");
        }
    }
}
