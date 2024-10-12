using api.Models;

public interface IHotelService
{
	List<Hotel> GetAllHotels();
	Hotel GetHotelById(int id);
	void AddHotel(Hotel hotel);
	void UpdateHotel(int id, Hotel updatedHotel);
	void DeleteHotel(int id);
}
