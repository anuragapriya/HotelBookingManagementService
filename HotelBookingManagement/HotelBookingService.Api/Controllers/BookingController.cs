using HotelBookingService.HotelBookingService.Application.DTOs;
using HotelBookingService.HotelBookingService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingService.HotelBookingService.Api.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;
        public BookingController(IBookingService service) 
        { 
            _service = service; 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingRequestDto request)
        {
            var booking = await _service.BookRoom(request);
            return Ok(booking);
        }

        [HttpGet("{reference}")]
        public async Task<IActionResult> Get(string reference)
        {
            var booking = await _service.Get(reference);
            if (booking == null) return NotFound();
            return Ok(booking);
        }
    }

}

