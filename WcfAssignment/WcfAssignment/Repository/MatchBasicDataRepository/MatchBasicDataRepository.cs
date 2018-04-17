using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WcfAssignment.MatchBasicDataService;

namespace WcfAssignment.Repository.MatchBasicDataRepository
{
	public class MatchBasicDataRepository
	{
		private readonly IMatchBasicDataService _matchBasicDataService;

		public MatchBasicDataRepository(IMatchBasicDataService matchBasicDataService)
		{
			_matchBasicDataService = matchBasicDataService;
		}

		public async Task<List<MatchBasicDataObject>> GetAll()
		{
			var result = await _matchBasicDataService.GetMatchListAsync(new MatchBasic() { SessionId = "xxx" });
			
			if (result != null && result.ResultData != null)
				return result.ResultData.Select(item => new MatchBasicDataObject
				{
					Id = item.Id,
					Name = item.Name,
					MatchDate = item.MatchDate?.ToString("dd.MM.yyyy"),
					Description = item.Description
				}).ToList();

			return new List<MatchBasicDataObject>();
		}

		public async Task<OperationResult> Update(MatchBasicDataObject data)
		{
			try
			{
				var result = await _matchBasicDataService.SaveMatchDateAsync(new MatchBasicData()
				{
					SessionId = "xxx",
					Id = data.Id,
					MatchDate = data.MatchDate == null ? (DateTime?)null : DateTime.ParseExact(data.MatchDate, "dd.MM.yyyy", CultureInfo.InvariantCulture)
				});

				return new OperationResult { IsSuccess = string.IsNullOrWhiteSpace(result.ErrorMessage) };
			}
			catch(Exception)
			{
				return new OperationResult { IsSuccess = false };
			}
		}
	}
}