using System.Runtime.InteropServices;
using WNPP_API.Models;

namespace WNPP_API.Services
{
	public interface IDropDownService
	{
		public List<MDropDown> getBranchType();
		public List<MDropDown> getLanguageType();
		public List<MDropDown> getMonasteryType();
		public List<MDropDown> getAcademicType();
		public List<MDropDown> getSageType();
		public List<MDropDown> getTheologianType();
	}
	public class DropDownService : CommonServices, IDropDownService
	{
		private readonly ILogger<DropDownService> _logger;
		private readonly Wnpp66Context ctx = new Wnpp66Context();
		public DropDownService() { }
		public DropDownService(ILogger<DropDownService> logger)
		{
		}
		public List<MDropDown> getTheologianType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where(o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_TheologianType))
					.ToList();

			result = row;
			return result;
		}

		public List<MDropDown> getSageType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where(o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_SageType))
					.ToList();

			result = row;
			return result;
		}
		public List<MDropDown> getAcademicType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where(o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_AcademicType))
					.ToList();

			result = row;
			return result;
		}

		public List<MDropDown> getMonasteryType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where(o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_MonasteryType))
					.ToList();

			result = row;
			return result;
		}
		public List<MDropDown> getLanguageType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where(o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_LanguageType))
					.ToList();

			result = row;
			return result;
		}

		public List<MDropDown> getBranchType()
		{
			List<MDropDown> result = new List<MDropDown>();
			var row = ctx.MDropDowns.Where( o =>
					o.ActiveStatus == _Record_Active &&
					o.DropDownType.Equals(_DropDown_BranchType))
					.ToList();

			result = row;
			return result;
		}
		public MDropDown createMDropDown(MDropDown model)
		{
			MDropDown result = null;
			var transaction = ctx.Database.BeginTransaction();
			try
			{
				ctx.MDropDowns.Add(model);
				result = model;
				ctx.SaveChanges();

				transaction.Commit();
			}
			catch (Exception)
			{
				throw;
				transaction.Rollback();
			}
			return result;
		}
	
	}
}
