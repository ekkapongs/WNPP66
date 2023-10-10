using WNPP_API.Models;
using WNPP_API.ViewModel;

namespace WNPP_API.Mapper
{
	public interface IBranchMapper
	{
		public TBranch ToModel(TBranchViewModel view);
		
		public TBranchViewModel ToViewModel(TBranch model);

		public List<TBranch> ToModels(List<TBranchViewModel> view);
		public List<TBranchViewModel> ToViewModels(List<TBranch> model);
	}
	public class BranchMapper : IBranchMapper
	{
		public BranchMapper() { }

		public List<TBranch> ToModels(List<TBranchViewModel> view)
		{
			List<TBranch> result = new List<TBranch>();
			foreach (var item in view)
				result.Add( ToModel(item));
			
			return result;
		}
		public List<TBranchViewModel> ToViewModels(List<TBranch> model)
		{
			List<TBranchViewModel> result = new List<TBranchViewModel>();
			foreach (TBranch modelItem in model)
				result.Add(ToViewModel(modelItem));

			return result;
		}
		public TBranchViewModel ToViewModel(TBranch model)
		{
			TBranchViewModel view = new TBranchViewModel();

			view.Id = model.Id;
			view.ActiveStatus = model.ActiveStatus;
			view.LanguageId = model.LanguageId;
			view.RecordStatus = model.RecordStatus;
			view.CreatedBy = model.CreatedBy;
			view.ModifiedBy = model.ModifiedBy;
			view.CreatedByName = model.CreatedByName;
			view.ModifiedByName = model.ModifiedByName;

			view.Notation = model.Notation;

			view.CreatedDate = model.CreatedDate;
			view.ModifiedDate = model.ModifiedDate;

			view.BranchName = model.BranchName;
			view.BranchType = model.BranchType;
			view.BranchTypeName = model.BranchTypeName;

			view.MonasteryName = model.MonasteryName;
			view.MonasteryType = model.MonasteryType;
			view.MonasteryTypeName = model.MonasteryTypeName;
			view.MonasteryPhoneNo = model.MonasteryPhoneNo;
			view.DepositaryName = model.DepositaryName;
			view.DepositaryPhoneNo = model.DepositaryPhoneNo;
			view.MonasteryAreaRai = model.MonasteryAreaRai;
			view.MonasteryAreaNgan = model.MonasteryAreaNgan;
			view.MonasteryAreaWa = model.MonasteryAreaWa;
			view.LandRightsDocuments = model.LandRightsDocuments;

			view.EcclesiasticalTitle = model.EcclesiasticalTitle;
			view.EcclesiasticalType = model.EcclesiasticalType;
			view.AbbotTitle = model.AbbotTitle;
			view.AbbotName = model.AbbotName;
			view.AbbotType = model.AbbotType;
			view.AbbotTemple = model.AbbotTemple;
			view.Preceptor = model.Preceptor;
			view.PreceptorTemple = model.PreceptorTemple;
			view.AbbotPhoneNo = model.AbbotPhoneNo;
			view.AbbotEmail = model.AbbotEmail;
			view.AbbotLineId = model.AbbotLineId;
			view.AbbotImageUrl = model.AbbotImageUrl;
			view.CertifierName = model.CertifierName;
			view.CertifierTemple = model.CertifierTemple;
			view.AbbotSignDate = model.AbbotSignDate;

			view.DateOfAcceptPosition = model.DateOfAcceptPosition;
			view.DateOfFounding = model.DateOfFounding;
			view.DateOfBirth = model.DateOfBirth;
			view.DateOfOrdination = model.DateOfOrdination;

			view.AddressTextMonatery = model.AddressTextMonatery;
			view.HouseNoMonatery = model.HouseNoMonatery;
			view.MooMonatery = model.MooMonatery;
			view.VillageMonatery = model.VillageMonatery;
			view.RoadMonatery = model.RoadMonatery;
			view.SubDistrictMonatery = model.SubDistrictMonatery;
			view.DistrictMonatery = model.DistrictMonatery;
			view.ProvinceMonatery = model.ProvinceMonatery;
			view.CountryMonatery = model.CountryMonatery;
			view.PostCodeMonatery = model.PostCodeMonatery;

			return view;
		}
		public TBranch ToModel(TBranchViewModel view)
		{
			TBranch model = new TBranch();

			model.Id = view.Id;
			model.ActiveStatus = view.ActiveStatus;
			model.LanguageId = view.LanguageId;
			model.RecordStatus = view.RecordStatus;
			model.CreatedBy = view.CreatedBy;
			model.ModifiedBy = view.ModifiedBy;
			model.CreatedByName = view.CreatedByName;
			model.ModifiedByName = view.ModifiedByName;

			model.Notation = view.Notation;

			model.CreatedDate = view.CreatedDate;
			model.ModifiedDate = view.ModifiedDate;

			model.BranchName = view.BranchName;
			model.BranchType = view.BranchType;
			model.BranchTypeName = view.BranchTypeName;

			model.MonasteryName = view.MonasteryName;
			model.MonasteryType = view.MonasteryType;
			model.MonasteryTypeName = view.MonasteryTypeName;
			model.MonasteryPhoneNo = view.MonasteryPhoneNo;
			model.DepositaryName = view.DepositaryName;
			model.DepositaryPhoneNo = view.DepositaryPhoneNo;
			model.MonasteryAreaRai = view.MonasteryAreaRai;
			model.MonasteryAreaNgan	= view.MonasteryAreaNgan;
			model.MonasteryAreaWa	= view.MonasteryAreaWa;
			model.LandRightsDocuments = view.LandRightsDocuments;
			
			model.EcclesiasticalTitle = view.EcclesiasticalTitle;
			model.EcclesiasticalType = view.EcclesiasticalType;
			model.AbbotTitle = view.AbbotTitle;
			model.AbbotName = view.AbbotName;
			model.AbbotType = view.AbbotType;
			model.AbbotTemple = view.AbbotTemple;
			model.Preceptor	= view.Preceptor;
			model.PreceptorTemple = view.PreceptorTemple;
			model.AbbotPhoneNo = view.AbbotPhoneNo;
			model.AbbotEmail = view.AbbotEmail;
			model.AbbotLineId = view.AbbotLineId;
			model.AbbotImageUrl = view.AbbotImageUrl;
			model.CertifierName = view.CertifierName;
			model.CertifierTemple = view.CertifierTemple;
			model.AbbotSignDate = view.AbbotSignDate;

			model.DateOfAcceptPosition = view.DateOfAcceptPosition;
			model.DateOfFounding = view.DateOfFounding;
			model.DateOfBirth = view.DateOfBirth;
			model.DateOfOrdination = view.DateOfOrdination;
			
			model.AddressTextMonatery = view.AddressTextMonatery;
			model.HouseNoMonatery = view.HouseNoMonatery;
			model.MooMonatery = view.MooMonatery;
			model.VillageMonatery = view.VillageMonatery;
			model.RoadMonatery = view.RoadMonatery;
			model.SubDistrictMonatery = view.SubDistrictMonatery;
			model.DistrictMonatery = view.DistrictMonatery;
			model.ProvinceMonatery = view.ProvinceMonatery;
			model.CountryMonatery = view.CountryMonatery;
			model.PostCodeMonatery = view.PostCodeMonatery;

			return model;
		}
	}
}
