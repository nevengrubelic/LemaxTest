using api.Models;
using api.Models.DTOs;

public interface IHotelService
{
	List<Hotel> GetAllHotels();
	Hotel GetHotelById(int id);
	void AddHotel(Hotel hotel);
	void UpdateHotel(int id, Hotel updatedHotel);
	void DeleteHotel(int id);
	List<SearchResultDTO> SearchForHotels((double, double) geolocation, int page, int pageSize);
}
