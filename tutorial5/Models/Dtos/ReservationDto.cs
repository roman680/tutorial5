using tutorial5.Enums;

namespace tutorial5.Dtos;

public class ReservationDto
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public string OrganizerName { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Status Status { get; set; }
}
