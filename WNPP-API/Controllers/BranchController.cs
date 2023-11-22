using Microsoft.AspNetCore.Mvc;
using WNPP_API.Models;
using WNPP_API.Services;

namespace WNPP_API.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class BranchController : ControllerBase
	{
		private IBranchService _service;
		private readonly ILogger<BranchController> _logger;

		public BranchController(ILogger<BranchController> logger)
		{
			_logger = logger;
			_service = new BranchService();
		}
		[HttpPost]
		[ActionName("UpdateBranch")]
		public TBranch UpdateBranch(TBranch data)
		{
			_logger.LogTrace("UpdateBranch Data : " + data);
			return _service.updateBranch(data);
		}
		[HttpPost]
		[ActionName("AddBranch")]
		public TBranch AddBranch(TBranch data)
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
		public List<TBranch> ListBranch(int branchType =0)
		{
			_logger.LogTrace("ListBranch Type : " + branchType);
			return _service.listBranch(branchType);
		}
		[HttpGet]
		[ActionName("GetBranchType")]
		public List<TBranch> GetBranchType(int branchType = 0)
		{
			_logger.LogTrace("GetBranchType ID : " + branchType);
			return _service.getBranchsByBranchType(branchType);
		}
		[HttpGet]
		[ActionName("GetBranch")]
		public TBranch GetBranch(int id)
		{
			_logger.LogTrace("GetBranch ID : " + id);
			return _service.getBranch(id);
		}
	}
}
