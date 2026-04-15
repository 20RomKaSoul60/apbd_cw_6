using APBD6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> rooms = DB.Rooms;

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly.HasValue && activeOnly.Value)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms);
    }

    [HttpGet("{id}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound($"Room with id {id} was not found.");

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
    {
        var rooms = DB.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!rooms.Any())
            return NotFound($"No rooms found for building code '{buildingCode}'.");

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> Create([FromBody] Room room)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        room.Id = DB.Rooms.Any() ? DB.Rooms.Max(r => r.Id) + 1 : 1;
        DB.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public ActionResult<Room> Update(int id, [FromBody] Room updatedRoom)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingRoom = DB.Rooms.FirstOrDefault(r => r.Id == id);

        if (existingRoom == null)
            return NotFound($"Room with id {id} was not found.");

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.Capacity = updatedRoom.Capacity;
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;

        return Ok(existingRoom);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = DB.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            return NotFound($"Room with id {id} was not found.");

        var hasRelatedReservations = DB.Reservations.Any(r => r.RoomId == id);

        if (hasRelatedReservations)
            return Conflict("Cannot delete room with related reservations.");

        DB.Rooms.Remove(room);
        return NoContent();
    }
}