// See https://aka.ms/new-console-template for more information

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using WNPP_API.Models;
using WNPP_API.Services;

using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;

using LicenseContext = OfficeOpenXml.LicenseContext;
using System.IO;

//test01();
//migrate03(); /// ==> Book Store

//migrate02(); /// ==> Branch
//migrate02_67(); /// ==> Branch
//migrate02_67_02(); /// ==> Branch ==> OK
//migrate02_67_02_1(); /// ==> Branch Get Image
migrate02_67_02_1_1(); /// ==> Branch Get Image

//migrate02_67_02_2(); /// ==> Test Branch 99 ==> Pass

//migrate02_67_02_24(); /// ===> Manage Image files /// Use EPPlus

void migrate02_67_02_24()
{
	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
	string excelFilePath = @"d:\NewContract2567v.2.04.05.xlsx";
	FileInfo fileInfo = new FileInfo(excelFilePath);

	List<string> base64Strings = GetImageAsBase64(fileInfo);

	//Convert2Html(fileInfo, base64Strings);
}
 static List<string> GetImageAsBase64(FileInfo uFileInfo)
{
	List<string> base64Strings = new List<string>();

	if (uFileInfo.Exists)
	{
		using (var package = new ExcelPackage(uFileInfo))
		{
			/// Sheet name [Index]
			ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
			ExcelDrawings drawings = worksheet.Drawings;
			int i = 0;
			foreach (ExcelPicture picture in drawings)
			{

				var excelImage = picture.Image as ExcelImage;
				var imageBytes = excelImage.ImageBytes;

				if (imageBytes != null)
				{

					/// ===> Debug ====
					i++;
					Console.WriteLine("Images'{0}' from '{1}' Cell '{2}{3}'",i, drawings.Count, picture.Position.X, picture.From.Row);


					/// ===> Debug ====
					//using (MemoryStream stream = new MemoryStream(imageBytes))
					//{
					//	Image image = Image.FromStream(stream);

					//	using (MemoryStream base64Stream = new MemoryStream())
					//	{
					//		image.Save(base64Stream, ImageFormat.Png);
					//		string base64String = Convert.ToBase64String(base64Stream.ToArray());
					//		base64Strings.Add(base64String);
					//	}

					//	i++;
					//}
				}
			}
		}
	}
	else
	{
		Console.WriteLine("File doesn't exist");
	}

	return base64Strings;
}

 static void Convert2Html(FileInfo uFileInfo, List<String> base64Strings)
{
	if (uFileInfo.Exists)
	{
		using (var package = new ExcelPackage(uFileInfo))
		{
			ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

			int rowCount = worksheet.Dimension.Rows;
			int columnCount = worksheet.Dimension.Columns;

			StringBuilder htmlBuilder = new StringBuilder();
			htmlBuilder.AppendLine("<html>");
			htmlBuilder.AppendLine("<head>");
			htmlBuilder.AppendLine("<link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css\">");
			htmlBuilder.AppendLine("</head>");
			htmlBuilder.AppendLine("<body>");
			htmlBuilder.AppendLine("<table class=\"table table-bordered\">");
			htmlBuilder.AppendLine("<tr>");

			//Header
			for (int col = 1; col <= columnCount; col++)
			{
				object cellValue = worksheet.Cells[1, col].Value;
				if (cellValue != null)
				{
					htmlBuilder.AppendLine($"<th>{cellValue}</th>");
					Console.WriteLine(cellValue);
				}
			}
			htmlBuilder.AppendLine("</tr>");

			int i = 0;
			//Rows
			for (int row = 2; row <= rowCount; row++)
			{
				htmlBuilder.AppendLine("<tr>");

				for (int col = 1; col <= columnCount; col++)
				{
					object cellValue = worksheet.Cells[row, col].Value;
					if (cellValue != null)
					{
						htmlBuilder.AppendLine($"<td>{cellValue}</td>");
						Console.WriteLine(cellValue);
					}

					//Image
					if (col == 6)
					{
						htmlBuilder.AppendLine($"<td><img src=\"data:image/png;base64,{base64Strings[i]}\" /></td>");
						i++;
					}
				}
				htmlBuilder.AppendLine("</tr>");
			}

			htmlBuilder.AppendLine("</table>");
			htmlBuilder.AppendLine("</body>");
			htmlBuilder.AppendLine("</html>");

			string htmlTable = htmlBuilder.ToString();
			string outputPath = @"D:\Branch\index.html";

			File.WriteAllText(outputPath, htmlTable);
			Console.WriteLine("HTML-File exported");
		}
	}
	else
	{
		Console.WriteLine("File doesn't exist");
	}
}
void migrate02_67_02_2()
{
	string fileName = @"d:\NewContract2567v.2.04.05.xlsx";

	int set_1_Subject = 1;
	int set_2_Add1 = 3;
	int set_3_Add2 = 4;
	int set_4_Abbot = 6;
	///=== สาขา สำรอง ===>
	int set_5_Ordinate = 8;
	///=== สำรวจ =======>
	//int set_5_Ordinate = 7;
	int set_6_Certifier = 8;
	int set_Phone = 9;
	int set_Reset = 10;


	Console.WriteLine(" === Open Connection === ");
	Wnpp66Context ctx = new Wnpp66Context();

	using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
	{
		using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
		{
			WorkbookPart workbookPart = doc.WorkbookPart;
			SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
			SharedStringTable sst = sstpart.SharedStringTable;

			Console.WriteLine(" === Load data Branch type 1. === ");
			migrateType1("สาขา", sst, doc, ctx);
			Console.WriteLine(" === Load data Branch type 2. === ");
			migrateType2("สำรอง", sst, doc, ctx);
			Console.WriteLine(" === Load data Branch type 3. === ");
			migrateType3("สำรวจ", sst, doc, ctx);

			Console.WriteLine(" === Finish ");
		}
	}
}
/// ===> สาขา
void migrateType1 (string sheetName, SharedStringTable sst, SpreadsheetDocument doc, Wnpp66Context ctx)
{
	WorksheetPart worksheetPart = GetWorksheetPartByName(doc, sheetName);
	Worksheet sheet = worksheetPart.Worksheet;

	string? data = null;
	string[] add = null;

	string cellColumn = "";
	int branchType = 1; /// 1, 2, 3

	List<TBranch> lstTBranch = new List<TBranch>();
	TBranch branch;
	int rowCount = 1, rowRecCount = 1;
	int maxRowCount = 2790;
	for (int i = 1; i <= maxRowCount; i++)
	{

		rowRecCount = 1;
		branch = getNewBranch();

		///=== 1 Subject ===
		///=================
		cellColumn = "B" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.BranchName = data;
		branch.BranchType = branchType;
		branch.BranchTypeName = sheetName;

		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.MonasteryName = data;
		if (branch.MonasteryName.StartsWith("วัด"))
		{
			branch.MonasteryType = 1;
			branch.MonasteryTypeName = "วัด";
			branch.AbbotType = 1;
		}
		else
		{
			branch.MonasteryType = 2;
			branch.MonasteryTypeName = "ที่พักสงฆ์";
			branch.AbbotType = 2;
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///3 === Add1 ===
		///==============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.AddressTextMonatery = data;

		rowRecCount++; i++;

		///4 === Add2 ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		add = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (add.Length == 5)
		{
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}
		else if (add.Length == 4)
		{
			if (add[3].Length > 5)
			{
				branch.CountryMonatery = add[3].Trim();
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}
			else
			{
				branch.CountryMonatery = "ไทย";
				branch.PostCodeMonatery = getArabicnumber(add[3].Trim());
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}

		}
		else if (add.Length == 3)
		{
			branch.CountryMonatery = "ไทย";
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///6 === Abbot ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.AbbotName = data;

		rowRecCount++; i++;
		rowRecCount++; i++;

		///8 === Ordinate ====
		///===================
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.Notation = data;
		branch.DateOfBirth = getDateOfBirth(data);
		branch.DateOfOrdination =  getOrdination(data);

		rowRecCount++; i++;

		///9 === Phone ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		data = data.Replace("-", "");
		branch.MonasteryPhoneNo = data;

		rowRecCount++; i++;

		lstTBranch.Add(branch);
	}
	ctx.TBranches.AddRange(lstTBranch);
	ctx.SaveChanges();
}
/// ===> สำรอง
void migrateType2(string sheetName, SharedStringTable sst, SpreadsheetDocument doc, Wnpp66Context ctx)
{
	WorksheetPart worksheetPart = GetWorksheetPartByName(doc, sheetName);
	Worksheet sheet = worksheetPart.Worksheet;

	string? data = null;
	string[] add = null;

	string cellColumn = "";
	int branchType = 2; /// 1, 2, 3

	List<TBranch> lstTBranch = new List<TBranch>();
	TBranch branch;
	int rowCount = 1, rowRecCount = 1;
	int maxRowCount = 580;
	for (int i = 1; i <= maxRowCount; i++)
	{

		rowRecCount = 1;
		branch = getNewBranch();

		///=== 1 Subject ===
		///=================
		cellColumn = "B" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.BranchName = data;
		branch.BranchType = branchType;
		branch.BranchTypeName = sheetName;

		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.MonasteryName = data;
		if (branch.MonasteryName.StartsWith("วัด"))
		{
			branch.MonasteryType = 1;
			branch.MonasteryTypeName = "วัด";
			branch.AbbotType = 1;
		}
		else
		{
			branch.MonasteryType = 2;
			branch.MonasteryTypeName = "ที่พักสงฆ์";
			branch.AbbotType = 2;
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///3 === Add1 ===
		///==============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.AddressTextMonatery = data;

		rowRecCount++; i++;

		///4 === Add2 ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		add = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (add.Length == 5)
		{
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}
		else if (add.Length == 4)
		{
			if (add[3].Length > 5)
			{
				branch.CountryMonatery = add[3].Trim();
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}
			else
			{
				branch.CountryMonatery = "ไทย";
				branch.PostCodeMonatery = getArabicnumber(add[3].Trim());
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}
		}
		else if (add.Length == 3)
		{
			branch.CountryMonatery = "ไทย";
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///6 === Abbot ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.AbbotName = data;

		rowRecCount++; i++;
		rowRecCount++; i++;

		///8 === Ordinate ====
		///===================
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.Notation = data;
		branch.DateOfBirth = getDateOfBirth(data);
		branch.DateOfOrdination = getOrdination(data);

		rowRecCount++; i++;

		///9 === Phone ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		data = data.Replace("-", "");
		branch.MonasteryPhoneNo = data;

		rowRecCount++; i++;

		lstTBranch.Add(branch);
	}
	ctx.TBranches.AddRange(lstTBranch);
	ctx.SaveChanges();
}
/// ===> สำรวจ
void migrateType3(string sheetName, SharedStringTable sst, SpreadsheetDocument doc, Wnpp66Context ctx)
{
	WorksheetPart worksheetPart = GetWorksheetPartByName(doc, sheetName);
	Worksheet sheet = worksheetPart.Worksheet;

	string tmp = "";
	string? data = null;
	string[] add = null;

	string cellColumn = "";
	int branchType = 3; /// 1, 2, 3

	List<TBranch> lstTBranch = new List<TBranch>();
	TBranch branch;
	int rowCount = 1, rowRecCount = 1;
	int maxRowCount = 700;
	for (int i = 1; i <= maxRowCount; i++)
	{

		rowRecCount = 1;
		branch = getNewBranch();

		///=== 1 Subject ===
		///=================
		cellColumn = "B" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.BranchName = data;
		branch.BranchType = branchType;
		branch.BranchTypeName = sheetName;

		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.MonasteryName = data;
		if (branch.MonasteryName.StartsWith("วัด"))
		{
			branch.MonasteryType = 1;
			branch.MonasteryTypeName = "วัด";
			branch.AbbotType = 1;
		}
		else
		{
			branch.MonasteryType = 2;
			branch.MonasteryTypeName = "ที่พักสงฆ์";
			branch.AbbotType = 2;
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///3 === Add1 ===
		///==============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.AddressTextMonatery = data;

		rowRecCount++; i++;

		///4 === Add2 ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		add = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		if (add.Length == 5)
		{
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}
		else if (add.Length == 4)
		{
			if (add[3].Length > 5)
			{
				branch.CountryMonatery = add[3].Trim();
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}
			else
			{
				branch.CountryMonatery = "ไทย";
				branch.PostCodeMonatery = getArabicnumber(add[3].Trim());
				branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
				branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
				branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
			}
		}
		else if (add.Length == 3)
		{
			branch.CountryMonatery = "ไทย";
			branch.ProvinceMonatery = add[2].Trim().Replace("จ.", "");
			branch.DistrictMonatery = add[1].Trim().Replace("อ.", "");
			branch.SubDistrictMonatery = add[0].Trim().Replace("ต.", "");
		}

		rowRecCount++; i++;
		rowRecCount++; i++;

		///6 === Abbot ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		branch.AbbotName = data;
		
		rowRecCount++; i++;

		///7 === Ordinate ====
		///===================
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		branch.Notation = data;
		branch.DateOfBirth = getDateOfBirth(data);
		branch.DateOfOrdination = getOrdination(data);
		
		rowRecCount++; i++;

		///8 === Certifier ===
		///===================
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? data.Trim() : "";
		data = data.Replace("รับรอง", "");
		add = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		tmp = "";
		for (int j = 0; j < add.Length - 1; j++)
		{
			tmp = tmp + add[j] + " ";
		}
		tmp = tmp.Trim();
		branch.CertifierName = tmp;
		branch.CertifierTemple = add[add.Length - 1];
		
		rowRecCount++; i++;
		
		///9 === Phone ===
		///===============
		cellColumn = "C" + i;
		data = getCellData(cellColumn, sheet, sst);
		data = data != null ? getArabicnumber(data.Trim()) : "";
		data = data.Replace("-", "");
		branch.MonasteryPhoneNo = data;

		rowRecCount++; i++;

		lstTBranch.Add(branch);
	}
	ctx.TBranches.AddRange(lstTBranch);
	ctx.SaveChanges();
}
TBranch getNewBranch()
{
	return new TBranch()
	{
		ActiveStatus = true,
		LanguageId = 1,
		RecordStatus = "c",
		CreatedDate = DateTime.Now,
		CreatedByName = "Administrator"
	};
}
DateTime? getDateOfBirth(String data)
{
	int ageYear = 0;
	string dateType = "MM/dd/yyyy";
	string dateNull = "01/01/1777";
	string age = "";

	string[] convData;
	string newDate;
	DateTime result = new DateTime();
	DateTime odinationDate = new DateTime();

	if (data.IndexOf("วันที่") > 0)
	{
		age = data.Substring(data.IndexOf("อุปสมบท เมื่ออายุ")).Trim();
		data = data.Substring(data.IndexOf("วันที่")).Trim();

		convData = age.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		bool canConvert = int.TryParse("-" + convData[2], out ageYear);
		if (canConvert == false)
		{
			result = DateTime.ParseExact(dateNull, dateType, null);
		}
		else
		{
			convData = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			newDate = String.Format("{0:00}", int.Parse(getMonthNumber(convData[2]))) + "/" +
						String.Format("{0:00}", int.Parse(convData[1])) + "/" +
						(int.Parse(convData[3]) - 543);

			odinationDate = DateTime.ParseExact(newDate, dateType, null);
			result = odinationDate.AddYears(ageYear);
		}
	}
	else
	{
		result = DateTime.ParseExact(dateNull, dateType, null);
	}

	return result;
}
DateTime? getOrdination(String data)
{
	string dateType = "MM/dd/yyyy";
	string dateNull = "01/01/1777";

	string[] convData;
	string newDate;
	DateTime result = new DateTime();

	if (data.IndexOf("วันที่") > 0)
	{
		data = data.Substring(data.IndexOf("วันที่")).Trim();
		convData = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		//newDate = (int.Parse(convData[3]) - 543) + "/" + String.Format("{0:00}", int.Parse(getMonthNumber(convData[2])))  + "/" + String.Format("{0:00}", int.Parse(convData[1]));

		newDate =	String.Format("{0:00}", int.Parse(getMonthNumber(convData[2]))) + "/" +
					String.Format("{0:00}", int.Parse(convData[1])) + "/" +
					(int.Parse(convData[3]) - 543);

		result = DateTime.ParseExact(newDate, dateType, null);
	}
	else
	{
		result = DateTime.ParseExact(dateNull, dateType, null);
	}
	return result;
}
String? getMonthNumber(String monthName)
{
	String result = null;
	switch (monthName){
		case "มกราคม":
			result = "1";
			break;
		case "กุมภาพันธ์":
			result = "2";
			break;
		case "มีนาคม":
			result = "3";
			break;
		case "เมษายน":
			result = "4";
			break;
		case "พฤษภาคม":
			result = "5";
			break;
		case "มิถุนายน":
			result = "6";
			break;
		case "กรกฎาคม":
			result = "7";
			break;
		case "สิงหาคม":
			result = "8";
			break;
		case "กันยายน":
			result = "9";
			break;
		case "ตุลาคม":
			result = "10";
			break;
		case "พฤศจิกายน":
			result = "11";
			break;
		case "ธันวาคม":
			result = "12";
			break;
	}
	return result;
} 
String? getCellData(String cellAddress, Worksheet sheet, SharedStringTable sst)
{
	String result = null;
	Cell theCell = sheet.Descendants<Cell>().Where(c => c.CellReference == cellAddress).FirstOrDefault();
	if (theCell != null)
	if ((theCell.DataType != null) && (theCell.DataType == CellValues.SharedString))
		result = sst.ChildElements[int.Parse(theCell.CellValue.Text)].InnerText;
	
	return result;
}
void migrate02_67_02_1_1()
{
	{
		string fileName = @"d:\NewContract2567v.2.04.05.xlsx";
		string sheetName = "สำรอง";
		
		string filesOut = @"D:\Branch\" + sheetName + "\\";

		using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
			{
				WorkbookPart workbookPart = doc.WorkbookPart;

				WorksheetPart workSheet = GetWorksheetPartByName(doc, sheetName);
				//Worksheet workSheet = worksheetPart.Worksheet;

				//WorkbookPart workbookPart = doc.WorkbookPart;
				//var workSheet = workbookPart.WorksheetParts.FirstOrDefault();

				foreach (ImagePart i in workSheet.DrawingsPart.ImageParts)
				{
					var rId = workSheet.DrawingsPart.GetIdOfPart(i);

					Stream stream = i.GetStream();
					long length = stream.Length;
					byte[] byteStream = new byte[length];
					stream.Read(byteStream, 0, (int)length);

					//var imageAsString = Convert.ToBase64String(byteStream);
					//Console.WriteLine("The rId of this Image is '{0}' data {1}", rId, imageAsString);
					Console.WriteLine("The rId of this Image is '{0}' data {1}", rId, i);
					Image img = System.Drawing.Image.FromStream(i.GetStream());
					img.Save(filesOut + rId + ".jpg", ImageFormat.Jpeg);
				}

			}
		}
	}
}
void migrate02_67_02_1()
{
	{
		string fileName = @"d:\NewContract2567v.2.04.05.xlsx";
		string filesOut = @"d:\Test01\";

		string sheetName = "สำรวจ";

		using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
			{
				WorkbookPart workbookPart = doc.WorkbookPart;
				var workSheet = workbookPart.WorksheetParts.FirstOrDefault();

				foreach (ImagePart i in workSheet.DrawingsPart.ImageParts)
				{
					var rId = workSheet.DrawingsPart.GetIdOfPart(i);

					Stream stream = i.GetStream();
					long length = stream.Length;
					byte[] byteStream = new byte[length];
					stream.Read(byteStream, 0, (int)length);

					//var imageAsString = Convert.ToBase64String(byteStream);
					//Console.WriteLine("The rId of this Image is '{0}' data {1}", rId, imageAsString);

					Console.WriteLine("The rId of this Image is '{0}' data {1}", rId, i.ContentType);
					Image img = System.Drawing.Image.FromStream(stream);
					img.Save(filesOut + rId+".jpg", ImageFormat.Jpeg);
				}

			}
		}
	}
}
void migrate02_67_02()
{
	string fileName = @"d:\AllNewContract2567v.1.00.01.xlsx";
	string sheetName = "สำรวจ";

	using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
	{
		using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
		{
			WorkbookPart workbookPart = doc.WorkbookPart;
			SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
			SharedStringTable sst = sstpart.SharedStringTable;

			WorksheetPart worksheetPart = GetWorksheetPartByName(doc, sheetName);
			Worksheet sheet = worksheetPart.Worksheet;

			var cells = sheet.Descendants<Cell>();
			var rows = sheet.Descendants<Row>();

			Console.WriteLine("Row count = {0}", rows.LongCount());
			Console.WriteLine("Cell count = {0}", cells.LongCount());

				/// One way: go through each cell in the sheet
				foreach (Cell cell in cells)
				{
					if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
					{
						int ssid = int.Parse(cell.CellValue.Text);
						string str = sst.ChildElements[ssid].InnerText;
						Console.WriteLine("Cell string {0}: {1}: {2}", ssid, cell.CellReference, str);
					}
					else if (cell.CellValue != null)
					{
						Console.WriteLine("Cell contents: {0}", cell.CellValue.Text);
					}
				}
			}
	}
}

static WorksheetPart GetWorksheetPartByName(SpreadsheetDocument document, string sheetName)
{
	IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>().Where(s => s.Name == sheetName);

	if (sheets.Count() == 0)
	{
		// The specified worksheet does not exist.
		return null;
	}

	string relationshipId = sheets.First().Id.Value;
	WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(relationshipId);

	return worksheetPart;
}

static Cell GetCell(Worksheet worksheet, string columnName, uint rowIndex)
{
	Row row = GetRow(worksheet, rowIndex);

	if (row == null)
	{
		return null;
	}

	return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName + rowIndex, true) == 0).First();
}
static Row GetRow(Worksheet worksheet, uint rowIndex)
{
	return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
}
void migrate02_67()
{
	string fileName = @"d:\AllNewContract2567v.1.00.01.xlsx";
	using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
	{
		using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
		{
			WorkbookPart workbookPart = doc.WorkbookPart;
			SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
			SharedStringTable sst = sstpart.SharedStringTable;

			WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
			Worksheet sheet = worksheetPart.Worksheet;

			var cells = sheet.Descendants<Cell>();
			var rows = sheet.Descendants<Row>();

			Console.WriteLine("Row count = {0}", rows.LongCount());
			Console.WriteLine("Cell count = {0}", cells.LongCount());

			// One way: go through each cell in the sheet
			foreach (Cell cell in cells)
			{
				if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
				{
					int ssid = int.Parse(cell.CellValue.Text);
					string str = sst.ChildElements[ssid].InnerText;
					Console.WriteLine("Cell string {0}: {1}: {2}", ssid, cell.CellReference, str);
				}
				else if (cell.CellValue != null)
				{
					Console.WriteLine("Cell contents: {0}", cell.CellValue.Text);
				}
			}

			// Or... via each row
			//foreach (Row row in rows)
			//{
			//	foreach (Cell c in row.Elements<Cell>())
			//	{
			//		if ((c.DataType != null) && (c.DataType == CellValues.SharedString))
			//		{
			//			int ssid = int.Parse(c.CellValue.Text);
			//			string str = sst.ChildElements[ssid].InnerText;
			//			Console.WriteLine("Row string {0}: {1}: {2}", ssid, c.CellReference, str);
			//		}
			//		else if (c.CellValue != null)
			//		{
			//			Console.WriteLine("Cell contents: {0}", c.CellValue.Text);
			//		}
			//	}
			//}
		}
	}
}
void migrate03()
{
	Console.WriteLine(" === Open Connection === ");
	Wnpp66Context ctx = new Wnpp66Context();
	TBook book = null;
	List<TBook> listBook = new List<TBook>();

	book = new TBook()
	{
		ActiveStatus = CommonServices._Record_Active,
		Language = CommonServices._Lang_TH,
		CreatedBy = CommonServices._Admin_ID,
		CreatedByName = CommonServices._Admin_Name,
		CreatedDate = DateTime.Now,
		Isbn = "0",
		BookName = "คู่มือทำวัตร – สวดมนต์แปล",
		Author = "พระโพธิญาณเถร (ชา สุภทฺโท)",
		Publisher = "ศูนย์ฯ",
		IsDhammaHeritage = true,
		Cost = 25,
		InStock = 1520,
	};
	listBook.Add(book);

	book = new TBook()
	{
		ActiveStatus = CommonServices._Record_Active,
		Language = CommonServices._Lang_TH,
		CreatedBy = CommonServices._Admin_ID,
		CreatedByName = CommonServices._Admin_Name,
		CreatedDate = DateTime.Now,
		Isbn = "1",
		BookName = "อุปลมณี",
		Author = "พระโพธิญาณเถร (ชา สุภทฺโท)",
		Publisher = "ศูนย์ฯ",
		IsDhammaHeritage = true,
		Cost = 300,
		InStock = 144,
	};
	listBook.Add(book);

	book = new TBook()
	{
		ActiveStatus = CommonServices._Record_Active,
		Language = CommonServices._Lang_TH,
		CreatedBy = CommonServices._Admin_ID,
		CreatedByName = CommonServices._Admin_Name,
		CreatedDate = DateTime.Now,
		Isbn = "2",
		BookName = "น้ำไหลนิ่ง",
		Author = "พระโพธิญาณเถร (ชา สุภทฺโท)",
		Publisher = "ศูนย์ฯ",
		IsDhammaHeritage = true,
		Cost = 30,
		InStock = 3708,
	};
	listBook.Add(book);

	ctx.TBooks.AddRange(listBook);
	ctx.SaveChanges();
}

void migrate02()
{
	Console.WriteLine(" === Read Excel Data === ");
	//String docName = @"d:\Branch25661001.v.3.xlsx";
	//String docName = @"d:\branch.v.4.csv";
	//string docName = @"C:\Users\Public\Documents\Sheet4.xlsx";
	//String docName = @"d:\branch.v.4.xlsx";
	//String docName = @"d:\branch.v.5.xlsx";
	//String docName = @"d:\B01.csv";
	//String docName = @"d:\B08.xlsx";

	//String docName = @"d:\b1.xlsx";
	//String docName = @"d:\c1.xlsx";
	String docName = @"d:\d1.xlsx";

	///=== สาขา
	//string worksheetName = "สาขา";
	//string startNewRow = "สาขาที่";
	//int branchType = 1;
	//string branchTypeName = worksheetName;

	///=== สำรอง
	//string worksheetName = "สำรอง";
	//string startNewRow = "สำรองที่";
	//int branchType = 2;
	//string branchTypeName = worksheetName;

	///=== สำรวจ
	string worksheetName = "สำรวจ";
	string startNewRow = "สำรวจที่";
	int branchType = 3;
	string branchTypeName = worksheetName;

	/// START cloumn [A B C , D E F ]
	string currentColumn = "A";

	string monasteryTypeName1 = "วัด";
	string monasteryTypeName2 = "ที่พักสงฆ์";

	string startNewAdd = "ที่ตั้ง";
	string startNewCall = "โทร";
	string startNewOrd = "อุปสมบท";
	string startCertifier = "ผู้รับรอง";

	Console.WriteLine(" === Open Connection === ");
	Wnpp66Context ctx = new Wnpp66Context();

	string cell1 = "";
	string cell2 = "";

	string BranchName; 
	string MonasteryName;
	string AddressText_Monatery;
	string AbbotName;
	string MonasteryPhoneNO;

	/// START row [1 2 3 4 5 6 7 ]
	int currentRow = 1;
	//int currentRow = 743;
	
	/// Step next row [7]
	int rowStep = 7;
	int rowAll = 979;

	List<TBranch> branches = new List<TBranch>();
	TBranch branch1 = null, branch2 = null;

	Console.WriteLine(" === Start migration. === ");
	for (int row = currentRow; row < rowAll; row++)
	{
		Console.WriteLine(" === read row {0} === ", row);

		cell1 = GetCellValue(docName, worksheetName, "A" + row);
		cell2 = GetCellValue(docName, worksheetName, "D" + row);

		if (string.IsNullOrEmpty(cell1) || string.IsNullOrEmpty(cell2))
		{
			Console.WriteLine(" === Create New at row {0} === ", row);
			if (branch1 != null)
			{
				ctx.TBranches.Add(branch1);
			}
			if (branch2 != null)
			{
				ctx.TBranches.Add(branch2);
			}
		}
		else if (cell1.StartsWith(startNewRow) || cell2.StartsWith(startNewRow))
		{
			if (!string.IsNullOrEmpty(cell1))
			{
				BranchName = cell1.Split(' ')[0] + cell1.Split(' ')[1];
				MonasteryName = cell1.Split(' ')[2];

				branch1 = new TBranch()
				{
					BranchName = BranchName.Trim(),
					BranchType = branchType,
					BranchTypeName = branchTypeName,

					MonasteryName = MonasteryName.Trim(),

					ActiveStatus = true,
					LanguageId = 1,
					CreatedDate = DateTime.Now,

				};
				if (branch1.MonasteryName.StartsWith(monasteryTypeName1))
				{
					branch1.MonasteryType = 1;
					branch1.MonasteryTypeName = monasteryTypeName1;
				}
				else
				{
					branch1.MonasteryType = 2;
					branch1.MonasteryTypeName = monasteryTypeName2;
				}
				branch1.BranchName = getArabicnumber(branch1.BranchName);
			}

			if (!string.IsNullOrEmpty(cell2))
			{
				if (cell2.Split(' ').Length >2)
				{
					BranchName = cell2.Split(' ')[0] + cell2.Split(' ')[1];
					MonasteryName = cell2.Split(' ')[2];

					branch2 = new TBranch()
					{
						BranchName = BranchName.Trim(),
						MonasteryName = MonasteryName.Trim(),
						BranchType = branchType,
						BranchTypeName = branchTypeName,

						ActiveStatus = true,
						LanguageId = 1,
						CreatedDate = DateTime.Now,

					};
					if (branch2.MonasteryName.StartsWith(monasteryTypeName1))
					{
						branch2.MonasteryType = 1;
						branch2.MonasteryTypeName = monasteryTypeName1;
					}
					else
					{
						branch2.MonasteryType = 2;
						branch2.MonasteryTypeName = monasteryTypeName2;
					}
					branch2.BranchName = getArabicnumber(branch2.BranchName);
				}
			}
				
		}
		else if (cell1.StartsWith(startNewAdd) || cell2.StartsWith(startNewAdd))
		{
			cell1 = GetCellValue(docName, worksheetName, "B" + row);
			cell2 = GetCellValue(docName, worksheetName, "E" + row);

			if (!string.IsNullOrEmpty(cell1))
			{
				AddressText_Monatery = cell1.Trim();
				AddressText_Monatery = AddressText_Monatery.Replace("\n", "");
				branch1.AddressTextMonatery = getArabicnumber(AddressText_Monatery);

			}

			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
			{
				AddressText_Monatery = cell2.Trim();
				AddressText_Monatery = AddressText_Monatery.Replace("\n", "");
				branch2.AddressTextMonatery = getArabicnumber(AddressText_Monatery);

			}

			cell1 = GetCellValue(docName, worksheetName, "C" + row);
			cell2 = GetCellValue(docName, worksheetName, "F" + row);

			if (!string.IsNullOrEmpty(cell1))
			{
				AbbotName = cell1.Trim();
				AbbotName = AbbotName.Replace("รูปถ่าย", "");
				AbbotName = AbbotName.Replace("รูปภาพ", "");
				AbbotName = AbbotName.Replace("ประธานสงฆ์", "");
				AbbotName = AbbotName.Replace("เจ้าอาวาส", "");
				AbbotName = AbbotName.Replace("\n", "");
				AbbotName = AbbotName.Trim();
				branch1.AbbotName = AbbotName;

			}

			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
			{
				AbbotName = cell2.Trim();
				AbbotName = AbbotName.Replace("รูปถ่าย", "");
				AbbotName = AbbotName.Replace("รูปภาพ", "");
				AbbotName = AbbotName.Replace("ประธานสงฆ์", "");
				AbbotName = AbbotName.Replace("เจ้าอาวาส", "");
				AbbotName = AbbotName.Replace("\n", "");
				AbbotName = AbbotName.Trim();
				branch2.AbbotName = AbbotName;

			}

		}
		else if (cell1.StartsWith(startNewCall) || cell2.StartsWith(startNewCall))
		{
			cell1 = GetCellValue(docName, worksheetName, "B" + row);
			cell2 = GetCellValue(docName, worksheetName, "E" + row);

			if (!string.IsNullOrEmpty(cell1))
			{
				MonasteryPhoneNO = cell1.Replace("-", "");
				MonasteryPhoneNO = MonasteryPhoneNO.Trim();
				branch1.MonasteryPhoneNo = getArabicnumber(MonasteryPhoneNO);
			}
			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
			{
				MonasteryPhoneNO = cell2.Replace("-", "");
				MonasteryPhoneNO = MonasteryPhoneNO.Trim();
				branch2.MonasteryPhoneNo = getArabicnumber(MonasteryPhoneNO);

			}

			if (branchType < 3)
			{
				row++;

				cell1 = GetCellValue(docName, worksheetName, "B" + row);
				cell2 = GetCellValue(docName, worksheetName, "E" + row);

				if (!string.IsNullOrEmpty(cell1))
				{
					MonasteryPhoneNO = cell1.Replace("-", "");
					MonasteryPhoneNO = MonasteryPhoneNO.Trim();
					branch1.MonasteryPhoneNo = branch1.MonasteryPhoneNo + ","
												+ getArabicnumber(MonasteryPhoneNO);
				}
				if (!string.IsNullOrEmpty(cell2) && branch2 != null)
				{
					MonasteryPhoneNO = cell2.Replace("-", "");
					MonasteryPhoneNO = MonasteryPhoneNO.Trim();
					branch2.MonasteryPhoneNo = branch2.MonasteryPhoneNo + ","
												+ getArabicnumber(MonasteryPhoneNO);
				}
			}
			
		}
		else if (cell1.StartsWith(startNewOrd) || cell2.StartsWith(startNewOrd))
		{
			branch1.Notation = cell1.Trim();
			branch2.Notation = cell2.Trim();

			row++;
			cell1 = GetCellValue(docName, worksheetName, "B" + row);
			cell2 = GetCellValue(docName, worksheetName, "E" + row);

			if(!string.IsNullOrEmpty(cell1))
				branch1.Notation = branch1.Notation + " " + cell1.Trim();
			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
				branch2.Notation = branch2.Notation + " " + cell2.Trim();

			branch1.Notation = getArabicnumber(branch1.Notation);
			branch2.Notation = getArabicnumber(branch2.Notation);
		}
		else if (cell1.StartsWith(startCertifier) || cell2.StartsWith(startCertifier))
		{
			branch1.CertifierTemple = cell1.Trim();
			branch2.CertifierTemple = cell2.Trim();

			row++;
			cell1 = GetCellValue(docName, worksheetName, "A" + row);
			cell2 = GetCellValue(docName, worksheetName, "D" + row);
			branch1.CertifierName = cell1.Trim();
			branch2.CertifierName = cell2.Trim();
		}
		else
		{
			Console.WriteLine(" === please check at row {0} === ", row);
		}

	}
	ctx.SaveChanges();
}
static string getArabicnumber(string source)
{
	string result = source;
	result = result.Replace("๑", "1");
	result = result.Replace("๒", "2");
	result = result.Replace("๓", "3");
	result = result.Replace("๔", "4");
	result = result.Replace("๕", "5");
	result = result.Replace("๖", "6");
	result = result.Replace("๗", "7");
	result = result.Replace("๘", "8");
	result = result.Replace("๙", "9");
	result = result.Replace("๐", "0");

	return result;
}
// Retrieve the value of a cell, given a file name, sheet name, 
// and address name.
static string GetCellValue(string fileName,
	string sheetName,
	string addressName)
{
	string value = null;

	// Open the spreadsheet document for read-only access.
	using (SpreadsheetDocument document =
		SpreadsheetDocument.Open(fileName, false))
	{
		// Retrieve a reference to the workbook part.
		WorkbookPart wbPart = document.WorkbookPart;

		// Find the sheet with the supplied name, and then use that 
		// Sheet object to retrieve a reference to the first worksheet.
		Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
		  Where(s => s.Name == sheetName).FirstOrDefault();

		// Throw an exception if there is no sheet.
		if (theSheet == null)
		{
			throw new ArgumentException("sheetName");
		}

		// Retrieve a reference to the worksheet part.
		WorksheetPart wsPart =
			(WorksheetPart)(wbPart.GetPartById(theSheet.Id));

		// Use its Worksheet property to get a reference to the cell 
		// whose address matches the address you supplied.
		Cell theCell = wsPart.Worksheet.Descendants<Cell>().
		  Where(c => c.CellReference == addressName).FirstOrDefault();

		// If the cell does not exist, return an empty string.
		if (theCell != null)
		{
			if (theCell.InnerText.Length > 0)
			{
				value = theCell.InnerText;

				// If the cell represents an integer number, you are done. 
				// For dates, this code returns the serialized value that 
				// represents the date. The code handles strings and 
				// Booleans individually. For shared strings, the code 
				// looks up the corresponding value in the shared string 
				// table. For Booleans, the code converts the value into 
				// the words TRUE or FALSE.
				if (theCell.DataType != null)
				{
					switch (theCell.DataType.Value)
					{
						case CellValues.SharedString:

							// For shared strings, look up the value in the
							// shared strings table.
							var stringTable =
								wbPart.GetPartsOfType<SharedStringTablePart>()
								.FirstOrDefault();

							// If the shared string table is missing, something 
							// is wrong. Return the index that is in
							// the cell. Otherwise, look up the correct text in 
							// the table.
							if (stringTable != null)
							{
								value =
									stringTable.SharedStringTable
									.ElementAt(int.Parse(value)).InnerText;
							}
							break;

						case CellValues.Boolean:
							switch (value)
							{
								case "0":
									value = "FALSE";
									break;
								default:
									value = "TRUE";
									break;
							}
							break;
					}
				}
			}
		}
		else
		{
			value = "";
		}
		
	}
	return value;
}
// Given a document name, a worksheet name, and a cell name, gets the column of the cell and returns
// the content of the first cell in that column.
static string GetColumnHeading(string docName, string worksheetName, string cellName)
{
	// Open the document as read-only.
	using (SpreadsheetDocument document = SpreadsheetDocument.Open(docName, false))
	{
		IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
		if (sheets.Count() == 0)
		{
			// The specified worksheet does not exist.
			return null;
		}

		WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);

		// Get the column name for the specified cell.
		string columnName = GetColumnName(cellName);

		// Get the cells in the specified column and order them by row.
		IEnumerable<Cell> cells = worksheetPart.Worksheet.Descendants<Cell>().Where(c => string.Compare(GetColumnName(c.CellReference.Value), columnName, true) == 0)
			.OrderBy(r => GetRowIndex(r.CellReference));

		if (cells.Count() == 0)
		{
			// The specified column does not exist.
			return null;
		}

		// Get the first cell in the column.
		Cell headCell = cells.First();

		// If the content of the first cell is stored as a shared string, get the text of the first cell
		// from the SharedStringTablePart and return it. Otherwise, return the string value of the cell.
		if (headCell.DataType != null && headCell.DataType.Value == CellValues.SharedString)
		{
			SharedStringTablePart shareStringPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
			SharedStringItem[] items = shareStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();
			return items[int.Parse(headCell.CellValue.Text)].InnerText;
		}
		else
		{
			return headCell.CellValue.Text;
		}
	}
}
// Given a cell name, parses the specified cell to get the column name.
 static string GetColumnName(string cellName)
{
	// Create a regular expression to match the column name portion of the cell name.
	Regex regex = new Regex("[A-Za-z]+");
	Match match = regex.Match(cellName);

	return match.Value;
}

// Given a cell name, parses the specified cell to get the row index.
 static uint GetRowIndex(string cellName)
{
	// Create a regular expression to match the row index portion the cell name.
	Regex regex = new Regex(@"\d+");
	Match match = regex.Match(cellName);

	return uint.Parse(match.Value);
}
void DataMigration01()
{
	System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
	//String excelPath = @"d:/Branch25661001.v.3.xlsx";
	//String excelPath = @"d:/branch.v.4.xlsx";
	String excelPath = @"d:/branch.v.4.csv";

	Console.WriteLine(" === Read: " + excelPath);
	using (SpreadsheetDocument document = SpreadsheetDocument.Open(excelPath, false))
	{
		Console.WriteLine(" === Read: " + excelPath);
	}
		//using var stream = new FileStream(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
		//var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
		//{
		// Gets or sets the encoding to use when the input XLS lacks a CodePage
		// record, or when the input CSV lacks a BOM and does not parse as UTF8. 
		// Default: cp1252 (XLS BIFF2-5 and CSV only)
		//FallbackEncoding = Encoding.GetEncoding(1252),

		// Gets or sets the password used to open password protected workbooks.
		//Password = "password",

		// Gets or sets an array of CSV separator candidates. The reader 
		// autodetects which best fits the input data. Default: , ; TAB | # 
		// (CSV only)
		//AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' },

		// Gets or sets a value indicating whether to leave the stream open after
		// the IExcelDataReader object is disposed. Default: false
		//LeaveOpen = false,

		// Gets or sets a value indicating the number of rows to analyze for
		// encoding, separator and field count in a CSV. When set, this option
		// causes the IExcelDataReader.RowCount property to throw an exception.
		// Default: 0 - analyzes the entire file (CSV only, has no effect on other
		// formats)
		//AnalyzeInitialCsvRows = 0,
		//});
		//reader = ExcelReaderFactory.CreateCsvReader(stream);
		//var ds = reader.AsDataSet();

		Console.WriteLine(" === Finish ...");
}
