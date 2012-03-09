/*
 * Author:  Lee Ellis
 * Project: SRR
 * Date:    From May 2011
 * 
 * Description
 * Handles SummaryWorkbench UI
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using Disney.iDash.LocalData;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using System.Diagnostics;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class SummaryWorkbenchForm : Disney.iDash.SRR.UI.Forms.Common.BaseParameters
    {
        private SummaryWorkbenchInfo _summaryWorkbenchInfo = new SummaryWorkbenchInfo();

        private CellHighlighter _highlights = new CellHighlighter();
        private bool _editMode = false;
		private bool _clearSortFilterGrouping = true;
		private bool _allowExit = true;

        private Color _colourOverridesBackGround1 = Color.Yellow;
        private Color _colourOverridesBackGround2 = Color.Yellow;
        private Color _colourInheritedForeground = Color.Blue;
        private Color _colourActualForeground = Color.Black;
        private Color _colourDefaultMarketBackGround1 = Color.LightGray;

        private BackgroundWorker _bwSearch = null;

        // private bool _HasClickedWeekly = false;
        // private bool _HasClickedDaily = false;

        #region Public methods and functions
        //-----------------------------------------------------------------------------------------
        public SummaryWorkbenchForm()
        {
            InitializeComponent();
//			DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true; 
			
			riPatternId.DataSource = Instance.GetPatterns;

            this.MdiParent = FormUtils.FindMdiParent();
            tabControl.Visible = false;
            EditMode = false;
        }

        public override void ShowSummaryForm(SummaryWorkbenchInfo summaryWorkbenchInfo)
        {
            _summaryWorkbenchInfo = summaryWorkbenchInfo;

            _summaryWorkbenchInfo.ProgressEvent += ((message, percentageComplete) =>
				{
					base.UpdateProgress(message, percentageComplete);
				});

			_summaryWorkbenchInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication)=>
				{
					ErrorDialog.Show(ex, extraInfo, terminateApplication);
				});

			_summaryWorkbenchInfo.ExceptionHandler.AlertEvent +=((message, caption, alertType)=>
				{
					ErrorDialog.ShowAlert(message, caption, alertType);
				});

            _summaryWorkbenchInfo.SelectedSummaryLevel = 16;
            DepartmentSelector.RefreshControls();
            this.Show();
        }

        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------

        /// <summary>
        /// Performs cleanup on files before exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves changes back to AS400
        /// Calls program to apply changes to the lever files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {            
			if (Question.YesNo("Are you sure you want to apply changes to these levers?", "APPLY CHANGES"))
			{
                viewClass.CloseEditor();
                viewClass.UpdateCurrentRow();
                viewClassGrade.CloseEditor();
                viewClassGrade.UpdateCurrentRow();
                viewClassMarket.CloseEditor();
                viewClassMarket.UpdateCurrentRow();
                viewClassStore.CloseEditor();
                viewClassStore.UpdateCurrentRow();
                viewDept.CloseEditor();
                viewDept.UpdateCurrentRow();
                viewDeptGrade.CloseEditor();
                viewDeptGrade.UpdateCurrentRow();
                viewDeptMarket.CloseEditor();
                viewDeptMarket.UpdateCurrentRow();
                viewDeptStore.CloseEditor();
                viewDeptStore.UpdateCurrentRow();

				var buttonState = new ControlStateCollection();
				buttonState.SaveState(btnEdit, btnCancel, btnApply, btnModelRun, btnExit);
				buttonState.Disable();
				_allowExit = false;

				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();

                if (_summaryWorkbenchInfo.IsModelRunAvailable())
                {
                    var timeStart = System.DateTime.Now;
                    this.Cursor = Cursors.WaitCursor;
                    base.UpdateStatusMessage("Applying changes...");

                    if (_summaryWorkbenchInfo.SaveChanges() && _summaryWorkbenchInfo.ApplyChanges())
                    {

                        tabControl.Visible = false;
                        ResetDepartmentSelector();
                        EditMode = false;

                        ResetForm();
                        UpdateStatusMessage("Lever changes have been applied");
                    }
                    else
                        UpdateStatusMessage("Failed to apply lever changes");
                }
                else
                {
                    MessageBox.Show("Unable to process your request at this time. The system is still preparing files. Please try again in a few minutes",
                         "Apply", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                buttonState.RestoreState();
                this.Cursor = Cursors.Default;
				_allowExit = true;
			}
        }

        private void ResetDepartmentSelector()
        {
            DepartmentSelector.DepartmentId = -1;
            DepartmentSelector.Workbench = Constants.Workbenches.Daily;
			DepartmentSelector.StoreType = Constants.StoreTypes.BricksAndMortar;
			DepartmentSelector.Enabled = true;
            SetFileGroup();
        }

        /// <summary>
        /// Saves changes back to AS400
        /// Calls program to perform a model run
        /// Refreshes data and grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModelRun_Click(object sender, EventArgs e)
        {
			if (Question.YesNo("Commence the Model Run?", "MODEL RUN"))
			{
                viewClass.CloseEditor();
                viewClass.UpdateCurrentRow();
                viewClassGrade.CloseEditor();
                viewClassGrade.UpdateCurrentRow();
                viewClassMarket.CloseEditor();
                viewClassMarket.UpdateCurrentRow();
                viewClassStore.CloseEditor();
                viewClassStore.UpdateCurrentRow();
                viewDept.CloseEditor();
                viewDept.UpdateCurrentRow();
                viewDeptGrade.CloseEditor();
                viewDeptGrade.UpdateCurrentRow();
                viewDeptMarket.CloseEditor();
                viewDeptMarket.UpdateCurrentRow();
                viewDeptStore.CloseEditor();
                viewDeptStore.UpdateCurrentRow();

				var buttonState = new ControlStateCollection();
				buttonState.SaveState(btnEdit, btnCancel, btnApply, btnModelRun, btnExit);
				buttonState.Disable();
				_allowExit = false;

				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();

				if (_summaryWorkbenchInfo.IsModelRunAvailable())
				{
					var timeStart = System.DateTime.Now;
					this.Cursor = Cursors.WaitCursor;
					base.UpdateStatusMessage("Running Model...");

                    if (_summaryWorkbenchInfo.SaveChanges() && _summaryWorkbenchInfo.RunModel())
                    {
                        gridDept.Visible = false;
                        gridDept.SuspendLayout();

                        _summaryWorkbenchInfo.AliasDrop();
                        _summaryWorkbenchInfo.InitialDataLoad();
                        _summaryWorkbenchInfo.InitialiseScreen();

                        ClearAllGrids();

                        BuildAllGrids();
                        BuildAllViews();

                        tabControl.Visible = true;
                        gridDept.Visible = true;
                        SetupMenu();
                        gridDept.ResumeLayout();

                        RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);

                        UpdateStatusMessage(string.Format("Model run time {0:0.0}secs", System.DateTime.Now.Subtract(timeStart).TotalSeconds));
                    }
                    else
                        UpdateStatusMessage("Failed to complete model run");
				}
				else
					MessageBox.Show("Unable to process your request at this time. The system is still preparing files. Please try again in a few minutes",
						"Model Run", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

				this.Cursor = Cursors.Default;
				buttonState.RestoreState();
				_allowExit = true;
			}
        }

        /// <summary>
        /// Switches screen into edit mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode = true;

            //_HasClickedWeekly = false;
            //_HasClickedDaily = false;

            RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);
        }

        /// <summary>
        /// Performs validation on data entered in views
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;

            view.ClearColumnErrors();

            // Dept Level has different validation to the rest of the Levels - Values cannot be deleted
            if (view == viewDept)
            {
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                && e.Value.ToString() != "Y"
                && e.Value.ToString() != "N")
                {
                    e.Valid = false;
                    view.SetColumnError(view.FocusedColumn, "Only Y or N permitted");
                }

                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff)
                 || view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)
                 || view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)
                 || view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor))
                {
                    decimal result;

                    if (e.Value == null
                    || !Decimal.TryParse(e.Value.ToString(), out result))
                    {
                        e.Valid = false;
                        view.SetColumnError(view.FocusedColumn, "The Levers at Department Level cannot be deleted");
                    }
                }
            }
            else
            // All other views
            {
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                && e.Value.ToString() != "Y"
                && e.Value.ToString() != ""
                && e.Value.ToString() != " "
                && e.Value.ToString() != "N")
                {
                    e.Valid = false;
                    view.SetColumnError(view.FocusedColumn, "Only Y, N or blank permitted");
                }

            }

            if (e.Value != null && e.Value.ToString().Trim().Length > 0)
            {
                // Validate uplift
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor))
                {
                    var selectedUpliftFactor = Convert.ToDouble(e.Value);

                    if (selectedUpliftFactor < 0.10
                     || selectedUpliftFactor > 9.99)
                    {
                        e.Valid = false;
                        e.ErrorText = "Uplift factor must be between 0.1 and 9.99";
                        //view.SetColumnError(view.FocusedColumn, "Uplift factor must be between 0.1 and 9.99");
                    }

                }

                // Validate Cover CutOff
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff))
                {
                    var selectedCutoff = Convert.ToDouble(e.Value);

                    if (selectedCutoff < 0
                        || selectedCutoff > 99)
                    {
                        e.Valid = false;
                        e.ErrorText = "Cover cutoff must be between 0 and 99";
                        //view.SetColumnError(view.FocusedColumn, "Cover cutoff must be between 0 and 99");
                    }
                }

                // Validate Smoothing factor
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor))
                {
                    var selectedSmoothFactor = Convert.ToDouble(e.Value);

                    if (selectedSmoothFactor < 0
                        || selectedSmoothFactor > 1)
                    {
                        e.Valid = false;
                        e.ErrorText = "Smoothing factor must be between 0 and 1";
                        // view.SetColumnError(view.FocusedColumn, "Smoothing factor must be between 0 and 1");
                    }
                }

                // Validate selected pattern based on store type
                if (view.FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern))
                {
                    // get store type
                    var storeType = view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType)).ToString();

                    var selectedPattern = Convert.ToInt32(e.Value);

                    // Online can only select patterns between 001 and 099
					if (storeType == Constants.kOnline && selectedPattern > 99)
                    {
                        e.Valid = false;
                        view.SetColumnError(view.FocusedColumn, "B&M patterns (100-999) are not permitted for online stores");
                    }

                    // B&M can only select patterns between 100 and 999
					if (storeType == Constants.kBricksAndMortar && selectedPattern < 100)
                    {
                        e.Valid = false;
                        view.SetColumnError(view.FocusedColumn, "Online patterns (001-099) are not permitted for B&M stores");
                    }

                }
            }



        }

        /// <summary>
        /// Overrides default grid validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_InvalidValueException(object sender, DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs e)
        {
            // Do not perform any default validation display action (displaying messagebox)
           // e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        /// <summary>
        /// Checks if file locking is required
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_Clicked(object sender, EventArgs e)
        {
            //if (EditMode)
            //{
            //    CheckFileLocks(sender);
            //}
        }

        /// <summary>
        /// Performs file locking checks
        /// </summary>
        /// <param name="sender"></param>
        private void CheckFileLocks(GridView view)
        {
            if (view.FocusedRowHandle >= 0)
            {
                string workbench = view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench)).ToString();
				if (workbench == Constants.kDaily && !_summaryWorkbenchInfo.IsDailyLocked)
                {
					if (_summaryWorkbenchInfo.LockFiles(Constants.kDaily))
                    {
                        base.UpdateStatusMessage("Daily records are locked for editing.");
                        _summaryWorkbenchInfo.IsDirty = true;
                        _summaryWorkbenchInfo.ApplyChangesAllowed = true;
                        btnApply.Enabled = true;
                        btnModelRun.Enabled = true;
                    }
                    else
                    {
                        base.UpdateStatusMessage(_summaryWorkbenchInfo.ErrorMessage, true);

                        // Commented out because the user could still be modifying the other workbench type
                        // _summaryWorkbenchInfo.ApplyChangesAllowed = false;
                        // btnApply.Enabled = false; 
                        // btnModelRun.Enabled = false;
                    }
                }
				if (workbench == Constants.kWeekly && !_summaryWorkbenchInfo.IsWeeklyLocked)
                {
					if (_summaryWorkbenchInfo.LockFiles(Constants.kWeekly))
                    {
                        base.UpdateStatusMessage("Weekly records are locked for editing.");
                        _summaryWorkbenchInfo.IsDirty = true;
                        _summaryWorkbenchInfo.ApplyChangesAllowed = true;
                        btnApply.Enabled = true;
                        btnModelRun.Enabled = true;
                    }
                    else
                    {
                        base.UpdateStatusMessage(_summaryWorkbenchInfo.ErrorMessage, true);

                        // Commented out because the user could still be modifying the other workbench type
                        // _summaryWorkbenchInfo.ApplyChangesAllowed = false;
                        // btnApply.Enabled = false;
                        // btnModelRun.Enabled = false;
                    }
                }
                SetFileGroup();
            }
        }

        /// <summary>
        /// Applies changes to other grids/levels based on changes made at the level being navigated away from
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            // Propogate the changed values to other levels and refresh the grid
            _summaryWorkbenchInfo.PropagateChanges(_summaryWorkbenchInfo.SelectedSummaryLevel);

            _summaryWorkbenchInfo.SelectedSummaryLevel = Convert.ToInt32(tabControl.SelectedTabPage.Tag);

            RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);
        }

        /// <summary>
        /// Returns screen to browse mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ConfirmCancel();
        }

        /// <summary>
        /// Handles user confirmation before cancelling
        /// </summary>
        private void ConfirmCancel()
        {
            if (_summaryWorkbenchInfo.IsDirty && _summaryWorkbenchInfo.ApplyChangesAllowed)
                switch (Question.YesNoCancel("Changes have been made to these items, save them before cancelling?", this.Text))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        var timeStart = System.DateTime.Now;
                        this.Cursor = Cursors.WaitCursor;
                        base.UpdateStatusMessage("Applying changes...");

                        if (_summaryWorkbenchInfo.SaveChanges() && _summaryWorkbenchInfo.ApplyChanges())
                        {
                            EditMode = false;
                            UpdateStatusMessage("Lever changes have been applied");
                        }
                        else
                            UpdateStatusMessage("Failed to apply lever changes");
                        this.Cursor = Cursors.Default;
                        break;

                    case System.Windows.Forms.DialogResult.No:
						_summaryWorkbenchInfo.ReleaseLocks();
                        ResetForm();
                        break;

                    case System.Windows.Forms.DialogResult.Cancel:
                        break;
                }

            else
                ResetForm();
        }

        /// <summary>
        /// Handles returning screen to origninal state after cancel is requested.
        /// </summary>
        private void ResetForm()
        {
            ResetDepartmentSelector();
            EditMode = false;

            _summaryWorkbenchInfo.RebuildWorkingSummaryItems();
            ClearAllGrids();
            BuildAllGrids();
            BuildAllViews();

            GetCurrentView().ClearColumnErrors();

            RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);

            ((DevExpress.XtraGrid.Views.Grid.GridView)GetCurrentGrid().Views[0]).RefreshData();

			btnSearchView.Enabled = true;
			btnSearchEdit.Enabled = true;
			btnEdit.Enabled = false;
			btnCancel.Enabled = false;
			tabControl.Visible = false;

		}

        /// <summary>
        /// Handles putting screen into Has changes mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
            //{
            //    GridView view = sender as GridView;
            //    CheckFileLocks(view);

            //    ////_summaryWorkbenchInfo.IsDirty = true;
            //    //btnApply.Enabled = _summaryWorkbenchInfo.IsDirty;
            //    //btnModelRun.Enabled = _summaryWorkbenchInfo.IsDirty;
            //}
        }

        private void riCommon_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }
        
        private void view_ShowingEditor(object sender, CancelEventArgs e)
        {
            List<string> EditableColumns = new List<string>
            { 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor),
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
            };

            GridView view = sender as GridView;
            if (this.Visible)
            {
                var rowWorkbench = (view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench)) ?? string.Empty).ToString();
                //var storeType = (view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType)) ?? string.Empty).ToString();

                if (EditableColumns.Contains(view.FocusedColumn.FieldName))
                {
                    CheckFileLocks(view);

					if (rowWorkbench == Constants.kDaily)
                        e.Cancel = !_summaryWorkbenchInfo.IsDailyLocked;
					else if (rowWorkbench == Constants.kWeekly)
                        e.Cancel = !_summaryWorkbenchInfo.IsWeeklyLocked;

                    if (e.Cancel)
                        base.UpdateStatusMessage(_summaryWorkbenchInfo.ErrorMessage, true);
                    else
                        base.ClearStatusMessage();
                }
            }

        }

        /// <summary>
        /// Handles changes to dependent properties based on UI value changes
        /// e.g. 
        /// When a user changes a Smoothing Factor
        /// the lever will be marked as changed or deleted 
        /// and the record will be marked as changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

            List<string> EditableColumns = new List<string>
            { 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern), 
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor),
                SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
            };

            // only process changes from the UI
            if (EditableColumns.Contains(e.Column.FieldName))
            {
                _summaryWorkbenchInfo.IsDirty = true;

                if (this.Visible)
                {
					_highlights.CellValueChanged(view, e);
                }
                var summaryItem = (SummaryItem)view.GetFocusedRow();

                summaryItem.Changed = "1";

                if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation))
                {
                    SetAllocationFlag(summaryItem);
                }
                if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff))
                {
                    SetCutOff(summaryItem);
                }
                if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor))
                {
                    SetUpliftFactor(summaryItem);
                }
                if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor))
                {
                    SetSmoothFactor(summaryItem);
                }
                if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern))
                {
                    SetPattern(summaryItem);
                }

                // set record changed field
                var cellValue = view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Changed));

                view.SetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Changed), "1");
                cellValue = view.GetFocusedRowCellValue(SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Changed));

                if (summaryItem.StoreType.StartsWith("B"))
                    _summaryWorkbenchInfo.IsBricksChanged = true;
                if (summaryItem.StoreType.StartsWith("O"))
                    _summaryWorkbenchInfo.IsClicksChanged = true;
            }
        }

        /// <summary>
        /// Starts a search in browse mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSearchView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Search();
        }

        /// <summary>
        /// Starts a search in edit mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSearchEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Search(true);
        }
        
        private void btnSearchView_Click(object sender, EventArgs e)
        {
            if (DepartmentSelector.DepartmentText != "")
                Search();
        }

        private void btnSearchEdit_Click(object sender, EventArgs e)
        {
            if (DepartmentSelector.DepartmentText != "")
                Search(true);
        }

        /// <summary>
        /// Handles rows background colour based on the StoreType and WorkbenchType
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
            {
                var storeType = GetCurrentView().GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType));
                var workBench = GetCurrentView().GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench));

                var market = GetCurrentView().GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Market));
                var classCell = GetCurrentView().GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Class));

                if (storeType != null && workBench != null)
                    switch (storeType.ToString())
                    {
						case Constants.kBricksAndMortar:
                            switch (workBench.ToString())
                            {
								case Constants.kDaily:
                                    e.Appearance.BackColor = Color.Bisque;
                                    break;
								case Constants.kWeekly:
                                    e.Appearance.BackColor = Color.PaleTurquoise;
                                    break;
                                default:
                                    e.Appearance.BackColor = Color.White;
                                    break;
                            }
                            break;

						case Constants.kOnline:
                            switch (workBench.ToString())
                            {
								case Constants.kDaily:
                                    e.Appearance.BackColor = Color.Thistle;
                                    break;
								case Constants.kWeekly:
                                    e.Appearance.BackColor = Color.LightSalmon;
                                    break;
                                default:
                                    e.Appearance.BackColor = Color.White;
                                    break;
                            }
                            break;

                        default:
                            e.Appearance.BackColor = Color.White;
                            break;
                    }
            }
        }

        /// <summary>
        /// Handles cells formatting colour based on the following:
        ///  1. Background highlighted yellow if lever has overrides at lower levels
        ///  2. Font is blue and bold if the value is inherited from a higher level
        ///  3. Font is black if it is displaying a lever value for this level
        ///  4. Font is red and Bold if the value was changed in this session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void view_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
            {
                var view = (GridView)sender;
                if (view.GetRow(e.RowHandle) != null)
                {
                    var defaultColor = Color.Black;
                    var changedColor = Color.Red;

                    bool SetBold = false;

                    // Highlight Columns with Background Colour of Market
                    if (_summaryWorkbenchInfo.SelectedSummaryLevel != 12)
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Class)
                            || e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Market)
                            || e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Grade)
                            || e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.GradeDescription)
                            || e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Store)
                            || e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreName))
                        {
                            e.Appearance.BackColor = GetMarketBackColour(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketColour)));
                            e.Appearance.BackColor2 = e.Appearance.BackColor;
                        }

                    // Highlight Overrides at lower level with a yellow background
                    if (Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.HierarchyKey))) > 9)
                    {
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)
                            && Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternOverrideLevel))) > 0)
                        {
                            HighlightHasOverrides(e);
                        }
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                             && Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationOverrideLevel))) > 0)
                        {
                            HighlightHasOverrides(e);
                        }
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff)
                             && Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffOverrideLevel))) > 0)
                        {
                            HighlightHasOverrides(e);
                        }
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
                             && Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftOverrideLevel))) > 0)
                        {
                            HighlightHasOverrides(e);
                        }
                        if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)
                             && Convert.ToInt32(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorOverrideLevel))) > 0)
                        {
                            HighlightHasOverrides(e);
                        }
                    }

                    // If value is Inherited change Foreground Colour and Style
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)
                    && (bool)view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternActualFlag)) == false)
                    {
                        defaultColor = _colourInheritedForeground;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                         && (bool)view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationActualFlag)) == false)
                    {
                        defaultColor = _colourInheritedForeground;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff)
                         && (bool)view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffActualFlag)) == false)
                    {
                        defaultColor = _colourInheritedForeground;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
                         && (bool)view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftActualFlag)) == false)
                    {
                        defaultColor = _colourInheritedForeground;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)
                         && (bool)view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorActualFlag)) == false)
                    {
                        defaultColor = _colourInheritedForeground;
                        SetBold = true;
                    }

                    // If value is changed in this session change Foreground Colour to Red
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)
                        && "CAD".Contains(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternStatus)).ToString()))
                    {
                        defaultColor = changedColor;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                         && "CAD".Contains(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationStatus)).ToString()))
                    {
                        defaultColor = changedColor;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff)
                         && "CAD".Contains(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffStatus)).ToString()))
                    {
                        defaultColor = changedColor;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
                         && "CAD".Contains(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftStatus)).ToString()))
                    {
                        defaultColor = changedColor;
                        SetBold = true;
                    }
                    if (e.Column.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)
                         && "CAD".Contains(view.GetRowCellValue(e.RowHandle, SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorStatus)).ToString()))
                    {
                        defaultColor = changedColor;
                        SetBold = true;
                    }

                    _highlights.SetRowCellStyle(view, e, defaultColor, Color.Red);

                    if (SetBold)
                        e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        /// <summary>
        /// Sets override highlighting
        /// </summary>
        /// <param name="e"></param>
        private void HighlightHasOverrides(RowCellStyleEventArgs e)
        {
            e.Appearance.BackColor = _colourOverridesBackGround1;
            e.Appearance.BackColor2 = _colourOverridesBackGround2;
        }

        /// <summary>
        /// Handles item copying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCopy_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.GridControl activeGrid = GetCurrentGrid();

            _summaryWorkbenchInfo.CopiedSummaryItem = (SummaryItem)((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).GetFocusedRow();

            SetupMenu();

            base.UpdateStatusMessage("Item copied to memory.");
        }

        /// <summary>
        /// Handles pasting of copied item to other rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPaste_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.GridControl activeGrid = GetCurrentGrid();

            // Get clicked column
            var clickedColumn = new SummaryWorkbenchInfo.SummaryItemColumns();
            if (((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation))
                clickedColumn = SummaryWorkbenchInfo.SummaryItemColumns.Allocation;
            if (((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff))
                clickedColumn = SummaryWorkbenchInfo.SummaryItemColumns.CutOff;
            if (((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern))
                clickedColumn = SummaryWorkbenchInfo.SummaryItemColumns.Pattern;
            if (((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor))
                clickedColumn = SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor;
            if (((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).FocusedColumn.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor))
                clickedColumn = SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor;

            SummaryItem summaryItem = new SummaryItem();

            // apply copied data to clicked column
            foreach (int index in ((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).GetSelectedRows())
            {
                summaryItem = (SummaryItem)((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).GetRow(index);

                if (clickedColumn == SummaryWorkbenchInfo.SummaryItemColumns.Allocation)
                {
                    summaryItem.Allocation = _summaryWorkbenchInfo.CopiedSummaryItem.Allocation;
                    if (summaryItem.AllocationActualFlag)
                        summaryItem.AllocationStatus = "C";
                    else
                        summaryItem.AllocationStatus = "A";
                }

                if (clickedColumn == SummaryWorkbenchInfo.SummaryItemColumns.CutOff)
                {
                    summaryItem.CutOff = _summaryWorkbenchInfo.CopiedSummaryItem.CutOff;
                    if (summaryItem.CutOffActualFlag)
                        summaryItem.CutOffStatus = "C";
                    else
                        summaryItem.CutOffStatus = "A";
                }

                if (clickedColumn == SummaryWorkbenchInfo.SummaryItemColumns.Pattern)
                {
                    summaryItem.Pattern = _summaryWorkbenchInfo.CopiedSummaryItem.Pattern;
                    if (summaryItem.PatternActualFlag)
                        summaryItem.PatternStatus = "C";
                    else
                        summaryItem.PatternStatus = "A";
                }

                if (clickedColumn == SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor)
                {
                    summaryItem.SmoothFactor = _summaryWorkbenchInfo.CopiedSummaryItem.SmoothFactor;
                    if (summaryItem.SmoothFactorActualFlag)
                        summaryItem.SmoothFactorStatus = "C";
                    else
                        summaryItem.SmoothFactorStatus = "A";
                }

                if (clickedColumn == SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor)
                {
                    summaryItem.UpliftFactor = _summaryWorkbenchInfo.CopiedSummaryItem.UpliftFactor;
                    if (summaryItem.UpliftActualFlag)
                        summaryItem.UpliftStatus = "C";
                    else
                        summaryItem.UpliftStatus = "A";
                }
                summaryItem.Changed = "1";

                // Make the changes show in the grid
                ((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).RefreshRow(index);
            }

            base.UpdateStatusMessage(string.Format("Item pasted to {0} records.",
                ((DevExpress.XtraGrid.Views.Grid.GridView)activeGrid.Views[0]).GetSelectedRows().Count()));
        }

        private void mnuGridOptions_Opening(object sender, CancelEventArgs e)
        {
            mnuRemoveOverridesThisLevel.Enabled = _summaryWorkbenchInfo.SelectedSummaryLevel < 16 && EditMode;
        }

        /// <summary>
        /// Removes allocation flag from all lower levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAllocationFlag_Click(object sender, EventArgs e)
        {
			DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever.Allocation, "A");
        }

        /// <summary>
        /// Removes cutoff from all lower levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuCutOffFactor_Click(object sender, EventArgs e)
        {
			DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever.CutOff, "C");
        }

        /// <summary>
        /// Removes pattern lever from all lower levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPattern_Click(object sender, EventArgs e)
        {
			DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever.Pattern, "P");
        }

        /// <summary>
        /// Removes smoothing factor from all lower levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSmoothingFactor_Click(object sender, EventArgs e)
        {
			DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever.SmoothingFactor, "S");
        }

        /// <summary>
        /// Removes uplift lever from all lower levels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUpliftFactor_Click(object sender, EventArgs e)
        {
			DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever.UpliftFactor, "U");
        }

        /// <summary>
        /// Handles removing all levers from the current level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRemoveOverridesThisLevel_Click(object sender, EventArgs e)
        {
            if (Question.YesNo("Are you sure you want to submit the job to remove the Overrides at this Level?", "Confirm"))
                RemoveOverridesFromLevel();
        }

        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------

        private bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                if (_editMode)
                {
                    btnEdit.Enabled = false;
                    btnCancel.Enabled = true;
                    _summaryWorkbenchInfo.ApplyChangesAllowed = _summaryWorkbenchInfo.IsDirty;
                    btnApply.Enabled = _summaryWorkbenchInfo.IsDirty;
                    btnModelRun.Enabled = _summaryWorkbenchInfo.IsDirty;
                }
                else
                {
                    btnEdit.Enabled = true;
                    btnCancel.Enabled = false;
                    _summaryWorkbenchInfo.ApplyChangesAllowed = false;
                    btnApply.Enabled = false;
                    btnModelRun.Enabled = false;

                    _summaryWorkbenchInfo.IsBricksChanged = false;
                    _summaryWorkbenchInfo.IsClicksChanged = false;

                    // Only release the locks if no changes were made in this session
                    // Otherwise we could be releasing a lock that an apply job on the AS400 is stll processing
                    // the AS400 apply job will release the locks as a final step before it completes.
                    if (_summaryWorkbenchInfo.IsDailyLocked || _summaryWorkbenchInfo.IsWeeklyLocked)
                        if (_summaryWorkbenchInfo.MustReleaseLocks)
                        {
                            _summaryWorkbenchInfo.ReleaseLocks();
                            SetFileGroup();
                        }

                    _summaryWorkbenchInfo.IsDirty = false;				
                }

				//btnSearchView.Enabled = !_editMode;
				//btnSearchEdit.Enabled = !_editMode;
				//DepartmentSelector.Enabled = !_editMode;

                //mnuApplyToAllLevels.Enabled = _editMode;
                //mnuRemoveOverridesThisLevel.Enabled = _editMode;
                mnuApplyToAllLevels.Enabled = false;
                mnuRemoveOverridesThisLevel.Enabled = false;
            }
        }

        /// <summary>
        /// Handles pre-search conditions and performs search
        /// </summary>
        private void Search(bool editMode = false)
        {
            if (_summaryWorkbenchInfo.IsDirty && _summaryWorkbenchInfo.ApplyChangesAllowed)
                switch (Question.YesNoCancel("Changes have been made to these items, save them before searching?", this.Text))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        var timeStart = System.DateTime.Now;
                        this.Cursor = Cursors.WaitCursor;
                        base.UpdateStatusMessage("Applying changes...");

                        if (_summaryWorkbenchInfo.SaveChanges() && _summaryWorkbenchInfo.ApplyChanges())
                        {
                            EditMode = false;
                            _summaryWorkbenchInfo.AliasDrop();
                            UpdateStatusMessage("Lever changes have been applied");
                        }
                        else
                            UpdateStatusMessage("Failed to apply lever changes");

                        this.Cursor = Cursors.Default;
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        if (_summaryWorkbenchInfo.WorkingSummaryItems != null)
                        {
                            _summaryWorkbenchInfo.AliasDrop();
                            _summaryWorkbenchInfo.CleanupAS400FileMembers();
                        }

                        EditMode = editMode;
                        InitialDataLoad();
                        break;

                    case System.Windows.Forms.DialogResult.Cancel:
                        break;
                }
            else
            {
                if (_summaryWorkbenchInfo.WorkingSummaryItems != null)
                {
                    _summaryWorkbenchInfo.AliasDrop();
                    _summaryWorkbenchInfo.CleanupAS400FileMembers();
                }
				_clearSortFilterGrouping = true;
                EditMode = editMode;
                InitialDataLoad();
            }
        }

        /// <summary>
        /// Provides access to the currently visible view
        /// </summary>
        /// <returns></returns>
        private GridView GetCurrentView()
        {
            switch (_summaryWorkbenchInfo.SelectedSummaryLevel)
            {
                case 9:
                    return viewClassStore;
                case 10:
                    return viewClassGrade;
                case 11:
                    return viewClassMarket;
                case 12:
                    return viewClass;
                case 13:
                    return viewDeptStore;
                case 14:
                    return viewDeptGrade;
                case 15:
                    return viewDeptMarket;
                case 16:
                    return viewDept;
                default:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Provides access to the currently displayed grid
        /// </summary>
        /// <returns></returns>
        private DevExpress.XtraGrid.GridControl GetCurrentGrid()
        {
            switch (_summaryWorkbenchInfo.SelectedSummaryLevel)
            {
                case 9:
                    return gridClassStore;
                case 10:
                    return gridClassGrade;
                case 11:
                    return gridClassMarket;
                case 12:
                    return gridClass;
                case 13:
                    return gridDeptStore;
                case 14:
                    return gridDeptGrade;
                case 15:
                    return gridDeptMarket;
                case 16:
                    return gridDept;
                default:
                    break;
            }

            return null;
        }

        /// <summary>
        /// Performs first load of data from AS400 and sets up grids
        /// </summary>
        private void InitialDataLoad()
        {
            var timeStart = System.DateTime.Now;
            this.Cursor = Cursors.WaitCursor;
            base.UpdateStatusMessage("Loading...");
            Application.DoEvents();

            try
            {
                gridDept.Visible = false;
                gridDept.SuspendLayout();

                _highlights.Clear();

				_summaryWorkbenchInfo.Workbench = (Constants.Workbenches)DepartmentSelector.Workbench;
				_summaryWorkbenchInfo.StoreType = (Constants.StoreTypes)DepartmentSelector.StoreType;

                if (_bwSearch == null)
                {
                    _bwSearch = new BackgroundWorker();
                    _bwSearch.DoWork += ((sender, e) =>
                        {
                            _summaryWorkbenchInfo.WorkbenchInitialLoad();
                        });

                    _bwSearch.RunWorkerCompleted += ((sender, e) =>
                        {
                            ClearAllGrids();

                            BuildAllGrids();
                            BuildAllViews();

                            tabControl.Visible = true;
                            gridDept.Visible = true;
                            SetupMenu();
                            gridDept.ResumeLayout();

                            DepartmentSelector.Enabled = false;
                            btnSearchEdit.Enabled = false;
                            btnSearchView.Enabled = false;

                            this.Cursor = Cursors.Default;
                            UpdateStatusMessage(string.Format("Load time {0:0.0}secs", System.DateTime.Now.Subtract(timeStart).TotalSeconds));
                            tabControl.SelectedTabPageIndex = 0; // select Department level
                            _allowExit = true;
                        });
                }

                _allowExit = false;
	    		_bwSearch.RunWorkerAsync();
/*
				_summaryWorkbenchInfo.WorkbenchInitialLoad();

				ClearAllGrids();

				BuildAllGrids();
				BuildAllViews();

				tabControl.Visible = true;
				gridDept.Visible = true;
				SetupMenu();
				gridDept.ResumeLayout();

				DepartmentSelector.Enabled = false;
				btnSearchEdit.Enabled = false;
				btnSearchView.Enabled = false;

				this.Cursor = Cursors.Default;
				UpdateStatusMessage(string.Format("Load time {0:0.0}secs", System.DateTime.Now.Subtract(timeStart).TotalSeconds));
				tabControl.SelectedTabPageIndex = 0; // select Department level
 */
			}
            catch (Exception ex)
            {
                _allowExit = true; 
                this.Cursor = Cursors.Default;
                ErrorDialog.Show(ex, "InitialDataLoad");
            }
        }

        /// <summary>
        /// Clears all grids
        /// </summary>
        private void ClearAllGrids()
        {
            gridDept.DataSource = null;
            gridDeptMarket.DataSource = null;
            gridDeptGrade.DataSource = null;
            gridDeptStore.DataSource = null;
            gridClass.DataSource = null;
            gridClassMarket.DataSource = null;
            gridClassGrade.DataSource = null;
            gridClassStore.DataSource = null;

            btnEdit.Visible = false;
        }

        /// <summary>
        /// Builds all grids
        /// </summary>
        private void BuildAllGrids()
        {
            SetFileGroup();

            BindAllGrids();

            if (!gridDept.Visible)
                base.LoadLayout(viewDept, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Department.ToString(), _clearSortFilterGrouping);
            if (!gridDeptMarket.Visible)
                base.LoadLayout(viewDeptMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentMarket.ToString(), _clearSortFilterGrouping);
            if (!gridDeptGrade.Visible)
                base.LoadLayout(viewDeptGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentGrade.ToString(), _clearSortFilterGrouping);
            if (!gridDeptStore.Visible)
                base.LoadLayout(viewDeptStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentStore.ToString(), _clearSortFilterGrouping);
            if (!gridClass.Visible)
                base.LoadLayout(viewClass, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Class.ToString(), _clearSortFilterGrouping);
            if (!gridClassMarket.Visible)
                base.LoadLayout(viewClassMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassMarket.ToString(), _clearSortFilterGrouping);
            if (!gridClassGrade.Visible)
				base.LoadLayout(viewClassGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassGrade.ToString(), _clearSortFilterGrouping);
            if (!gridClassStore.Visible)
				base.LoadLayout(viewClassStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassStore.ToString(), _clearSortFilterGrouping);

            SetupColumns(viewDept, 16);
            SetupColumns(viewDeptMarket, 15);
            SetupColumns(viewDeptGrade, 14);
            SetupColumns(viewDeptStore, 13);
            SetupColumns(viewClass, 12);
            SetupColumns(viewClassMarket, 11);
            SetupColumns(viewClassGrade, 10);
            SetupColumns(viewClassStore, 9);

            btnEdit.Visible = true;

			_clearSortFilterGrouping = false;
        }

        /// <summary>
        /// Builds all views
        /// </summary>
        private void BuildAllViews()
        {
            SetupView(viewDept);
            SetupView(viewDeptMarket);
            SetupView(viewDeptGrade);
            SetupView(viewDeptStore);
            SetupView(viewClass);
            SetupView(viewClassMarket);
            SetupView(viewClassGrade);
            SetupView(viewClassStore);
        }

        /// <summary>
        /// Binds grids to data for their level
        /// </summary>
        private void BindAllGrids()
        {
            gridDept.DataSource = _summaryWorkbenchInfo.GetLevelData(16);
            gridDeptMarket.DataSource = _summaryWorkbenchInfo.GetLevelData(15);
            gridDeptGrade.DataSource = _summaryWorkbenchInfo.GetLevelData(14);
            gridDeptStore.DataSource = _summaryWorkbenchInfo.GetLevelData(13);
            gridClass.DataSource = _summaryWorkbenchInfo.GetLevelData(12);
            gridClassMarket.DataSource = _summaryWorkbenchInfo.GetLevelData(11);
            gridClassGrade.DataSource = _summaryWorkbenchInfo.GetLevelData(10);
            gridClassStore.DataSource = _summaryWorkbenchInfo.GetLevelData(9);
			btnCancel.Enabled = true;
        }

        /// <summary>
        /// Resets view appearance
        /// </summary>
        /// <param name="view"></param>
        private void SetupView(GridView view)
        {
            view.ClearColumnErrors();
            view.OptionsView.EnableAppearanceEvenRow = false;
            view.OptionsView.EnableAppearanceOddRow = false;
            view.OptionsBehavior.Editable = this.EditMode;
        }

        /// <summary>
        /// Refreshes and redisplays a grid 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="view"></param>
        /// <param name="level"></param>
        private void RefreshGrid(DevExpress.XtraGrid.GridControl grid, GridView view, int level)
        {
            var timeStart = System.DateTime.Now;
            this.Cursor = Cursors.WaitCursor;
            base.UpdateStatusMessage("Loading...");

            view.ClearColumnsFilter();
            view.ClearGrouping();
            view.ClearSorting();

            Application.DoEvents();

            try
            {
                grid.Visible = false;
                grid.SuspendLayout();

                view.ClearColumnErrors();
				view.OptionsView.EnableAppearanceEvenRow = false;
                view.OptionsView.EnableAppearanceOddRow = false;
                view.OptionsBehavior.Editable = this.EditMode;

                _highlights.Clear();

                grid.Visible = true;
                // SetupColumns(view, level);
                SetupMenu();
                grid.Views[0].RefreshData();
                grid.ResumeLayout();

            }
            catch (Exception ex)
            {
                ErrorDialog.Show(ex, "RefreshGrid");
            }
            this.Cursor = Cursors.Default;
            UpdateStatusMessage(string.Format("Load time {0:0.0}secs", System.DateTime.Now.Subtract(timeStart).TotalSeconds));
        }

        private void LoadGridLayout(int level)
        {
            switch (level)
            {
                case 16:
                    base.LoadLayout(viewDept, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Department.ToString());
                    break;
                case 15:
                    base.LoadLayout(viewDeptMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentMarket.ToString());
                    break;
                case 14:
                    base.LoadLayout(viewDeptGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentGrade.ToString());
                    break;
                case 13:
                    base.LoadLayout(viewDeptStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentStore.ToString());
                    break;
                case 12:
                    base.LoadLayout(viewClass, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Class.ToString());
                    break;
                case 11:
                    base.LoadLayout(viewClassMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassMarket.ToString());
                    break;
                case 10:
                    base.LoadLayout(viewClassGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassGrade.ToString());
                    break;
                case 9:
                    base.LoadLayout(viewClassStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassStore.ToString());
                    break;
            }
        }

        /// <summary>
        /// Sets up menu items
        /// </summary>
        private void SetupMenu()
        {
            mnuPaste.Enabled = _editMode && _summaryWorkbenchInfo.CopiedSummaryItem != null;
            mnuRemoveOverridesThisLevel.Enabled = _summaryWorkbenchInfo.SelectedSummaryLevel == 16 ? false : EditMode;
        }

        /// <summary>
        /// Controls display of file groups to user
        /// </summary>
        private void SetFileGroup()
        {
            if (_summaryWorkbenchInfo.DailyFilegroup <= 0)
            {
                txtDailyFileGroup.Text = "N/A";
                txtDailyFileGroup.ToolTip = string.Empty;
                txtDailyFileGroup.ToolTipTitle = string.Empty;
            }
            else
            {
                txtDailyFileGroup.Text = _summaryWorkbenchInfo.DailyFilegroup.ToString("000");
                txtDailyFileGroup.ToolTip = _summaryWorkbenchInfo.SessionMemberName;
                txtDailyFileGroup.ToolTipTitle = "Summary Workbench Member";
            }

            if (_summaryWorkbenchInfo.WeeklyFilegroup <= 0)
            {
                txtWeeklyFileGroup.Text = "N/A";
                txtWeeklyFileGroup.ToolTip = string.Empty;
                txtWeeklyFileGroup.ToolTipTitle = string.Empty;
            }
            else
            {
                txtWeeklyFileGroup.Text = _summaryWorkbenchInfo.WeeklyFilegroup.ToString("000");
                txtWeeklyFileGroup.ToolTip = _summaryWorkbenchInfo.SessionMemberName;
                txtWeeklyFileGroup.ToolTipTitle = "Summary Workbench Member";
            }
        }

        /// <summary>
        /// Performs configuration of all columns in a grid
        /// </summary>
        /// <param name="view"></param>
        /// <param name="level"></param>
        private void SetupColumns(GridView view, int level)
        {
            var unknownColumns = new List<string>();

            //bool ShowHiddenFields = true; // Used for showing all hidden columns when debugging
            bool ShowHiddenFields = false;

            //view.BestFitColumns();

            foreach (GridColumn col in view.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
                col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

                if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.HierarchyKey))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Hier Key";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:0000}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Department))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Department";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:0000}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Class))
                {
                    col.Caption = "Class";
                    col.Visible = new int[] { 12, 11, 10, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:0000}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Market))
                {
                    col.Caption = "Market";
                    col.Visible = new int[] { 15, 14, 13, 11, 10, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketColour))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "MarketColour";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.MarketSequence))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "MarketSequence";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Grade))
                {
                    col.Caption = "Grade";
                    col.Visible = new int[] { 14, 13, 10, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:000}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.GradeDescription))
                {
                    col.Caption = "Grade Description";
                    col.Visible = new int[] { 14, 13, 10, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Store))
                {
                    col.Caption = "Store";
                    col.Visible = new int[] { 13, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:000}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreName))
                {
                    col.Caption = "Store Name";
                    col.Visible = new int[] { 13, 9 }.Contains(level);
                    col.OptionsColumn.ShowInCustomizationForm = col.Visible;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.KeyValue))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "KeyValue";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StoreType))
                {
                    col.Caption = "Store Type";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.WorkBench))
                {
                    col.Caption = "Workbench";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftFactor))
                {
                    col.Visible = true;
                    col.Caption = "Uplift Factor";
                    col.OptionsColumn.AllowEdit = true;
                    col.ColumnEdit = riUpliftFactor;
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftInheritedLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "UpliftInheritedLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftActualFlag))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Uplift Actual Flag";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftOverrideLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Uplift Override Level";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.UpliftStatus))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Uplift Status";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOff))
                {
                    col.Visible = true;
                    col.Caption = "Cover Cutoff";
                    col.ColumnEdit = riCutOff;
                    col.OptionsColumn.AllowEdit = true;
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffInheritedLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "CutoffInheritedLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffActualFlag))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "CutoffActualFlag";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffOverrideLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "CutoffOverrideLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.CutOffStatus))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "CutoffStatus";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Allocation))
                {
                    col.Visible = true;
                    col.Caption = "Allocation Flag";
                    col.OptionsColumn.AllowEdit = true;
                    col.ColumnEdit = riYesNo;
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationInheritedLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "AllocationInheritedLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationActualFlag))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Allocation Actual Flag";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationOverrideLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Allocation Override Level";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.AllocationStatus))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Allocation Status";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactor))
                {
                    col.Visible = true;
                    col.Caption = "Smoothing Factor";
                    col.ColumnEdit = riSmoothingFactor;
                    col.OptionsColumn.AllowEdit = true;
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N2";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorInheritedLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Smoothing Factor Inherited Level";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorActualFlag))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "SmoothFactorActualFlag";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorOverrideLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "SmoothFactorOverrideLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.SmoothFactorStatus))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "SmoothFactorStatus";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern))
                {
                    // Pattern should only show for department level
                    if (level == 16)
                    {
                        col.OptionsColumn.AllowEdit = true;
                        col.Visible = true;
                        col.OptionsColumn.ShowInCustomizationForm = true;
						col.Tag = "Show";
                    }
                    else
                    {
                        col.OptionsColumn.AllowEdit = true;
                        col.Visible = false;
                        col.OptionsColumn.ShowInCustomizationForm = true;
						col.Tag = null;
                    }
                    col.Caption = "Pattern Id";
                    col.ColumnEdit = riPatternId;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternInheritedLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "PatternInheritedLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternActualFlag))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "PatternActualFlag";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternOverrideLevel))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "PatternOverrideLevel";
                    col.DisplayFormat.FormatType = FormatType.Custom;
                    col.DisplayFormat.FormatString = "{0:00}";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.PatternStatus))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "PatternStatus";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }

                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.StockRequirement))
                {
                    col.Visible = true;
                    col.Caption = "Total Stock Requirement";
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N0";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.IdealReplenishmentQuantity))
                {
                    col.Visible = true;
                    //                    col.Caption = "Ideal Quantity";
                    col.Caption = "Ideal Allocation";
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N0";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.ProposedAllocationQuantity))
                {
                    col.Visible = true;
                    col.Caption = "Proposed Allocation";
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "N0";
                    col.Fixed = FixedStyle.Left;
                }
                else if (col.FieldName == SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Changed))
                {
                    col.Visible = ShowHiddenFields;
                    col.OptionsColumn.ShowInCustomizationForm = ShowHiddenFields;
                    col.Caption = "Changed";
                    col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    col.Fixed = FixedStyle.Left;
                }
                else
                {
                    unknownColumns.Add(col.FieldName);
                }

                if (col.OptionsColumn.AllowEdit)
                {
                    col.AppearanceCell.BackColor = this.EditMode ? Color.LightCyan : Color.White;
                }

            }

            view.ClearSelection();

            if (unknownColumns.Count > 0)
                MessageBox.Show(string.Join("\n", unknownColumns), "Unknown Columns", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Handles user exiting the department selector control
        /// </summary>
        /// <param name="controlName"></param>
        private void DepartmentSelector_ComboLeave(Controls.DepartmentSelectorControl.ControlNames controlName)
        {
            this._summaryWorkbenchInfo.DepartmentId = DepartmentSelector.DepartmentId;
            this.Text = this._summaryWorkbenchInfo.DepartmentId.ToString() + " - Summary Allocators Workbench";

			btnSearchView.Enabled = true;
            btnSearchEdit.Enabled = true;
        }

        /// <summary>
        /// Converts colour integer from As400 into a colour that can be used in UI
        /// </summary>
        /// <param name="MarketColour"></param>
        /// <returns></returns>
        private Color GetMarketBackColour(object MarketColour)
        {
            Color backColor = _colourDefaultMarketBackGround1;

            if (MarketColour != null)
            {
                UInt32 marketColour = Convert.ToUInt32(MarketColour.ToString());

                if (marketColour > 0)
                    backColor = Color.FromArgb((int)marketColour);
            }
            return backColor;
        }

		private void DeleteLowerLevelOverrides(SummaryWorkbenchInfo.SummaryLever summaryLever, string leverType)
		{
			if (Question.YesNo("Are you sure you want to submit the job to remove all the Lower Level Overrides?", "Confirm"))
			{

				this.Cursor = Cursors.WaitCursor;
				base.UpdateStatusMessage("Processing...");
				Application.DoEvents();

				_summaryWorkbenchInfo.MustReleaseLocks = false; // ensure locking is handled by the AS400 process

				SummaryItem summaryItem = (SummaryItem)GetCurrentView().GetFocusedRow() ;
				CheckFileLocks(GetCurrentView());

				_summaryWorkbenchInfo.DeleteLowerLevelOverrides(summaryItem, leverType); 

				EditMode = false;
				BuildAllViews();

				_summaryWorkbenchInfo.AliasDrop();
				this.Cursor = Cursors.Default;
				UpdateStatusMessage("Lever changes have been applied");

				_summaryWorkbenchInfo.RemoveOverridesBelowLevel(summaryItem, summaryLever);
			}
		}

        /// <summary>
        /// Removes all overrides from the current level
        /// </summary>
        private void RemoveOverridesFromLevel()
        {
            SummaryItem parentSummaryItem = new SummaryItem();
            _summaryWorkbenchInfo.ChangedSummaryItems.Clear();

            foreach (SummaryItem summaryItem in _summaryWorkbenchInfo.WorkingSummaryItems
                .Where(i => i.HierarchyKey.Equals(_summaryWorkbenchInfo.SelectedSummaryLevel))
                .Where(i => i.AllocationActualFlag.Equals(true)
                         || i.CutOffActualFlag.Equals(true)
                         || i.PatternActualFlag.Equals(true)
                         || i.SmoothFactorActualFlag.Equals(true)
                         || i.UpliftActualFlag.Equals(true))
                 )
            {
                parentSummaryItem = GetParentSummaryItem(summaryItem);
                if (summaryItem.AllocationActualFlag == true)
                {
                    summaryItem.Allocation = parentSummaryItem.Allocation;
                    summaryItem.AllocationActualFlag = false;
                    summaryItem.AllocationStatus = "D";

                    if (parentSummaryItem.AllocationActualFlag)
                    {
                        summaryItem.AllocationInheritedLevel = parentSummaryItem.HierarchyKey;
                    }
                    else
                    {
                        summaryItem.AllocationInheritedLevel = parentSummaryItem.AllocationInheritedLevel;
                    }

                    _summaryWorkbenchInfo.ApplyAllocationToLowerLevels(_summaryWorkbenchInfo.SelectedSummaryLevel, summaryItem);
                }

                if (summaryItem.CutOffActualFlag == true)
                {
                    summaryItem.CutOff = parentSummaryItem.CutOff;
                    summaryItem.CutOffActualFlag = false;
                    summaryItem.CutOffStatus = "D";

                    if (parentSummaryItem.CutOffActualFlag)
                    {
                        summaryItem.CutOffInheritedLevel = parentSummaryItem.HierarchyKey;
                    }
                    else
                    {
                        summaryItem.CutOffInheritedLevel = parentSummaryItem.CutOffInheritedLevel;
                    }

                    _summaryWorkbenchInfo.ApplyCutoffToLowerLevels(_summaryWorkbenchInfo.SelectedSummaryLevel, summaryItem);
                }

                if (summaryItem.PatternActualFlag == true)
                {
                    summaryItem.Pattern = parentSummaryItem.Pattern;
                    summaryItem.PatternActualFlag = false;
                    summaryItem.PatternStatus = "D";

                    if (parentSummaryItem.PatternActualFlag)
                    {
                        summaryItem.PatternInheritedLevel = parentSummaryItem.HierarchyKey;
                    }
                    else
                    {
                        summaryItem.PatternInheritedLevel = parentSummaryItem.PatternInheritedLevel;
                    }

                    _summaryWorkbenchInfo.ApplyPatternToLowerLevels(_summaryWorkbenchInfo.SelectedSummaryLevel, summaryItem);
                }

                if (summaryItem.SmoothFactorActualFlag == true)
                {
                    summaryItem.SmoothFactor = parentSummaryItem.SmoothFactor;
                    summaryItem.SmoothFactorActualFlag = false;
                    summaryItem.SmoothFactorStatus = "D";

                    if (parentSummaryItem.SmoothFactorActualFlag)
                    {
                        summaryItem.SmoothFactorInheritedLevel = parentSummaryItem.HierarchyKey;
                    }
                    else
                    {
                        summaryItem.SmoothFactorInheritedLevel = parentSummaryItem.SmoothFactorInheritedLevel;
                    }

                    _summaryWorkbenchInfo.ApplySmoothFactorToLowerLevels(_summaryWorkbenchInfo.SelectedSummaryLevel, summaryItem);
                }

                if (summaryItem.UpliftActualFlag == true)
                {
                    summaryItem.UpliftFactor = parentSummaryItem.UpliftFactor;
                    summaryItem.UpliftActualFlag = false;
                    summaryItem.UpliftStatus = "D";

                    if (parentSummaryItem.UpliftActualFlag)
                    {
                        summaryItem.UpliftInheritedLevel = parentSummaryItem.HierarchyKey;
                    }
                    else
                    {
                        summaryItem.UpliftInheritedLevel = parentSummaryItem.UpliftInheritedLevel;
                    }

                    _summaryWorkbenchInfo.ApplyUpliftFactorToLowerLevels(_summaryWorkbenchInfo.SelectedSummaryLevel, summaryItem);
                }

                summaryItem.Changed = "1";
                _summaryWorkbenchInfo.ChangedSummaryItems.Add(summaryItem);

            }

            if (_summaryWorkbenchInfo.Save())
                RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);
            else
                UpdateStatusMessage("Failed to save changes");
        }

        /// <summary>
        /// Provides access to the parent summary item of the current summary item
        /// </summary>
        /// <param name="summaryItem"></param>
        /// <returns></returns>
        private SummaryItem GetParentSummaryItem(SummaryItem summaryItem)
        {
            SummaryItem parentSummaryItem = new SummaryItem();

            if (summaryItem.HierarchyKey == 16) return null;

            var query = from items in _summaryWorkbenchInfo.WorkingSummaryItems
                        select items;

            // Determine which Level to inherit from
            if (summaryItem.HierarchyKey == 9)
            {
                query = query.Where(i => i.HierarchyKey == 10);
                query = query.Where(i => i.Department == summaryItem.Department);
                query = query.Where(i => i.Class == summaryItem.Class);
                query = query.Where(i => i.Market == summaryItem.Market);
                query = query.Where(i => i.Grade == summaryItem.Grade);
            }
            else if (summaryItem.HierarchyKey == 10)
            {
                query = query.Where(i => i.HierarchyKey == 11);
                query = query.Where(i => i.Department == summaryItem.Department);
                query = query.Where(i => i.Class == summaryItem.Class);
                query = query.Where(i => i.Market == summaryItem.Market);
            }
            else if (summaryItem.HierarchyKey == 11)
            {
                query = query.Where(i => i.HierarchyKey == 12);
                query = query.Where(i => i.Department == summaryItem.Department);
                query = query.Where(i => i.Class == summaryItem.Class);
            }
            else if (summaryItem.HierarchyKey == 12)
            {
                query = query.Where(i => i.HierarchyKey == 16);
                query = query.Where(i => i.Department == summaryItem.Department);
            }
            else if (summaryItem.HierarchyKey == 13)
            {
                query = query.Where(i => i.HierarchyKey == 14);
                query = query.Where(i => i.Department == summaryItem.Department);
                query = query.Where(i => i.Market == summaryItem.Market);
                query = query.Where(i => i.Grade == summaryItem.Grade);
            }
            else if (summaryItem.HierarchyKey == 14)
            {
                query = query.Where(i => i.HierarchyKey == 15);
                query = query.Where(i => i.Department == summaryItem.Department);
                query = query.Where(i => i.Market == summaryItem.Market);
            }
            else if (summaryItem.HierarchyKey == 15)
            {
                query = query.Where(i => i.HierarchyKey == 16);
                query = query.Where(i => i.Department == summaryItem.Department);
            }

            parentSummaryItem = query.FirstOrDefault();

            return parentSummaryItem;
        }

        /// <summary>
        /// Maintains status of allocation flag 
        /// </summary>
        /// <param name="summaryItem"></param>
        private void SetAllocationFlag(SummaryItem summaryItem)
        {
			if (summaryItem.Allocation.Trim().Length == 0)
			{
				if (summaryItem.AllocationStatus == "A")
					summaryItem.AllocationStatus = "U";
				else
					summaryItem.AllocationStatus = "D";
				var parent = ((SummaryItem)GetParentSummaryItem(summaryItem));
				summaryItem.Allocation = parent.Allocation;
				summaryItem.AllocationActualFlag = false;
				summaryItem.AllocationInheritedLevel = parent.AllocationInheritedLevel;
			}
			else
			{
				// If this is already an actual lever value this is a 'C'hange
				if (summaryItem.AllocationActualFlag == true && summaryItem.AllocationStatus != "A")
					summaryItem.AllocationStatus = "C";
				else // Otherwise mark it as an 'A'dded Lever
					summaryItem.AllocationStatus = "A";
				summaryItem.AllocationActualFlag = true;
				summaryItem.AllocationInheritedLevel = 0;
			}
		}

        /// <summary>
        /// Maintains status of cuttoff lever 
        /// </summary>
        /// <param name="summaryItem"></param>
        private void SetCutOff(SummaryItem summaryItem)
        {
			if (summaryItem.CutOff == null)
			{
				if (summaryItem.CutOffStatus == "A")
					summaryItem.CutOffStatus = "U";
				else
					summaryItem.CutOffStatus = "D";
				
				var parent = ((SummaryItem)GetParentSummaryItem(summaryItem));
				summaryItem.CutOff = parent.CutOff;
				summaryItem.CutOffActualFlag = false;
				summaryItem.CutOffInheritedLevel = parent.CutOffInheritedLevel;
			}
			else
			{
				// If this is already an actual lever value this is a 'C'hange
				if (summaryItem.CutOffActualFlag == true && summaryItem.CutOffStatus != "A")
					summaryItem.CutOffStatus = "C";
				else // Otherwise mark it as an 'A'dded Lever
					summaryItem.CutOffStatus = "A";

				summaryItem.CutOffActualFlag = true;
				summaryItem.CutOffInheritedLevel = 0;
			}
        }

        /// <summary>
        /// Maintains status of pattern lever
        /// </summary>
        /// <param name="summaryItem"></param>
        private void SetPattern(SummaryItem summaryItem)
        {

			if (summaryItem.Pattern == null)
			{
				if (summaryItem.PatternStatus == "A")
					summaryItem.PatternStatus = "U";
				else
					summaryItem.PatternStatus = "D";
				var parent = ((SummaryItem)GetParentSummaryItem(summaryItem));
				summaryItem.Pattern = parent.Pattern;
				summaryItem.PatternActualFlag = false;
				summaryItem.PatternInheritedLevel = parent.PatternInheritedLevel;
			}
			else
			{
				// If this is already an actual lever value this is a 'C'hange
				if (summaryItem.PatternActualFlag == true && summaryItem.PatternStatus != "A")
					summaryItem.PatternStatus = "C";
				else // Otherwise mark it as an 'A'dded Lever
					summaryItem.PatternStatus = "A";

				summaryItem.PatternActualFlag = true;
				summaryItem.PatternInheritedLevel = 0;
			}
        }

        /// <summary>
        /// Maintains status of smoothing factor lever
        /// </summary>
        /// <param name="summaryItem"></param>
        private void SetSmoothFactor(SummaryItem summaryItem)
        {

            if (summaryItem.SmoothFactor == null)
            {
				if (summaryItem.SmoothFactorStatus == "A")
					summaryItem.SmoothFactorStatus = "U";
				else
					summaryItem.SmoothFactorStatus = "D";

				var parent = ((SummaryItem)GetParentSummaryItem(summaryItem));
				summaryItem.SmoothFactor = parent.SmoothFactor;
				summaryItem.SmoothFactorActualFlag = false;
				summaryItem.SmoothFactorInheritedLevel = parent.SmoothFactorInheritedLevel ;
			}
            else
            {
                // If this is already an actual lever value this is a 'C'hange
                if (summaryItem.SmoothFactorActualFlag == true && summaryItem.SmoothFactorStatus != "A")
                    summaryItem.SmoothFactorStatus = "C";
                else // Otherwise mark it as an 'A'dded Lever
                    summaryItem.SmoothFactorStatus = "A";
				summaryItem.SmoothFactorActualFlag = true;
				summaryItem.SmoothFactorInheritedLevel = 0;
			}
        }

        /// <summary>
        /// Maintains status of uplift factor lever
        /// </summary>
        /// <param name="summaryItem"></param>
        private void SetUpliftFactor(SummaryItem summaryItem)
        {
			if (summaryItem.UpliftFactor == null)
			{
				if (summaryItem.UpliftStatus == "A")
					summaryItem.UpliftStatus = "U";
				else
					summaryItem.UpliftStatus = "D";

				var parent = ((SummaryItem)GetParentSummaryItem(summaryItem));
				summaryItem.UpliftFactor = parent.UpliftFactor;
				summaryItem.UpliftActualFlag = false;
				summaryItem.UpliftInheritedLevel = parent.UpliftInheritedLevel;
			}
			else
			{
				// If this is already an actual lever value this is a 'C'hange
				if (summaryItem.UpliftActualFlag == true && summaryItem.UpliftStatus != "A")
					summaryItem.UpliftStatus = "C";
				else // Otherwise mark it as an 'A'dded Lever
					summaryItem.UpliftStatus = "A";

				summaryItem.UpliftActualFlag = true;
				summaryItem.UpliftInheritedLevel = 0;
			}
        }

		/// <summary>
        /// Performs clean-up when form is closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryWorkbench_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_allowExit)
            {

                if (FormUtils.TagContains(this, "ForceClose"))
                {
                    _summaryWorkbenchInfo.IsDirty = false;
                    _summaryWorkbenchInfo.ReleaseLocks();
                }

                if (_summaryWorkbenchInfo.IsDirty && _summaryWorkbenchInfo.ApplyChangesAllowed)
                    switch (Question.YesNoCancel("Changes have been made to these items, save them before exiting?", this.Text))
                    {
                        case System.Windows.Forms.DialogResult.Yes:

                            var timeStart = System.DateTime.Now;
                            this.Cursor = Cursors.WaitCursor;
                            base.UpdateStatusMessage("Applying changes...");

                            if (_summaryWorkbenchInfo.SaveChanges() && _summaryWorkbenchInfo.ApplyChanges())
                            {
                                EditMode = false;
                                UpdateStatusMessage("Lever changes have been applied");
                            }
                            else
                                UpdateStatusMessage("Failed to apply lever changes");
                            this.Cursor = Cursors.Default;
                            break;

                        case System.Windows.Forms.DialogResult.No:
                            _summaryWorkbenchInfo.ReleaseLocks();
                            break;

                        case System.Windows.Forms.DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
            }
            else
                e.Cancel = true;
        }

        private void SummaryWorkbench_FormClosed(object sender, FormClosedEventArgs e)
        {
            _summaryWorkbenchInfo.AliasDrop();
            _summaryWorkbenchInfo.CleanupAS400FileMembers();

            base.SaveLayout(this.viewDept, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Department.ToString());
            base.SaveLayout(this.viewDeptMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentMarket.ToString());
            base.SaveLayout(this.viewDeptGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentGrade.ToString());
            base.SaveLayout(this.viewDeptStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentStore.ToString());
            base.SaveLayout(this.viewClass, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Class.ToString());
            base.SaveLayout(this.viewClassMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassMarket.ToString());
            base.SaveLayout(this.viewClassGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassGrade.ToString());
            base.SaveLayout(this.viewClassStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassStore.ToString());
        }

        private void grid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
					GridControl grid = GetCurrentGrid();
                    if (grid == gridDept)
						ResetLayout(16);
                    if (grid == gridDeptMarket)
						ResetLayout(15);
                    if (grid == gridDeptGrade)
						ResetLayout(14);
                    if (grid == gridDeptStore)
						ResetLayout(13);
                    if (grid == gridClass)
						ResetLayout(12);
                    if (grid == gridClassMarket)
						ResetLayout(11);
                    if (grid == gridClassGrade)
						ResetLayout(10);
                    if (grid == gridClassStore)
						ResetLayout(9);
                    break;
            }
        }

        private void ResetLayout(int level)
        {
            switch (level)
            {
                case 16:
                    base.ResetLayout(viewDept, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Department.ToString());
                    break;
                case 15:
                    base.ResetLayout(viewDeptMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentMarket.ToString());
                    break;
                case 14:
                    base.ResetLayout(viewDeptGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentGrade.ToString());
                    break;
                case 13:
                    base.ResetLayout(viewDeptStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.DepartmentStore.ToString());
                    break;
                case 12:
                    base.ResetLayout(viewClass, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.Class.ToString());
                    break;
                case 11:
                    base.ResetLayout(viewClassMarket, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassMarket.ToString());
                    break;
                case 10:
                    base.ResetLayout(viewClassGrade, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassGrade.ToString());
                    break;
                case 9:
                    base.ResetLayout(viewClassStore, "SummaryWorkbench." + SummaryWorkbenchInfo.SummaryLevel.ClassStore.ToString());
                    break;
            }

            RefreshGrid(GetCurrentGrid(), GetCurrentView(), _summaryWorkbenchInfo.SelectedSummaryLevel);

			foreach (GridColumn col in GetCurrentView().Columns)
				if (col.Visible || col.OptionsColumn.ShowInCustomizationForm)
					col.VisibleIndex = col.AbsoluteIndex;

			if (level != 16)
				GetCurrentView().Columns[SummaryWorkbenchInfo.GetSummaryItemName(SummaryWorkbenchInfo.SummaryItemColumns.Pattern)].Visible = false;
        }

        private void mnuResetLayout_Click(object sender, EventArgs e)
        {
			ResetLayout(_summaryWorkbenchInfo.SelectedSummaryLevel);
        }

        private void viewDept_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewDept.CustomizationForm.Text = "Department Grid Columns";
        }

        private void viewDeptMarket_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewDeptMarket.CustomizationForm.Text = "Department Market Grid Columns";
        }

        private void viewDeptGrade_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewDeptGrade.CustomizationForm.Text = "Department Grade Grid Columns";
        }

        private void viewDeptStore_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewDeptStore.CustomizationForm.Text = "Department Store Grid Columns";
        }

        private void viewClass_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewClass.CustomizationForm.Text = "Class Grid Columns";
        }

        private void viewClassMarket_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewClassMarket.CustomizationForm.Text = "Class Market Grid Columns";
        }

        private void viewClassGrade_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewClassGrade.CustomizationForm.Text = "Class Grade Grid Columns";
        }

        private void viewClassStore_ShowCustomizationForm(object sender, EventArgs e)
        {
            viewClassStore.CustomizationForm.Text = "Class Store Grid Columns";
        }

		private void mnuExport_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = "xlsx",
				AddExtension = true,
				AutoUpgradeEnabled = true,
				CheckPathExists = true,
				FileName = "Summary Workbench Extract",
				Filter = "Microsoft Excel Files (*.xlsx)|*.xlsx",
				FilterIndex = 0,
				OverwritePrompt = true,
				Title = "Export to Excel"
			};

			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Application.DoEvents();
                var utils = new DevExpressUtils();
                utils.ExportToExcel(dialog.FileName, viewDept, viewDeptMarket, viewDeptGrade, viewDeptStore, viewClass, viewClassMarket, viewClassGrade, viewClassStore);
			}
		}
        //-----------------------------------------------------------------------------------------
        #endregion
    }

}
