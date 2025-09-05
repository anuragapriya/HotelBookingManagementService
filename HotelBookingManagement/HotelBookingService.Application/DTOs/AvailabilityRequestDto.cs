using System.ComponentModel.DataAnnotations;

namespace HotelBookingService.HotelBookingService.Application.DTOs
{
    public class AvailabilityRequestDto
    {
        [Required]
        public DateOnly From { get; set; }

        [Required]
        public DateOnly To { get; set; }

        [Required]
        [System.ComponentModel.DataAnnotations.Range(1, int.MaxValue, ErrorMessage = "People must be at least 1.")]
        public int People { get; set; }
    }
}
