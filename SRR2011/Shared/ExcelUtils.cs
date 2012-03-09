using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel=Microsoft.Office.Interop.Excel;

namespace Disney.iDash.Shared
{
	public class ExcelUtils
	{
		public Exception LastException { get; private set; }

		public bool CombineWorkbooks(string masterFile, List<string> filenames)
		{
			var result = false;
			try
			{
				if (System.IO.File.Exists(masterFile))
					System.IO.File.Delete(masterFile);

                var app = new Excel.Application();

                app.DefaultSaveFormat = Excel.XlFileFormat.xlExcel12;
                app.SheetsInNewWorkbook = 1;

                var masterWorkbook = app.Workbooks.Add();
                
				foreach (var filename in filenames)
				{
					var workbook = app.Workbooks.Open(filename);
					workbook.Sheets.Copy(Type.Missing, masterWorkbook.Sheets[masterWorkbook.Sheets.Count]);
					workbook.Close(false);
					System.IO.File.Delete(filename);
				}
               
                foreach (var sheetName in new[] { "Sheet1", "Sheet2", "Sheet3" })
                    try
                    {
                        if (masterWorkbook.Sheets[sheetName] != null)
                            masterWorkbook.Sheets[sheetName].Delete();
                    }
                    catch
                    {
                        // ignore any error.
                    }

				masterWorkbook.Close(true, masterFile);

				app.Quit();

				result = true;
			}
			catch (Exception ex)
			{
				LastException = ex;
			}
			return result;
		}
	}
}
