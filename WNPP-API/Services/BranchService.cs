using Microsoft.EntityFrameworkCore;
using WNPP_API.Mapper;
using WNPP_API.Models;
using WNPP_API.ViewModel;

namespace WNPP_API.Services
{
	public interface IBranchService
	{
		public TBranchViewModel getBranch(int ID);
		public List<TBranchViewModel> listBranch(int branchType = 0);
		public TBranchViewModel addBranch(TBranchViewModel view);
		public TBranchViewModel updateBranch(TBranchViewModel view);

	}
	public class BranchService : CommonServices, IBranchService
	{
		private readonly ILogger<BranchService> _logger;
		private readonly IBranchMapper _mapper;

		private readonly Wnpp66Context ctx = new Wnpp66Context();

		public BranchService() {
			_mapper = new BranchMapper();
		}
		public BranchService(
			IBranchMapper mapper,
			ILogger<BranchService> logger)
		{
			_mapper = mapper;
			_logger = logger;
		}
		public TBranchViewModel updateBranch(TBranchViewModel view)
		{
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				var rows = ctx.TBranches.Where(x =>
						x.ActiveStatus == true &&
						x.Id == view.Id)
						.ToList();

				if (!rows.Any())
					throw new Exception($"The Branch ID [{view.Id}] is not found. ");
				var row = rows.First();
				//row.Id = view.Id;
				row.ActiveStatus = view.ActiveStatus;
				row.LanguageId = view.LanguageId;
				row.RecordStatus = view.RecordStatus;
				row.CreatedBy = view.CreatedBy;
				row.ModifiedBy = view.ModifiedBy;
				row.CreatedByName = view.CreatedByName;
				row.ModifiedByName = view.ModifiedByName;

				row.Notation = view.Notation;

				row.CreatedDate = view.CreatedDate;
				row.ModifiedDate = DateTime.Now;

				row.BranchName = view.BranchName;
				row.BranchType = view.BranchType;
				row.BranchTypeName = view.BranchTypeName;

				row.MonasteryName = view.MonasteryName;
				row.MonasteryType = view.MonasteryType;
				row.MonasteryTypeName = view.MonasteryTypeName;
				row.MonasteryPhoneNo = view.MonasteryPhoneNo;
				row.DepositaryName = view.DepositaryName;
				row.DepositaryPhoneNo = view.DepositaryPhoneNo;
				row.MonasteryAreaRai = view.MonasteryAreaRai;
				row.MonasteryAreaNgan = view.MonasteryAreaNgan;
				row.MonasteryAreaWa = view.MonasteryAreaWa;
				row.LandRightsDocuments = view.LandRightsDocuments;

				row.EcclesiasticalTitle = view.EcclesiasticalTitle;
				row.EcclesiasticalType = view.EcclesiasticalType;
				row.AbbotTitle = view.AbbotTitle;
				row.AbbotName = view.AbbotName;
				row.AbbotType = view.AbbotType;
				row.AbbotTemple = view.AbbotTemple;
				row.Preceptor = view.Preceptor;
				row.PreceptorTemple = view.PreceptorTemple;
				row.AbbotPhoneNo = view.AbbotPhoneNo;
				row.AbbotEmail = view.AbbotEmail;
				row.AbbotLineId = view.AbbotLineId;
				row.AbbotImageUrl = view.AbbotImageUrl;
				row.CertifierName = view.CertifierName;
				row.CertifierTemple = view.CertifierTemple;
				row.AbbotSignDate = view.AbbotSignDate;

				row.DateOfAcceptPosition = view.DateOfAcceptPosition;
				row.DateOfFounding = view.DateOfFounding;
				row.DateOfBirth = view.DateOfBirth;
				row.DateOfOrdination = view.DateOfOrdination;

				row.AddressTextMonatery = view.AddressTextMonatery;
				row.HouseNoMonatery = view.HouseNoMonatery;
				row.MooMonatery = view.MooMonatery;
				row.VillageMonatery = view.VillageMonatery;
				row.RoadMonatery = view.RoadMonatery;
				row.SubDistrictMonatery = view.SubDistrictMonatery;
				row.DistrictMonatery = view.DistrictMonatery;
				row.ProvinceMonatery = view.ProvinceMonatery;
				row.CountryMonatery = view.CountryMonatery;
				row.PostCodeMonatery = view.PostCodeMonatery;

				//ctx.TBranches.Update(row);
				ctx.SaveChanges();

				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw;
			}
			return view;
		}
		public TBranchViewModel addBranch(TBranchViewModel view) 
		{
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				var row = ctx.TBranches.Where(x => 
						x.ActiveStatus == true && 
						x.MonasteryName.Contains(view.MonasteryName))
					.ToList();
				if (row.Any() ) 
					throw new Exception($"The MonasteryName [{view.MonasteryName}] is already. ");

				view.CreatedDate = DateTime.Now;
				ctx.TBranches.Add(view);

				transaction.Commit();
			}
			catch (Exception)
			{
				transaction.Rollback();
				throw;
			}
			return view;
		}
		public TBranchViewModel getBranch(int ID)
		{
			TBranchViewModel result;
			var row = ctx.TBranches.Where(o =>
					o.Id == ID &&
					o.ActiveStatus == _Record_Active)
					.ToList();

			if (!row.Any())
				throw new Exception($"Data not found by ID {ID}");

			result = _mapper.ToViewModel(row.First());
			return result;
		}
		public List<TBranchViewModel> listBranch(int branchType = 0)
		{
			List<TBranchViewModel> result = new List<TBranchViewModel>();
			var row = ctx.TBranches.Where(o =>
					o.ActiveStatus == _Record_Active )
					.AsNoTracking()
					.ToList();

			if (branchType != 0)
			{
				row = row.Where(o => o.BranchType == branchType )
					.ToList();
			}
			result = _mapper.ToViewModels( row);
			return result;
		}
	}
}
