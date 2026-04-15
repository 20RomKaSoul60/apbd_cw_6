using APBD6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateTime? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = DB.Reservations;

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date.Date == date.Value.Date);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = DB.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound($"Reservation with id {id} was not found.");

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = DB.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room == null)
            return BadRequest("Cannot create reservation for a room that does not exist.");

        if (!room.IsActive)
            return BadRequest("Cannot create reservation for an inactive room.");

        bool hasConflict = DB.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date.Date == reservation.Date.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("This reservation conflicts with an existing reservation for the same room.");

        reservation.Id = DB.Reservations.Any() ? DB.Reservations.Max(r => r.Id) + 1 : 1;
        DB.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation updatedReservation)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingReservation = DB.Reservations.FirstOrDefault(r => r.Id == id);

        if (existingReservation == null)
            return NotFound($"Reservation with id {id} was not found.");

        var room = DB.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

        if (room == null)
            return BadRequest("Cannot assign reservation to a room that does not exist.");

        if (!room.IsActive)
            return BadRequest("Cannot assign reservation to an inactive room.");

        bool hasConflict = DB.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date.Date == updatedReservation.Date.Date &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("This reservation conflicts with an existing reservation for the same room.");

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = DB.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
            return NotFound($"Reservation with id {id} was not found.");

        DB.Reservations.Remove(reservation);
        return NoContent();
    }
}