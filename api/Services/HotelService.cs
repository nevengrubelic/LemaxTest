using api.Models;

public class HotelService : IHotelService
{
	private List<Hotel> Hotels = new List<Hotel>();

	public List<Hotel> GetAllHotels() => Hotels;

	public Hotel GetHotelById(int id) => Hotels.FirstOrDefault(h => h.Id == id);

	public void AddHotel(Hotel hotel)
	{
		hotel.Id = Hotels.Max(h => h.Id) + 1;
		Hotels.Add(hotel);
	}

	public void UpdateHotel(int id, Hotel updatedHotel)
	{
		var hotel = GetHotelById(id);
		if (hotel != null)
		{
			hotel.Name = updatedHotel.Name;
			hotel.Price = updatedHotel.Price;
			hotel.Latitude = updatedHotel.Latitude;
			hotel.Longitude = updatedHotel.Longitude;
		}
	}

	public void DeleteHotel(int id)
	{
		var hotel = GetHotelById(id);
		if (hotel != null)
		{
			Hotels.Remove(hotel);
		}
	}
}