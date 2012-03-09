using System;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;

namespace Disney.iDash.Shared
{
    public class DevExpressUtils
    {
        // wrapper for devexpress view.ExportToXlsx
        public void ExportToExcel(string masterfilename, params GridView[] views)
        {
            var askToView = false;

            try
            {
                var options = new XlsxExportOptions(TextExportMode.Value, false);
                if (views.Length > 1)
                {
                    var filenames = new List<string>();
                    foreach (GridView view in views)
                    {
                        var filename = System.IO.Path.GetDirectoryName(masterfilename) + "\\" + System.IO.Path.GetFileNameWithoutExtension(masterfilename) + "_" + view.Name.ToString().Replace("view", "") + System.IO.Path.GetExtension(masterfilename);

                        if (System.IO.File.Exists(filename))
                            System.IO.File.Delete(filename);

                        filenames.Add(filename);
                        options.ShowGridLines = true;
                        options.SheetName = view.Name.ToString().Replace("view", "");
                        view.ExportToXlsx(filename, options);
                    }

                    var excel = new ExcelUtils();

                    if (excel.CombineWorkbooks(masterfilename, filenames))
                        askToView=true;
                    else
                        ErrorDialog.Show(excel.LastException, "CombineWorkbooks", false);
                }
                else
                {
                    views[0].ExportToXlsx(masterfilename, options);
                    askToView = true;
                }

                if (askToView && Question.YesNo(masterfilename + "\n\nDo you want to view it?", "Export Complete"))
                    Process.Start(masterfilename);
            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "ExportToExcel", false);
            }
        }
    }
}
