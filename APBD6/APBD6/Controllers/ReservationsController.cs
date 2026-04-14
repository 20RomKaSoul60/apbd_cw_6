using APBD6.Models;
using APBD6.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        
        //GET 

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()
        {
            return Ok(DB.Reservations.Values);
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (!DB.Reservations.Values.Any(r => r.Id == id))
            {
                return NotFound("Reservation not found");
            }
            return Ok(DB.Reservations[id]);
        }

        [HttpGet("?date=2026-05-10&status=confirmed&roomId=2")]
        public ActionResult<Reservation> getRoomStatus()
        {
            var reservation = DB.Reservations.Values.Where(r => r.Date == DateTime.Parse("2026-05-10") && 
                                                                             r.Status == "confirmed" && 
                                                                             r.RoomId == 2).First();
            if (reservation == null)
            {
                return NotFound("Room not found");
            }
            return Ok(reservation);
        }
        
        
        //POST 

        [HttpPost]
        public ActionResult<Reservation> Create(Reservation reservation)
        {
            if (String.IsNullOrEmpty(reservation.Id.ToString()) || 
                String.IsNullOrEmpty(reservation.RoomId.ToString()) || 
                String.IsNullOrEmpty(reservation.OrganizerName) || 
                String.IsNullOrEmpty(reservation.Topic) || 
                String.IsNullOrEmpty(reservation.Date.ToString()) || 
                String.IsNullOrEmpty(reservation.StartTime.ToString()) || 
                String.IsNullOrEmpty(reservation.EndTime.ToString()) || 
                String.IsNullOrEmpty(reservation.Status))
            {
                return BadRequest("Invalid reservation");
            }
            DB.Reservations.Add(DB.Reservations.Last().Key+1,reservation);
            return Ok(reservation);
            
        }

        //PUT
        [HttpPut("{id}")]
        public ActionResult<Reservation> Update(int id, Reservation reservation)
        {
            if (!DB.Reservations.Values.Any(r => r.Id == id) || reservation == null)
            {
                return BadRequest("Invalid reservation");
            }
            DB.Reservations[id] = reservation;
            return Ok(reservation);
        }
        
        //DELETE
        [HttpDelete("{id}")]
        public ActionResult<Reservation> Delete(int id)
        {
            if (!DB.Reservations.Values.Any(r => r.Id == id))
            {
                return NotFound("Reservation not found");
            }
            DB.Reservations.Remove(DB.Reservations.First(r => r.Value.Id == id).Key);
            return Ok();
        }
        
    }
}
