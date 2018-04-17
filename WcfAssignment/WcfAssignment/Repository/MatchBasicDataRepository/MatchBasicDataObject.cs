using System.ComponentModel.DataAnnotations;
using WcfAssignment.Validators;

namespace WcfAssignment.Repository.MatchBasicDataRepository
{
	public class MatchBasicDataObject
	{
		[Required]
		public int Id { get; set; }

		public string Name { get; set; }

		[Required]
		[ValidDateString]
		public string MatchDate { get; set; }

		public string Description { get; set; }
	}
}