using Microsoft.AspNetCore.Mvc;
using WNPP_API.Models;
using WNPP_API.Services;
using WNPP_API.ViewModel;

namespace WNPP_API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class BranchController : ControllerBase
	{
		private readonly IBranchService _service;
		private readonly ILogger<BranchController> _logger;

		public BranchController(
			ILogger<BranchController> logger)
		{
			_logger = logger;
			_service = new BranchService();
		}
		[HttpPost]
		[ActionName("UpdateBranch")]
		public TBranchViewModel UpdateBranch(TBranchViewModel data)
		{
			_logger.LogTrace("UpdateBranch Data : " + data);
			return _service.updateBranch(data);
		}
		[HttpPost]
		[ActionName("AddBranch")]
		public TBranchViewModel AddBranch(TBranchViewModel data)
		{
			_logger.LogTrace("AddBranch Data : " + data);
			return _service.addBranch(data);
		}
		[HttpPost]
		[ActionName("findByMonasteryName")]
		public List<TBranch> FindByMonasteryName(String monasteryName)
		{
			_logger.LogTrace("findByMonasteryName : " + monasteryName);
			return _service.findByMonasteryName(monasteryName);
		}
		[HttpPost]
		[ActionName("findByAbbotName")]
		public List<TBranch> FindByAbbotName(String abbotName)
		{
			_logger.LogTrace("findByAbbotName : " + abbotName);
			return _service.findByAbbotName(abbotName);
		}

		[HttpGet]
		[ActionName("ListBranch")]
		public List<TBranchViewModel> ListBranch(int branchType =0)
		{
			_logger.LogTrace("ListBranch Type : " + branchType);
			return _service.listBranch(branchType);
		}
	}
}
