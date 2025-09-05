using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingService.HotelBookingService.Api.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        public HotelController(IHotelService hotelService, IRoomService roomService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.Get();
            if (hotels == null) return NotFound();
            return Ok(hotels);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery,Required] string name)
        {
            var hotel = await _hotelService.Get(name);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpGet("{hotelId}/availability")]
        public async Task<IActionResult> Availability([FromRoute] int hotelId, [FromQuery] AvailabilityRequestDto requestDto)
        {
            try
            {
                var result = await _roomService.Get(hotelId, requestDto.From, requestDto.To, requestDto.People);
                if (result is null) return NotFound("Rooms not Available.");
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
