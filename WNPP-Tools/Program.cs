// See https://aka.ms/new-console-template for more information

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;
using WNPP_API.Models;
using WNPP_API.Services;


//test01();
migrate02(); /// ==> Branch
//migrate03(); /// ==> Book Store

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
	String docName = @"d:\B08.xlsx";

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
				branch1.BranchName = GetArabicnumber(branch1.BranchName);
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
					branch2.BranchName = GetArabicnumber(branch2.BranchName);
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
				branch1.AddressTextMonatery = GetArabicnumber(AddressText_Monatery);

			}

			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
			{
				AddressText_Monatery = cell2.Trim();
				AddressText_Monatery = AddressText_Monatery.Replace("\n", "");
				branch2.AddressTextMonatery = GetArabicnumber(AddressText_Monatery);

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
				branch1.MonasteryPhoneNo = GetArabicnumber(MonasteryPhoneNO);
			}
			if (!string.IsNullOrEmpty(cell2) && branch2 != null)
			{
				MonasteryPhoneNO = cell2.Replace("-", "");
				MonasteryPhoneNO = MonasteryPhoneNO.Trim();
				branch2.MonasteryPhoneNo = GetArabicnumber(MonasteryPhoneNO);

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
												+ GetArabicnumber(MonasteryPhoneNO);
				}
				if (!string.IsNullOrEmpty(cell2) && branch2 != null)
				{
					MonasteryPhoneNO = cell2.Replace("-", "");
					MonasteryPhoneNO = MonasteryPhoneNO.Trim();
					branch2.MonasteryPhoneNo = branch2.MonasteryPhoneNo + ","
												+ GetArabicnumber(MonasteryPhoneNO);
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

			branch1.Notation = GetArabicnumber(branch1.Notation);
			branch2.Notation = GetArabicnumber(branch2.Notation);
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
static string GetArabicnumber(string source)
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
