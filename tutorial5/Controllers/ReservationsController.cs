using Microsoft.AspNetCore.Mvc;
using tutorial5.Dtos;
using tutorial5.Enums;
using tutorial5.Mappings;
using tutorial5.Models;
using tutorial5.Utils;

namespace tutorial5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] Status? status,
        [FromQuery] long? roomId)
    {
        var reservations = Data.Reservations.AsEnumerable();

        if (date.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.Date == date.Value);
        }

        if (status.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.Status == status.Value);
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.RoomId == roomId.Value);
        }

        return Ok(reservations.Select(reservation => reservation.ToDto()));
    }

    [HttpGet("{id:long}")]
    public ActionResult<ReservationDto> GetById([FromRoute] long id)
    {
        var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation.ToDto());
    }

    [HttpPost]
    public ActionResult<ReservationDto> Create([FromBody] CreateReservationDto createReservationDto)
    {
        var reservation = createReservationDto.ToModel();
        var roomValidationResult = ValidateRoomForReservation(reservation.RoomId);
        if (roomValidationResult is not null)
        {
            return roomValidationResult;
        }

        if (HasOverlap(reservation))
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Reservation conflict",
                detail: "Reservation overlaps with an existing reservation for this room.");
        }

        reservation.Id = Data.Reservations.Count == 0 ? 1 : Data.Reservations.Max(r => r.Id) + 1;
        Data.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation.ToDto());
    }

    [HttpPut("{id:long}")]
    public ActionResult<ReservationDto> Update([FromRoute] long id, [FromBody] UpdateReservationDto updateReservationDto)
    {
        var existingReservation = Data.Reservations.FirstOrDefault(reservation => reservation.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        var roomValidationResult = ValidateRoomForReservation(updateReservationDto.RoomId);
        if (roomValidationResult is not null)
        {
            return roomValidationResult;
        }

        var updatedReservation = updateReservationDto.ToModel(id);

        if (HasOverlap(updatedReservation, id))
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Reservation conflict",
                detail: "Reservation overlaps with an existing reservation for this room.");
        }

        updateReservationDto.MapTo(existingReservation);

        return Ok(existingReservation.ToDto());
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete([FromRoute] long id)
    {
        var reservation = Data.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        Data.Reservations.Remove(reservation);
        return NoContent();
    }

    private ActionResult? ValidateRoomForReservation(long roomId)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == roomId);

        if (room is null)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Room not found",
                detail: "Cannot create or update a reservation for a room that does not exist.");
        }

        if (!room.IsActive)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Inactive room",
                detail: "Cannot create or update a reservation for an inactive room.");
        }

        return null;
    }

    private static bool HasOverlap(Reservation candidateReservation, long? reservationIdToIgnore = null)
    {
        if (candidateReservation.Status == Status.Cancelled)
        {
            return false;
        }

        return Data.Reservations.Any(existingReservation =>
            existingReservation.Id != reservationIdToIgnore &&
            existingReservation.RoomId == candidateReservation.RoomId &&
            existingReservation.Date == candidateReservation.Date &&
            existingReservation.Status != Status.Cancelled &&
            existingReservation.StartTime < candidateReservation.EndTime &&
            candidateReservation.StartTime < existingReservation.EndTime);
    }
}
