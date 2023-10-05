using WNPP_API.Models;

namespace WNPP_API.Services
{
	public interface IBranchService
	{
		public List<TBranch> listBranch(int branchType = 0);
		public TBranch addBranch(TBranch data);
		public TBranch updateBranch(TBranch data);

	}
	public class BranchService : CommonServices, IBranchService
	{
		private readonly ILogger<BranchService> _logger;
		private readonly Wnpp66Context ctx = new Wnpp66Context();
		public BranchService() { }
		public BranchService(ILogger<BranchService> logger)
		{
		}
		public TBranch updateBranch(TBranch data)
		{
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				var row = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.Id == data.Id)
						.ToList();
				if (row.Any())
					throw new Exception("The Branch ID is not found. " + data.Id);

				data.ModifiedDate = DateTime.Now;
				ctx.TBranches.Update(data);

				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw;
			}
			return data;
		}
		public TBranch addBranch(TBranch data) 
		{
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				var row = ctx.TBranches.Where(x => 
						x.ActiveStatus == true && 
						x.MonasteryName.Contains(data.MonasteryName))
					.ToList();
				if (row.Any() ) 
					throw new Exception("The MonasteryName is already. " + data.MonasteryName);
				
				data.CreatedDate = DateTime.Now;
				ctx.TBranches.Add(data);

				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw;
			}
			return data;
		}
		public List<TBranch> listBranch(int branchType = 0)
		{
			List<TBranch> result = new List<TBranch>();
			var row = ctx.TBranches.Where(o =>
					o.ActiveStatus == _Record_Active )
					.ToList();
			if (branchType != 0)
			{
				row = row.Where(o => o.BranchType == branchType )
					.ToList();
			}
			result = row;
			return result;
		}
	}
}
