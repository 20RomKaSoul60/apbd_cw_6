using APBD6.Models;

namespace APBD6;

public static class DB
{
    public static List<Room> Rooms { get; set; } = new()
    {
        new Room { Id = 1, Name = "Room 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true,  IsActive = true },
        new Room { Id = 2, Name = "Room 102", BuildingCode = "A", Floor = 1, Capacity = 35, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "Lab 204",  BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true,  IsActive = true },
        new Room { Id = 4, Name = "Room 301", BuildingCode = "C", Floor = 3, Capacity = 50, HasProjector = true,  IsActive = false },
        new Room { Id = 5, Name = "Room 205", BuildingCode = "B", Floor = 2, Capacity = 18, HasProjector = false, IsActive = true }
    };

    public static List<Reservation> Reservations { get; set; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "HTTP Basics",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(10, 30, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Nowak",
            Topic = "REST Workshop",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(11, 0, 0),
            EndTime = new TimeSpan(12, 30, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Maria Zielinska",
            Topic = "Consultation",
            Date = new DateTime(2026, 5, 11),
            StartTime = new TimeSpan(8, 30, 0),
            EndTime = new TimeSpan(9, 30, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 1,
            OrganizerName = "Piotr Wójcik",
            Topic = "Architecture Review",
            Date = new DateTime(2026, 5, 12),
            StartTime = new TimeSpan(13, 0, 0),
            EndTime = new TimeSpan(15, 0, 0),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "Ewa Lis",
            Topic = "Team Meeting",
            Date = new DateTime(2026, 5, 10),
            StartTime = new TimeSpan(15, 0, 0),
            EndTime = new TimeSpan(16, 0, 0),
            Status = "confirmed"
        }
    };
}