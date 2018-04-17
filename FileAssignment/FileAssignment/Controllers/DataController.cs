using FileAssignment.Models;
using FileAssignment.Repository;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace FileAssignment.Controllers
{
	public class DataController : Controller
	{
		private readonly DataRepository _dataRepository;

		public DataController(DataRepository dataRepository)
		{
			_dataRepository = dataRepository;
		}

		public ActionResult Index()
		{
			return View(new Data { Date = _dataRepository.Read() });
		}

		[HttpPost]
		public ActionResult SaveData(Data data)
		{
			if (ModelState.IsValid)
			{
				bool saveResult = _dataRepository.Save(data.Date);
				if (!saveResult)
					return Json(new DataResponse { IsSuccess = false });

				return Json(new DataResponse { IsSuccess = true, Date = _dataRepository.Read() });
			}

			return Json(new DataResponse { IsSuccess = false });
		}
	}
}