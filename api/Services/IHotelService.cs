using api.Models;
using api.Models.DTOs;

public interface IHotelService
{
	List<Hotel> GetAllHotels();
	Hotel GetHotelById(int id);
	bool AddHotel(Hotel hotel);
	bool UpdateHotel(int id, Hotel updatedHotel);
	bool DeleteHotel(int id);
	List<SearchResultDTO> SearchForHotels((double, double) geolocation, int page, int pageSize);
}
