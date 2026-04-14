using APBD6.Models;

namespace APBD6.Properties;

public class DB
{
    public static Dictionary<int,Room> Rooms = new Dictionary<int, Room>
    {
        {1,new Room{Id=1,Name ="Room 1",BuildingCode = 1,Floor =1,Capacity = 20,HasProjector = true,IsActive = false}},
        {2,new Room{Id=2,Name ="Room 2",BuildingCode = 1,Floor =1,Capacity = 300,HasProjector = true,IsActive = true}},
        {3,new Room{Id=3,Name ="Room 3",BuildingCode = 2,Floor =3,Capacity = 30,HasProjector = true,IsActive = false}},
        {4,new Room{Id=4,Name ="Room 4",BuildingCode = 1,Floor =2,Capacity = 20,HasProjector = false,IsActive = true}},
        {5,new Room{Id=5,Name ="Room 5",BuildingCode = 2,Floor =3,Capacity = 30,HasProjector = false,IsActive = false}},
        
    };

    


    public static Dictionary<int, Reservation> Reservations = new Dictionary<int, Reservation>
    {
        {1,new Reservation{Id = 1,RoomId = 4,OrganizerName = "Kassemberg",Topic = "BSI",Date = DateTime.Parse("2026-04-10"),StartTime = DateTime.Parse("15:45:00").ToUniversalTime(),EndTime = DateTime.Parse("17:15:00").ToUniversalTime(),Status = "Active"}},
        {2,new Reservation{Id = 2,RoomId = 2,OrganizerName = "Gago",Topic = "APBD",Date = DateTime.Parse("2026-04-12"),StartTime = DateTime.Parse("14:00:00").ToUniversalTime(),EndTime = DateTime.Parse("15:30:00").ToUniversalTime(),Status = "Active"}},
        {3,new Reservation{Id = 3,RoomId = 1,OrganizerName = "Pękalski",Topic = "PSM",Date = DateTime.Parse("2026-04-11"),StartTime = DateTime.Parse("08:30:00").ToUniversalTime(),EndTime = DateTime.Parse("10:00:00").ToUniversalTime(),Status = "Active"}},
        {4,new Reservation{Id = 4,RoomId = 5,OrganizerName = "Werner",Topic = "PPY",Date = DateTime.Parse("2026-04-09"),StartTime = DateTime.Parse("10:15:00").ToUniversalTime(),EndTime = DateTime.Parse("11:45:00").ToUniversalTime(),Status = "Active"}},
        {5,new Reservation{Id = 5,RoomId = 4,OrganizerName = "Lenkewicz",Topic = "ABD",Date = DateTime.Parse("2026-04-16"),StartTime = DateTime.Parse("17:30:00").ToUniversalTime(),EndTime = DateTime.Parse("19:00:00").ToUniversalTime(),Status = "Active"}},
        {6,new Reservation{Id = 6,RoomId = 3,OrganizerName = "Pierzchała",Topic = "TPO",Date = DateTime.Parse("2026-04-11"),StartTime = DateTime.Parse("15:45:00").ToUniversalTime(),EndTime = DateTime.Parse("17:15:00").ToUniversalTime(),Status = "Active"}},

    };


}