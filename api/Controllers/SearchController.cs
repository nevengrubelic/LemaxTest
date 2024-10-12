using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
		var userLocation = new { Latitude = lat, Longitude = lng };

		// Calculate distance for each hotel and sort by price and distance
		var hotels = _hotelService.GetAllHotels()
			.Select(h => new
			{
				h.Name,
				h.Price,
				Distance = GetDistance(userLocation.Latitude, userLocation.Longitude, h.Latitude, h.Longitude)
			})
			.OrderBy(h => h.Price)  // First sort by price
			.ThenBy(h => h.Distance);  // Then sort by distance

		// Pagination logic: Skip the records for previous pages and take the records for the current page
		var paginatedHotels = hotels
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToList();

		// Return paginated results
		return Ok(paginatedHotels);
	}

	// Haversine formula to calculate distance between two geo points in kilometers
	private double GetDistance(double lat1, double lon1, double lat2, double lon2)
	{
		const double R = 6371; // Radius of Earth in kilometers
		var lat = ToRadians(lat2 - lat1);
		var lon = ToRadians(lon2 - lon1);
		var a = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
				Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
				Math.Sin(lon / 2) * Math.Sin(lon / 2);
		var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
		return R * c;
	}

	private double ToRadians(double angle)
	{
		return angle * Math.PI / 180;
	}
}
