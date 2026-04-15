using System.ComponentModel.DataAnnotations;
using tutorial5.Enums;

namespace tutorial5.Dtos;

public class CreateReservationDto : IValidatableObject
{
    [Range(1, long.MaxValue)]
    public long RoomId { get; set; }

    [Required]
    public string OrganizerName { get; set; } = string.Empty;

    [Required]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public Status Status { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                [nameof(StartTime), nameof(EndTime)]);
        }
    }
}
