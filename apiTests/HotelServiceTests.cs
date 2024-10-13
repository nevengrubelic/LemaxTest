using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using api.Models;

public class HotelServiceTests
{
	private readonly HotelService _hotelService;

	public HotelServiceTests()
	{
		_hotelService = new HotelService();
	}

	[Fact]
	public void AddHotel_ShouldAddHotelToList()
	{
		// Arrange
		_hotelService.CleanHotelList();
		var hotel = new Hotel
		{
			Name = "Test Hotel",
			Price = 100,
			Longitude = 40.7128,
			Latitude = -74.0060
		};

		// Act
		var result = _hotelService.AddHotel(hotel);
		var allHotels = _hotelService.GetAllHotels();

		// Assert
		Assert.True(result);
		Assert.Single(allHotels);
	}

	[Fact]
	public void UpdateHotel_ShouldUpdateHotelDetails()
	{
		// Arrange
		_hotelService.CleanHotelList();
		var hotel = new Hotel
		{
			Id = 1,
			Name = "Old Hotel",
			Price = 150,
			Latitude = 40.7128,
			Longitude = -74.0060
		};
		_hotelService.AddHotel(hotel);

		Hotel updatedHotel = new Hotel
		{
			Name = "Updated Hotel",
			Price = 120,
			Latitude = 40.7128,
			Longitude = -74.0060
		};

		// Act
		_hotelService.UpdateHotel(1, updatedHotel);
		var allHotels = _hotelService.GetAllHotels();

		// Assert
		Assert.Single(allHotels);
		Assert.Equal("Updated Hotel", allHotels[0].Name);
		Assert.Equal(120, allHotels[0].Price);
	}

	[Fact]
	public void DeleteHotel_ShouldRemoveHotelFromList()
	{
		// Arrange
		_hotelService.CleanHotelList();
		var hotel = new Hotel
		{
			Id = 1,
			Name = "Test Hotel",
			Price = 100,
			Latitude = 40.7128,
			Longitude = -74.0060
		};
		_hotelService.AddHotel(hotel);

		// Act
		var result = _hotelService.DeleteHotel(1);
		var allHotels = _hotelService.GetAllHotels();

		// Assert
		Assert.True(result);
		Assert.Empty(allHotels);
	}

	[Fact]
	public void SearchHotels_ShouldReturnSortedHotelsByPriceAndDistance()
	{
		// Arrange
		_hotelService.CleanHotelList();
		_hotelService.AddHotel(new Hotel { Id = 0, Name = "Hotel A", Price = 200, Latitude = 40.7128, Longitude = -74.0060 });
		_hotelService.AddHotel(new Hotel { Id = 1, Name = "Hotel B", Price = 100, Latitude = 34.0522, Longitude = -118.2437 });
		_hotelService.AddHotel(new Hotel { Id = 2, Name = "Hotel C", Price = 150, Latitude = 34.0522, Longitude = -118.2437 });

		var myLocation = (40.7128, -74.0060); // NYC

		// Act
		var result = _hotelService.SearchForHotels(myLocation, 1, 10);

		// Assert
		Assert.Equal(3, result.Count);
		Assert.Equal("Hotel A", result[0].Name); // Closer hotel
		Assert.Equal("Hotel B", result[1].Name); // Cheaper hotel
		Assert.Equal("Hotel C", result[2].Name); // Further and more expensive hotel
	}

	[Fact]
	public void UpdateHotel_NonExistentHotel_ShouldReturnFalse()
	{
		// Arrange
		_hotelService.CleanHotelList();
		var updatedHotel = new Hotel
		{
			Name = "Non-Existent Hotel",
			Price = 200,
			Latitude = 40.7128,
			Longitude = -74.0060
		};

		// Act
		var result = _hotelService.UpdateHotel(999, updatedHotel); // Non-existent ID

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void DeleteHotel_NonExistentHotel_ShouldReturnFalse()
	{
		_hotelService.CleanHotelList();

		// Act
		var result = _hotelService.DeleteHotel(999); // Non-existent ID

		// Assert
		Assert.False(result);
	}
}
