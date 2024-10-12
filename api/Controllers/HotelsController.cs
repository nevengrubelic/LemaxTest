using api.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
	private readonly IHotelService _hotelService;

	public HotelsController(IHotelService hotelService)
	{
		_hotelService = hotelService;
	}

	// GET: api/hotels
	[HttpGet]
	public IActionResult GetAll()
	{
		return Ok(_hotelService.GetAllHotels());
	}

	// GET: api/hotels/{id}
	[HttpGet("{id:int}")]
	public IActionResult Get(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var hotel = _hotelService.GetHotelById(id);
		if (hotel == null)
		{
			return NotFound();
		}
		return Ok(hotel);
	}

	// POST: api/hotels
	[HttpPost]
	public IActionResult Post([FromBody] Hotel hotel)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		_hotelService.AddHotel(hotel);
		return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
	}

	// PUT: api/hotels/{id}
	[HttpPut("{id:int}")]
	public IActionResult Create(int id, [FromBody] Hotel updatedHotel)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var hotel = _hotelService.GetHotelById(id);
		if (hotel == null)
		{
			return NotFound();
		}
		_hotelService.UpdateHotel(id, updatedHotel);
		return Ok(_hotelService.GetHotelById(id));
	}

	// DELETE: api/hotels/{id}
	[HttpDelete("{id:int}")]
	public IActionResult Delete(int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var hotel = _hotelService.GetHotelById(id);
		if (hotel == null)
		{
			return NotFound();
		}
		_hotelService.DeleteHotel(id);
		return NoContent();
	}
}
