using WNPP_API.Models;

namespace WNPP_API.Services
{
	public interface IBranchService
	{
		public TBranch getBranch(int id);
		public List<TBranch> getBranchsByBranchType(int branchType);

		public List<TBranch> findByMonasteryName(String monasteryName);
		public List<TBranch> findByAbbotName(String abbotName);


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
		public List<TBranch> findByAbbotName(String abbotName)
		{
			List<TBranch> result = new List<TBranch>();
			try
			{
				var rows = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.AbbotName.Contains(abbotName))
						.ToList();

				if (!rows.Any())
					throw new Exception($"The Abbot Name [{abbotName}] is not found. ");

				result = rows;
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		public List<TBranch> findByMonasteryName(String monasteryName)
		{
			List<TBranch> result = new List<TBranch>();
			try
			{
				var rows = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.MonasteryName.Contains(monasteryName))
						.ToList();

				if (!rows.Any())
					throw new Exception($"The Monastery Name [{monasteryName}] is not found. ");

				result = rows;
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		public List<TBranch> getBranchsByBranchType(int branchType)
		{
			List<TBranch> result = new List<TBranch>();
			try
			{
				var rows = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.BranchType == branchType)
						.ToList();

				if (!rows.Any())
					throw new Exception($"The Branch Type ID [{branchType}] is not found. " );

				result = rows;
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		public TBranch getBranch(int id)
		{
			TBranch result;
			try
			{
				var rows = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.Id == id)
						.ToList();
				if (!rows.Any())
					throw new Exception($"The Branch ID [{id}] is not found. ");

				result = rows.First();
			}
			catch (Exception)
			{
				throw;
			}
			return result;
		}
		/// <summary>
		/// ต้องทำการปรับปรุง บาง Field ไม่ใช่ทั้ง Entity
		/// ต้องหาวิธีหใหม่
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public TBranch updateBranch(TBranch data)
		{
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				var row = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.Id == data.Id)
						.ToList();

				if (!row.Any())
					throw new Exception($"The Branch ID [{data.Id}] is not found. ");

				data.ModifiedDate = DateTime.Now;
				ctx.TBranches.Update(data);
				ctx.SaveChanges();

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

				if (row.Any()) 
					throw new Exception("The Monastery Name is already. " + data.MonasteryName);
				
				data.CreatedDate = DateTime.Now;
				ctx.TBranches.Add(data);
				ctx.SaveChanges();

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
