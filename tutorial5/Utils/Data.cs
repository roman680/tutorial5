using tutorial5.Enums;
using tutorial5.Models;

namespace tutorial5.Utils;

public static class Data
{
    public static List<Room> Rooms { get; set; } =
    [
        new Room
        {
            Id = 1,
            Name = "Lab 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 18,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Workshop 204",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Seminar 305",
            BuildingCode = "B",
            Floor = 3,
            Capacity = 32,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Consulting 012",
            BuildingCode = "C",
            Floor = 0,
            Capacity = 8,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 5,
            Name = "Studio 401",
            BuildingCode = "A",
            Floor = 4,
            Capacity = 20,
            HasProjector = true,
            IsActive = false
        }
    ];
    
    public static List<Reservation> Reservations { get; set; } =
    [
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "HTTP and REST Workshop",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeSpan(10, 0, 0),
            EndTime = new TimeSpan(12, 30, 0),
            Status = Status.Confirmed
        },
        new Reservation
        {
            Id = 2,
            RoomId = 1,
            OrganizerName = "Piotr Nowak",
            Topic = "Postman Basics",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeSpan(13, 0, 0),
            EndTime = new TimeSpan(14, 30, 0),
            Status = Status.Planned
        },
        new Reservation
        {
            Id = 3,
            RoomId = 2,
            OrganizerName = "Marta Zielinska",
            Topic = "Controller Routing Lab",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(11, 0, 0),
            Status = Status.Confirmed
        },
        new Reservation
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "John Smith",
            Topic = "API Validation Clinic",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeSpan(12, 0, 0),
            EndTime = new TimeSpan(14, 0, 0),
            Status = Status.Cancelled
        },
        new Reservation
        {
            Id = 5,
            RoomId = 4,
            OrganizerName = "Katarzyna Lewandowska",
            Topic = "One-to-One Consultation",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeSpan(8, 30, 0),
            EndTime = new TimeSpan(9, 30, 0),
            Status = Status.Confirmed
        },
        new Reservation
        {
            Id = 6,
            RoomId = 2,
            OrganizerName = "Michael Brown",
            Topic = "Testing Web APIs",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeSpan(15, 0, 0),
            EndTime = new TimeSpan(17, 0, 0),
            Status = Status.Planned
        }
    ];
}
