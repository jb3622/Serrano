using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Disney.iDash.DataLayer;
using DevExpress.XtraPrinting;
using Disney.iDash.Shared;
using System.Diagnostics;

namespace Disney.iDash.Framework.Forms
{
	public partial class SQLQuery : DevExpress.XtraEditors.XtraForm
	{

		public SQLQuery()
		{
			InitializeComponent();
		}

		private void mnuCopy_Click(object sender, EventArgs e)
		{
			ResultsView.CopyToClipboard();
			MessageBox.Show("Selection copied to the clipboard", "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void mnuExport_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = "xlsx",
				AddExtension = true,
				AutoUpgradeEnabled = true,
				CheckPathExists = true,
				FileName = "SQLResults",
				Filter = "Microsoft Excel Files (*.xlsx)|*.xlsx",
				FilterIndex = 0,
				OverwritePrompt = true,
				Title = "Export to Excel"
			};

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Application.DoEvents();
                var utils = new DevExpressUtils();
                utils.ExportToExcel(dialog.FileName, ResultsView);
            }
		}

		private void mnuClearResults_Click(object sender, EventArgs e)
		{
			ClearMessages();
			HideGrid();
		}

		private void mnuExecuteSQL_Click(object sender, EventArgs e)
		{
			if (memoSQL.EditValue != null && memoSQL.EditValue.ToString() != string.Empty)
			{
				var factory = new DB2Factory();

				if (factory.OpenConnection())
				{
					try
					{
						HideGrid();

						UpdateMessages("Executing SQL...");
						this.Cursor = Cursors.WaitCursor;
						Application.DoEvents();

						var sw = new Stopwatch();
						var cmd = factory.CreateCommand(memoSQL.EditValue.ToString());
						sw.Start();
						var reader = cmd.ExecuteReader();

						if (reader != null)
						{
							var table = new DataTable();
							table.Load(reader);
							ShowGrid(table);						
							reader.Close();
							sw.Stop();
							UpdateMessages(string.Format("Execution Complete - {0:#,##} row(s) returned in {1:0.0} seconds",  table.Rows.Count, sw.Elapsed.TotalSeconds));
						}
						else
							UpdateMessages("Execution Complete - no rows returned");

						tabResults.SelectedTabPage = pageResults;
					}
					catch (Exception ex)
					{
						UpdateMessages("Execution failed\r\n\r\n" + ex.Message);
					}
					finally
					{
						factory.CloseConnection();
					}

					this.Cursor = Cursors.Default;
				}
			}
			else
				UpdateMessages("No SQL statement entered");
		}

		private void UpdateMessages(string message)
		{
			memoMessages.EditValue = DateTime.Now.ToString("hh:mm:ss - ") + message;
			tabResults.SelectedTabPage = pageMessages;
		}

		private void ClearMessages()
		{
			memoMessages.EditValue = null;
		}

		private void ShowGrid(DataTable table)
		{
			ResultsView.Columns.Clear();
			ResultsGrid.DataSource = table;
			ResultsView.BestFitMaxRowCount = 20;
			ResultsView.BestFitColumns();
			pageResults.PageVisible = true;
			tabResults.SelectedTabPage = pageResults;
		}

		private void HideGrid()
		{
			ResultsView.Columns.Clear();
			ResultsGrid.DataSource = null;
			pageResults.PageVisible = false;
			tabResults.SelectedTabPage = pageMessages;
		}
	}
}