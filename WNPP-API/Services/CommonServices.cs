using WNPP_API.Controllers;

namespace WNPP_API.Services
{
	public class CommonServices
	{
		/// === Administrator
		public const int _Admin_ID = 0;
		public const string _Admin_Name = "Administrator";

		/// === LANG
		public const int _Lang_TH = 0;
		public const int _Lang_EN = 1;

		/// === RECORD STATUS
		public const bool _Record_Active = true;
		public const bool _Record_DisActive = false;

		/// === DROP DOWN TYPE
		public const string _DropDown_BranchType = "BranchType";
		public const string _DropDown_LanguageType = "LanguageType";
		public const string _DropDown_MonasteryType = "MonasteryType";
		public const string _DropDown_AcademicType = "AcademicType";
		public const string _DropDown_SageType = "SageType";
		public const string _DropDown_TheologianType = "TheologianType";
	}
}
