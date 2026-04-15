using Microsoft.AspNetCore.Mvc;
using tutorial5.Dtos;
using tutorial5.Mappings;
using tutorial5.Models;
using tutorial5.Utils;

namespace tutorial5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<RoomDto>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool activeOnly = false)
    {
        var rooms = Data.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(room => room.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(room => room.HasProjector == hasProjector.Value);
        }

        if (activeOnly)
        {
            rooms = rooms.Where(room => room.IsActive);
        }

        return Ok(rooms.Select(room => room.ToDto()));
    }

    [HttpGet("{id:long}")]
    public ActionResult<RoomDto> GetById([FromRoute] long id)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room.ToDto());
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<RoomDto>> GetByBuilding([FromRoute] string buildingCode)
    {
        var rooms = Data.Rooms
            .Where(room => string.Equals(room.BuildingCode, buildingCode, StringComparison.OrdinalIgnoreCase))
            .Select(room => room.ToDto())
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<RoomDto> Create([FromBody] CreateRoomDto createRoomDto)
    {
        var room = createRoomDto.ToModel(Data.Rooms.Count == 0 ? 1 : Data.Rooms.Max(r => r.Id) + 1);
        Data.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room.ToDto());
    }

    [HttpPut("{id:long}")]
    public ActionResult<RoomDto> Update([FromRoute] long id, [FromBody] UpdateRoomDto updateRoomDto)
    {
        var existingRoom = Data.Rooms.FirstOrDefault(room => room.Id == id);

        if (existingRoom is null)
        {
            return NotFound();
        }

        updateRoomDto.MapTo(existingRoom);

        return Ok(existingRoom.ToDto());
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        var room = Data.Rooms.FirstOrDefault(r => r.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        var hasRelatedReservations = Data.Reservations.Any(reservation => reservation.RoomId == id);

        if (hasRelatedReservations)
        {
            return Problem(
                statusCode: StatusCodes.Status409Conflict,
                title: "Room cannot be deleted",
                detail: "Cannot delete a room with existing reservations.");
        }

        Data.Rooms.Remove(room);
        return NoContent();
    }
}
