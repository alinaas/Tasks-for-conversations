using System.Threading.Tasks;
using System.Web.Mvc;
using WcfAssignment.Models;
using WcfAssignment.Repository;
using WcfAssignment.Repository.MatchBasicDataRepository;

namespace WcfAssignment.Controllers
{
	public class MatchBasicDataController : Controller
	{
		private readonly MatchBasicDataRepository _dataRepository;

		public MatchBasicDataController(MatchBasicDataRepository dataRepository)
		{
			_dataRepository = dataRepository;
		}

		public async Task<ActionResult> Index()
		{
			return View(new MatchBasicDataModel { Items = await _dataRepository.GetAll() });
		}

		[HttpPost]
		public async Task<ActionResult> Update(MatchBasicDataObject data)
		{
			if (ModelState.IsValid)
				return Json(await _dataRepository.Update(data));

			return Json(new OperationResult { IsSuccess = false });
		}
	}
}