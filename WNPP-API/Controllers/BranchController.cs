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
			return _service.updateBranch(data);
		}
		[HttpPost]
		[ActionName("AddBranch")]
		public TBranchViewModel AddBranch(TBranchViewModel data)
		{
			return _service.addBranch(data);
		}
		[HttpGet]
		[ActionName("ListBranch")]
		public List<TBranchViewModel> ListBranch(int branchType =0)
		{
			return _service.listBranch(branchType);
		}
		[HttpGet]
		[ActionName("GetBranch")]
		public TBranchViewModel GetBranch(int ID)
		{
			return _service.getBranch(ID);
		}
	}
}
