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
			return _service.updateBranch(data);
		}
		[HttpPost]
		[ActionName("AddBranch")]
		public TBranch AddBranch(TBranch data)
		{
			return _service.addBranch(data);
		}
		[HttpGet]
		[ActionName("ListBranch")]
		public List<TBranch> ListBranch(int branchType =0)
		{
			return _service.listBranch(branchType);
		}
	}
}
