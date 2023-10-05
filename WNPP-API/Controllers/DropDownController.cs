using Microsoft.AspNetCore.Mvc;
using WNPP_API.Models;
using WNPP_API.Services;

namespace WNPP_API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class DropDownController : ControllerBase
	{
		private IDropDownService _service;
		private readonly ILogger<DropDownController> _logger;
		
		public DropDownController(
			ILogger<DropDownController> logger)
		{
			_logger = logger;
			_service = new DropDownService();
		}

		[HttpGet]
		[ActionName("GetBranchType")]
		public List<MDropDown> GetBranchType()
		{
			return _service.getBranchType();
		}
		[HttpGet]
		[ActionName("GetLanguageType")]
		public List<MDropDown> GetLanguageType()
		{
			return _service.getLanguageType();
		}
		[HttpGet]
		[ActionName("GetMonasteryType")]
		public List<MDropDown> GetMonasteryType()
		{
			return _service.getMonasteryType();
		}
		[HttpGet]
		[ActionName("GetAcademicType")]
		public List<MDropDown> GetAcademicType()
		{
			return _service.getAcademicType();
		}
		[HttpGet]
		[ActionName("GetSageType")]
		public List<MDropDown> GetSageType()
		{
			return _service.getSageType();
		}
		[HttpGet]
		[ActionName("GetTheologianType")]
		public List<MDropDown> GetTheologianType()
		{
			return _service.getTheologianType();
		}
	}
}
