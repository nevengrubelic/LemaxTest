namespace api.Models
{
	public class Hotel
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public decimal Price { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}