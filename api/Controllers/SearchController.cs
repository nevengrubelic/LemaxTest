using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
	private readonly IHotelService _hotelService;

	public SearchController(IHotelService hotelService)
	{
		_hotelService = hotelService;
	}

	// GET: api/search?lat={lat}&lng={lng}&page={page}&pageSize={pageSize}
	[HttpGet]
	public ActionResult<IEnumerable<object>> Search(double lat, double lng, int page = 1, int pageSize = 10)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		if (pageSize <= 0 || page <= 0)
			return BadRequest("Page parameters invalid!");

		if (lat > 360 || lat < -360 || lng > 360 || lng < -360)
			return BadRequest("Geolocation invalid!");

		var paginatedHotels = _hotelService.SearchForHotels((lat, lng), page, pageSize);

		// Return paginated results
		return Ok(paginatedHotels);
	}
}
