﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace WNPP_Tools
{
	class WriteDoc
	{
		// To search and replace content in a document part.
		public static void SearchAndReplace(string document)
		{
			using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(document, true))
			{
				string docText = null;
				using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
				{
					docText = sr.ReadToEnd();
				}

				Regex regexText = new Regex("Hello world!");
				docText = regexText.Replace(docText, "Hi Everyone!");

				using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
				{
					sw.Write(docText);
				}
			}
		}

	}
}
