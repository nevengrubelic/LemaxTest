using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
	public class Hotel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		[MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
		[MaxLength(35, ErrorMessage = "Name can't be longer than 2 characters")]
		public string Name { get; set; } = string.Empty;
		[Required]
		[Range(1, 100000)]
		public double Price { get; set; }
		[Column("double(3,4")]
		[Required]
		[Range(-360, 360)]
		public double Latitude { get; set; }
		[Column("double(3,4")]
		[Required]
		[Range(-360, 360)]
		public double Longitude { get; set; }
	}
}