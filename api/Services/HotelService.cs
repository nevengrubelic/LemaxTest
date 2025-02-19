using api.Helpers;
using api.Models;
using api.Models.DTOs;

public class HotelService : IHotelService
{
	private List<Hotel> Hotels = new List<Hotel>();

	public List<Hotel> GetAllHotels()
	{
		return Hotels;
	}

	public Hotel GetHotelById(int id)
	{
		var hotel = Hotels.FirstOrDefault(h => h.Id == id);
		return hotel;
	}

	public bool AddHotel(Hotel hotel)
	{
		Hotels.Add(hotel);
		return true;
	}

	public bool UpdateHotel(int id, Hotel updatedHotel)
	{
		var hotel = GetHotelById(id);
		if (hotel == null)
			return false;

		hotel.Name = updatedHotel.Name;
		hotel.Price = updatedHotel.Price;
		hotel.Latitude = updatedHotel.Latitude;
		hotel.Longitude = updatedHotel.Longitude;
		return true;
	}

	public bool DeleteHotel(int id)
	{
		var hotel = GetHotelById(id);
		if (hotel == null)
			return false;

		Hotels.Remove(hotel);
		return true;
	}

	public List<SearchResultDTO> SearchForHotels((double, double) geolocation, int page, int pageSize)
	{
		var userLocation = new { Latitude = geolocation.Item1, Longitude = geolocation.Item2 };
		// Calculate distance for each hotel and sort by price and distance
		var hotels = GetAllHotels()
			.Select(h => new SearchResultDTO
			{
				Name = h.Name,
				Price = h.Price,
				Distance = UtilityFunctions.GetDistance(userLocation.Latitude, userLocation.Longitude, h.Latitude, h.Longitude),
			})
			.OrderBy(h => h.Distance + h.Price);

		// Pagination logic: Skip the records for previous pages and take the records for the current page
		var paginatedHotels = hotels
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToList();

		return paginatedHotels;
	}

	public void CleanHotelList()
	{
		Hotels.Clear();
	}
}