using FileAssignment.Validators;
using System.ComponentModel.DataAnnotations;

namespace FileAssignment.Models
{
	public class Data
	{
		[Required]
		[ValidDateString]
		public string Date { get; set; }
	}

	public class DataResponse
	{
		public string Date { get; set; }

		public bool IsSuccess { get; set; }
	}
}