using System.ComponentModel.DataAnnotations;

namespace api.Models.DTOs
{
	public class SearchResultDTO
	{
		[Required]
		[MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
		[MaxLength(35, ErrorMessage = "Name can't be longer than 2 characters")]
		public string Name { get; set; } = string.Empty;
		[Required]
		[Range(1, 100000)]
		public double Price { get; set; }
		[Required]
		[Range(0, 1000000)]
		public double Distance { get; set; }
	}
}