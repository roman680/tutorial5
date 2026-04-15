using tutorial5.Dtos;
using tutorial5.Models;

namespace tutorial5.Mappings;

public static class Mapper
{
    public static RoomDto ToDto(this Room room) =>
        new()
        {
            Id = room.Id,
            Name = room.Name,
            BuildingCode = room.BuildingCode,
            Floor = room.Floor,
            Capacity = room.Capacity,
            HasProjector = room.HasProjector,
            IsActive = room.IsActive
        };

    public static Room ToModel(this CreateRoomDto dto, long id = 0) =>
        new()
        {
            Id = id,
            Name = dto.Name,
            BuildingCode = dto.BuildingCode,
            Floor = dto.Floor,
            Capacity = dto.Capacity,
            HasProjector = dto.HasProjector,
            IsActive = dto.IsActive
        };

    public static void MapTo(this UpdateRoomDto dto, Room room)
    {
        room.Name = dto.Name;
        room.BuildingCode = dto.BuildingCode;
        room.Floor = dto.Floor;
        room.Capacity = dto.Capacity;
        room.HasProjector = dto.HasProjector;
        room.IsActive = dto.IsActive;
    }

    public static ReservationDto ToDto(this Reservation reservation) =>
        new()
        {
            Id = reservation.Id,
            RoomId = reservation.RoomId,
            OrganizerName = reservation.OrganizerName,
            Topic = reservation.Topic,
            Date = reservation.Date,
            StartTime = reservation.StartTime,
            EndTime = reservation.EndTime,
            Status = reservation.Status
        };

    public static Reservation ToModel(this CreateReservationDto dto, long id = 0) =>
        new()
        {
            Id = id,
            RoomId = dto.RoomId,
            OrganizerName = dto.OrganizerName,
            Topic = dto.Topic,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status
        };

    public static void MapTo(this UpdateReservationDto dto, Reservation reservation)
    {
        reservation.RoomId = dto.RoomId;
        reservation.OrganizerName = dto.OrganizerName;
        reservation.Topic = dto.Topic;
        reservation.Date = dto.Date;
        reservation.StartTime = dto.StartTime;
        reservation.EndTime = dto.EndTime;
        reservation.Status = dto.Status;
    }

    public static Reservation ToModel(this UpdateReservationDto dto, long id = 0) =>
        new()
        {
            Id = id,
            RoomId = dto.RoomId,
            OrganizerName = dto.OrganizerName,
            Topic = dto.Topic,
            Date = dto.Date,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status
        };
}
