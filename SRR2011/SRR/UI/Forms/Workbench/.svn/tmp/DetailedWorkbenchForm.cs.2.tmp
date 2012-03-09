using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    public partial class DetailedWorkbenchForm : Disney.iDash.SRR.UI.Forms.Common.BaseParameters
    {
        private CellHighlighter _highlights = new CellHighlighter();
        private bool _editMode = false;
		private const string kGridRegistryKey = "DetailedWorkbench.";
		private bool _allowExit = true;
        private bool _cellChanging = false;
        private bool _giveItBackChanged = false;
        
        private BackgroundWorker _bwSearch = null;

        #region Public methods and functions
        //-----------------------------------------------------------------------------------------
        public DetailedWorkbenchForm(bool editMode = false)
        {
            InitializeComponent();
						
			viewLevers.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
            viewLevers.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewLevers.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

			riHierarchyLevel.DataSource = new ArrayList(DetailedWorkbenchInfo.GetHierarchyLevels);
            riPatternId.DataSource = Instance.GetPatterns;

            this.MdiParent = FormUtils.FindMdiParent();
            EditMode = editMode;

        }

        public override void ShowDetailedForm(DetailedWorkbenchInfo WBDetailedInfo)
        {
			if (base.Setup())
			{
				Application.UseWaitCursor = true;
				Application.DoEvents();

				base.Instance = WBDetailedInfo;
				base.Instance.ProgressEvent += ((message, percentageComplete) =>
					{
						base.UpdateProgress(message, percentageComplete);
					});

				base.Instance.SelectedItem.Clear();
				base.Instance.Stage = BusinessLayer.DetailedWorkbenchInfo.Stages.DetailedWorkbench;
				this.Text = base.Instance.DepartmentId.ToString() + " - Detailed Allocators Workbench";
				this.Show();
				Application.UseWaitCursor = false;
				RefreshGrid(true);
			}
			
        }

        private bool EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                RefreshButtons();
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (base.GoBack())
            {
				this.Close();
                base.ShowParentForm();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
			var ok = false;
			var buttonState = new ControlStateCollection();

            if (Question.YesNo("Are you sure you want to apply changes to these levers?", this.Text))
			{
                viewLevers.CloseEditor();
                viewLevers.UpdateCurrentRow();

				buttonState.SaveState(btnEdit, btnModelRun, btnApply, btnBack, btnExit);
				buttonState.Disable();
				_allowExit = false;

				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();

                base.Instance._giveItBackChanged = _giveItBackChanged;
                ok = base.Instance.ApplyChanges();
                _giveItBackChanged = false;
                base.Instance._giveItBackChanged = false;

				this.Cursor = Cursors.Default;
				
				buttonState.RestoreState();
				_allowExit = true; 
				
				if (ok)
				{
					base.IsDirty = false;
					this.Close();
					base.ShowParentForm();
				}
            }
			
		}

        private void btnModelRun_Click(object sender, EventArgs e)
        {
			var ok = false;
			var buttonState = new ControlStateCollection();

			if (Question.YesNo("Commence the Model Run?", this.Text))
			{
                viewLevers.CloseEditor();
                viewLevers.UpdateCurrentRow();

				buttonState.SaveState(btnEdit, btnModelRun, btnApply, btnBack, btnExit);
				buttonState.Disable();
				_allowExit = false;

				this.Cursor = Cursors.WaitCursor;
				Application.DoEvents();

                base.Instance._giveItBackChanged = _giveItBackChanged;
                ok = base.Instance.RunModel();
  
                _giveItBackChanged = false;
                base.Instance._giveItBackChanged = false;

				this.Cursor = Cursors.Default;

				buttonState.RestoreState();
				_allowExit = true;

				if (ok)
				{
					gridLevers.RefreshDataSource();

					if (base.Instance.GetGiveItBackItems.Count() > 0)
					{
						var frm = new GiveItBackConfirmationDialog();
						frm.ShowForm(base.Instance.GetGiveItBackItems);
					}
				}
			}
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            grpCriteria.Visible = !grpCriteria.Visible;
            if (grpCriteria.Visible)
            {
                btnHide.Text = "&Hide Criteria";
                gridLevers.Top = grpCriteria.Top + grpCriteria.Height + 6;
                gridLevers.Height -= grpCriteria.Height + 6;
            }
            else
            {
                btnHide.Text = "&Show Criteria";
                gridLevers.Top = grpCriteria.Top;
                gridLevers.Height += grpCriteria.Height + 6;
            }
        }
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditMode = true;
            RefreshGrid(false);
        }

        private void viewLevers_RowStyle(object sender, RowStyleEventArgs e)
        {
			if (e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
			{
				var storeType = viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colStoreType);
				var workBench = viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colWorkBench);               

				// Don't set row highlighting when alternative highlighting is enabled.
				if (viewLevers.OptionsView.EnableAppearanceEvenRow == false && viewLevers.OptionsView.EnableAppearanceOddRow == false && storeType != null && workBench != null)
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

        private void viewLevers_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
			if (e != null && e.Column != null && e.RowHandle != GridControl.InvalidRowHandle && e.RowHandle != GridControl.AutoFilterRowHandle && e.RowHandle != GridControl.NewItemRowHandle)
			{
				var defaultColor = Color.Black;
				var columnName = e.Column.FieldName;

                // Set inheritted Cell Fore Colour
				if ((columnName == DetailedWorkbenchInfo.colNewUpliftFactor && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colUpliftThisLevel) ?? string.Empty).ToString() != "Y")
					|| (columnName == DetailedWorkbenchInfo.colNewCutOff && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colCutOffThisLevel) ?? string.Empty).ToString() != "Y")
					|| (columnName == DetailedWorkbenchInfo.colNewAllocFlag && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colAllocFlagThisLevel) ?? string.Empty).ToString() != "Y")
					|| (columnName == DetailedWorkbenchInfo.colNewSmoothingFactor && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colSmoothingFactorThisLevel) ?? string.Empty).ToString() != "Y")
                    || (columnName == DetailedWorkbenchInfo.colNewPattern && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colPatternThisLevel) ?? string.Empty).ToString() != "Y"))
					defaultColor = Properties.Settings.Default.InheritedCellForeColor;

                // Cludge: 'Hide' the values of Ideal and Proposed Alloc qty by setting the foreground and background to the same colour.
                else if (((columnName == DetailedWorkbenchInfo.colIdealAllocQty || columnName == DetailedWorkbenchInfo.colProposedAllocQty) && (viewLevers.GetRowCellValue(e.RowHandle, DetailedWorkbenchInfo.colHideQuantities) ?? string.Empty).ToString() == "1"))
                    defaultColor = Color.Aquamarine;

                // Display fixed band cell foreground as red for AppItems.
                else if (IsPackItem(e.RowHandle) && bandFixed.Columns.Contains((BandedGridColumn)e.Column))
                {
                    defaultColor = Color.Red;
                    lblAPPAllocations.Visible = true;
                }
                
                // Don't highlight changed cells in red if we are in 'Show Changes' mode.  All row indexes will be invalid for this view.
                if (mnuShowChanges.CheckState == CheckState.Unchecked)
                    _highlights.SetRowCellStyle(viewLevers, e, defaultColor, Color.Red);
                else
                    e.Appearance.ForeColor = defaultColor;

				// show the inheritted field as bold.
				if (defaultColor == Properties.Settings.Default.InheritedCellForeColor)
					e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, e.Appearance.Font.Style | FontStyle.Bold);
			}
        }

        private void viewLevers_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (this.Visible)
            {
                _highlights.CellValueChanged(viewLevers, e);
                if (e.Column.FieldName == DetailedWorkbenchInfo.colNewSmoothingFactor)
                    _highlights.CellValueChanged(viewLevers, e, "col" + DetailedWorkbenchInfo.colNewSmoothedRateOfSale);

                IsDirty = true;
            }
        }

        private void viewLevers_ShowingEditor(object sender, CancelEventArgs e)
        {
            SysInfo sysInfo = new SysInfo();

            if (this.Visible)
            {

                var error = string.Empty;
                
                var workbench = (viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colWorkBench) ?? string.Empty).ToString();
				var storeType = (viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colStoreType) ?? string.Empty).ToString();
                var APPItemNo = (viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colAPPItemNo) ?? string.Empty).ToString();

				var stockItem = new StockItem((decimal)viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colClass),
												(decimal)viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colVendor),
												(decimal)viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colStyle),
												(decimal)viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colColour),
												(decimal)viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colSize));

                if (base.Instance.GetNextFileGroup(workbench))
				{
					CriteriaSummary.SetFileGroup(base.Instance);
					e.Cancel = !base.Instance.LockItem(workbench, stockItem);
				}
				else
					e.Cancel = true;

                if (e.Cancel)
                    error = this.Instance.ErrorMessage;
                else
                {
                    // Include here any logic to determine whether a cell can be edited.
                    switch (viewLevers.FocusedColumn.FieldName)
                    {
                        case DetailedWorkbenchInfo.colGiveItBack:
                            var storType = viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colStoreType);
                                                        
                            var currentLimit = (decimal)viewLevers.GetFocusedRowCellValue(GiveItBackCollection.GiveItBackMethod.ToString().ToUpper());
                            e.Cancel = !(currentLimit > 0 && workbench == Constants.kDaily);

                            if (e.Cancel)
                            {
                                error = "Give it Back can only be applied for Daily entries where the " + GiveItBackCollection.GiveItBackMethod.ToString() + " value is greater than zero.";
                            }

                            if (storType.ToString() == Constants.kBricksAndMortar)
                            {
                                e.Cancel = true;
                                error = "Give It Back cannot be applied to Brick && Mortar Stores.";
                            }

                            if (base.Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleLevel || base.Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleGradeLevel
                                || base.Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleMarketLevel || base.Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleStoreLevel)
                            {
                                e.Cancel = true;
                                error = "Cannot Give It Back at any Style level.";
                            }
                            break;

                        case DetailedWorkbenchInfo.colNewAllocFlag:
                            if (IsPackItem() && APPItemNo != string.Empty)
                            {
                                e.Cancel = true;
                                error = "Allocations may not be applied to pack items with outstanding APP's.";
                            }
                            break;
                    }
                }

				if (e.Cancel)
					base.UpdateStatusMessage(error, true);
				else
					base.ClearStatusMessage();
            }

        }

		private void viewLevers_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (e.PrevFocusedRowHandle>=0)
				base.ClearStatusMessage();
        }

		private void viewLevers_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
            string statusMsg = String.Empty;

			switch (viewLevers.FocusedColumn.FieldName)
			{				
				case DetailedWorkbenchInfo.colGiveItBack:
					if (e.Value == null || e.Value.ToString() == string.Empty || "YN".Contains(e.Value.ToString()) == false)
					{
						e.Valid = false;
						e.ErrorText = "Only Y or N permitted";
					}
					break;

				case DetailedWorkbenchInfo.colNewPattern:
					var storeType = viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colStoreType).ToString();
					if (storeType == Constants.kOnline)
						UpdateCells("PATTERN", e, CheckRange(e.Value, 0, 99), "Valid range is: 0 to 99");
					else
						UpdateCells("PATTERN", e, CheckRange(e.Value, 100, 999), "Valid range is: 100 to 999");
					break;

				case DetailedWorkbenchInfo.colNewUpliftFactor:
					UpdateCells("UPLIFT", e, CheckRange(e.Value, 0.01m, 9.99m), "Valid range is: 0.01 to 9.99");
					break;

				case DetailedWorkbenchInfo.colNewCutOff:
					UpdateCells("CUTOFF", e, CheckRange(e.Value, 0.00m, 99.99m), "Valid range is: 0.00 to 99.999");
					break;

				case DetailedWorkbenchInfo.colNewAllocFlag:
					UpdateCells("ALLOCFLAG", e, "YN ".Contains((e.Value ?? string.Empty).ToString()), "Only Y or N permitted");
					break;

				case DetailedWorkbenchInfo.colNewSmoothingFactor:
					UpdateCells("SMOOTHINGFACTOR", e, CheckRange(e.Value, 0m, 1m), "Valid range is: 0 to 1");
					break;

			}
		}

		/// Blank out the existing Y or N before updating the contents to ensure the field registers a change event.
		private void riYesNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			viewLevers.EditingValue = null;
		}

		private bool CheckRange(object cellValue, decimal minValue, decimal maxValue)
		{
			return cellValue == null || (((decimal)cellValue) >= minValue && ((decimal)cellValue) <= maxValue);
		}

		private void viewLevers_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
		{
			if (e.Band != null)
			{
				var rect = e.Bounds;
				ControlPaint.DrawBorder3D(e.Graphics, e.Bounds);
				var backColor = e.Band.AppearanceHeader.BackColor;

				if (e.Band.Caption == string.Empty)
					backColor = Color.LightYellow;

				var brush = e.Cache.GetGradientBrush(rect, backColor, e.Band.AppearanceHeader.BackColor2, e.Band.AppearanceHeader.GradientMode);
				rect.Inflate(-1, -1);

				// Fill column headers with the specified colors.
				e.Graphics.FillRectangle(brush, rect);
				e.Appearance.DrawString(e.Cache, e.Info.Caption, e.Info.CaptionRect);

				// Draw the filter and sort buttons.
				foreach (DevExpress.Utils.Drawing.DrawElementInfo info in e.Info.InnerElements)
					DevExpress.Utils.Drawing.ObjectPainter.DrawObject(e.Cache, info.ElementPainter, info.ElementInfo);
			}
			e.Handled = true;
		}
		
		private void gridLevers_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    ConfirmRefresh(e.Control);
                    break;
            }
        }

        private void DetailedWorkbench_FormClosing(object sender, FormClosingEventArgs e)
        {
			if (_allowExit)
			{
				if (!e.Cancel)
				{
					base.Instance.CloseAS400Files();
					base.SaveLayout(this.viewLevers, kGridRegistryKey + base.Instance.Parameter.ToString());
				}
			}
			else
				e.Cancel = true;
        }

        private void mnuGridContextStrip_Opened(object sender, EventArgs e)
        {
            SetupMenu();
        }

        private void mnuStyleLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.StyleLevel);
        }

        private void mnuStyleMarketLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.StyleMarketLevel);
        }

        private void mnuStyleGradeLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.StyleGradeLevel);
        }

        private void mnuStyleStoreLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.StyleStoreLevel);
        }

        private void mnuItemLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.ItemLevel);
        }

        private void mnuItemMarketLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.ItemMarketLevel);
        }

        private void mnuItemGradeLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.ItemGradeLevel);
        }

        private void mnuItemStoreLevel_Click(object sender, EventArgs e)
        {
            SwitchLevel(BusinessLayer.DetailedWorkbenchInfo.Parameters.ItemStoreLevel);
        }

        private void mnuShowAll_Click(object sender, EventArgs e)
        {
            SwitchLevel(base.Instance.Parameter);
        }
        
		private void mnuReviewAppAllocation_Click(object sender, EventArgs e)
        {
			var showRelease = false;
			var manual = new WorkbenchManualInfo();
			var stockItem = new StockItem(viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colAPPItemNo).ToString());
            var workbench = viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colWorkBench).ToString();

            try
            {
                manual.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

                stockItem.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });

                base.ClearStatusMessage();

                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                if (manual.IsItemLocked(stockItem))
                    base.UpdateStatusMessage("APP is being reviewed by " + manual.LockedBy, true);

                else if (this.IsDirty)
                    switch (Question.YesNoCancel("Apply uncommitted changes to the model", "Review APP Allocation"))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            base.Instance._giveItBackChanged = _giveItBackChanged;
                            showRelease = base.Instance.ApplyChanges();
                            _giveItBackChanged = false;
                            base.Instance._giveItBackChanged = false;
                            break;

                        case System.Windows.Forms.DialogResult.No:
                            base.Instance.DiscardChanges();
                            base.IsDirty = false;
                            showRelease = true;
                            break;

                        case System.Windows.Forms.DialogResult.Cancel:
                            break;
                    }
                else
                {
                    showRelease = true;
                }

                CriteriaSummary.SetFileGroup(base.Instance);

                if (showRelease)
                {
                    if (base.Instance.GetNextFileGroup(workbench))
                    {
                        var frm = new Forms.Workbench.WorkbenchAPPAllocationReleaseForm();

                        UpdateStatusMessage("Loading APP pre-pack Allocation workbench...");
                        var setupOk = frm.Setup(stockItem, workbench, base.Instance.GetMember(workbench));
                        ClearStatusMessage();

                        if (setupOk)
                        {
                            frm.ShowForm();
                            base.Instance.ReleaseLocks();
                            CriteriaSummary.SetFileGroup(base.Instance);
                            //if (frm.RefreshParent)
                            //{
                                RefreshGrid(true);
                            //}
                        }
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                base.Instance.ReleaseLocks();
            }
        }

        private void mnuShowChanges_Click(object sender, EventArgs e)
        {
            if (mnuShowChanges.Checked && this.Instance.GetRawChangedCount > 0)
                gridLevers.DataSource = this.Instance.GetLastResult(true);
            else
            {
                mnuShowChanges.Checked = false;
                gridLevers.DataSource = this.Instance.GetLastResult();
            }

            lblShowingChanges.Visible = mnuShowChanges.Checked;
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            ConfirmRefresh(false);
        }

        private void mnuRefreshAndRebuild_Click(object sender, EventArgs e)
        {
            ConfirmRefresh(true);
        }

		private void mnuExport_Click(object sender, EventArgs e)
		{
			var dialog = new SaveFileDialog
			{
				DefaultExt = "xlsx",
				AddExtension = true,
				AutoUpgradeEnabled = true,
				CheckPathExists = true,
				FileName = "Detailed Workbench Extract",
				Filter = "Microsoft Excel Files (*.xlsx)|*.xlsx",
				FilterIndex = 0,
				OverwritePrompt = true,
				Title = "Export to Excel"
			};

			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Application.DoEvents();
                var utils = new DevExpressUtils();
				utils.ExportToExcel(dialog.FileName, viewLevers);
			}
		}

		private void mnuCopy_Click(object sender, EventArgs e)
		{
			viewLevers.CopyToClipboard();
		}
		
		private void riPatternId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)
                viewLevers.EditingValue = null;
        }

        private void riCommon_Spin(object sender, DevExpress.XtraEditors.Controls.SpinEventArgs e)
        {
            e.Handled = true;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void ConfirmRefresh(bool resetLayout = false)
        {
            if (base.GoBack())
            {
                if (base.IsDirty)
                    base.Instance.DiscardChanges();
                if (resetLayout)
					base.ResetLayout(this.viewLevers, kGridRegistryKey + base.Instance.Parameter.ToString());
				RefreshGrid(resetLayout);
            }
        }

        private void RefreshGrid(bool clearHighlights, object previousParameter = null)
        {
            this.Cursor = Cursors.WaitCursor;

            viewLevers.ClearColumnsFilter();
            viewLevers.ClearGrouping();
            viewLevers.ClearSorting();
            
            Application.DoEvents();

            CriteriaSummary.SetValues(base.Instance);
			CriteriaSummary.SetFileGroup(base.Instance);

            lblAPPAllocations.Visible = false;

            try
            {
                DataTable data = new DataTable();
                var firstTime = gridLevers.DataSource == null;

                if (_bwSearch == null)
                {
                    _bwSearch = new BackgroundWorker();
                    _bwSearch.DoWork += ((sender, e)=>
                        {
                            data = base.Instance.GetData();
                        });

                    _bwSearch.RunWorkerCompleted += ((sender, e) =>
                    {
                        viewLevers.Columns.Clear();
                        gridLevers.DataSource = data;
                        viewLevers.OptionsBehavior.Editable = this.EditMode;

                        SetupColumns();
                        base.LoadLayout(this.viewLevers, kGridRegistryKey + base.Instance.Parameter.ToString(), firstTime);

                        // If one type of store and workbench selected then ensure alternate band highlighting is enabled
                        if (base.Instance.Workbench == Constants.Workbenches.Both && base.Instance.StoreType == Constants.StoreTypes.Both)
                        {
                            viewLevers.OptionsView.EnableAppearanceEvenRow = false;
                            viewLevers.OptionsView.EnableAppearanceOddRow = false;
                        }
                        else
                        {
                            viewLevers.OptionsView.EnableAppearanceEvenRow = true;
                            viewLevers.OptionsView.EnableAppearanceOddRow = true;
                        }

                        gridLevers.Visible = true;

                        RefreshButtons();
                        _allowExit = true;
                        base.IsDirty = false;
                        this.Cursor = Cursors.Default;

                        if (viewLevers.RowCount > 0)
                            gridLevers.Focus();
                    });
                }

                if (clearHighlights)
					_highlights.Clear();

				if (!firstTime)
				{
					if (previousParameter == null)
					{
						if (!clearHighlights)
							base.SaveLayout(this.viewLevers, kGridRegistryKey + base.Instance.Parameter.ToString());
					}
					else
						base.SaveLayout(this.viewLevers, kGridRegistryKey + ((DetailedWorkbenchInfo.Parameters)previousParameter).ToString());
				}

                _allowExit = false;
                _bwSearch.RunWorkerAsync();
                
            }
            catch (Exception ex)
            {
                _allowExit = true;
                this.Cursor = Cursors.Default;
                ErrorDialog.Show(ex, "RefreshGrid");
            }
        }

        private bool IsPackItem()
        {
            return (!base.Instance.Parameter.ToString().Contains("Style") && viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colPack) != null && viewLevers.GetFocusedRowCellValue(DetailedWorkbenchInfo.colPack).ToString() == "Y");
        }

        private bool IsPackItem(int rowHandle)
        {
            return (!base.Instance.Parameter.ToString().Contains("Style") && viewLevers.GetRowCellValue(rowHandle, DetailedWorkbenchInfo.colPack) != null && viewLevers.GetRowCellValue(rowHandle, DetailedWorkbenchInfo.colPack).ToString() == "Y");
        }

        private void RefreshButtons()
        {
            if (_editMode)
            {
                btnEdit.Enabled = false;
                btnApply.Enabled = true && viewLevers.RowCount > 0;
				btnModelRun.Enabled = true && viewLevers.RowCount > 0;
            }
            else
            {
                btnEdit.Enabled = viewLevers.RowCount > 0;
                btnApply.Enabled = false;
                btnModelRun.Enabled = false;
            }
			btnBack.Enabled = true;
			btnExit.Enabled = true;
        }

        private void SetupMenu()
        {
            mnuStyle.Enabled = viewLevers.RowCount > 0 && viewLevers.FocusedRowHandle != GridControl.InvalidRowHandle
                && viewLevers.FocusedRowHandle != GridControl.AutoFilterRowHandle
                && viewLevers.FocusedRowHandle != GridControl.NewItemRowHandle
                && !viewLevers.IsGroupRow(viewLevers.FocusedRowHandle);

            mnuItem.Enabled = mnuStyle.Enabled;
            mnuShowAll.Enabled = mnuStyle.Enabled;
            mnuReviewAppAllocation.Enabled = this.EditMode && IsPackItem();
            mnuExport.Enabled = viewLevers.RowCount > 0;

            mnuItemLevel.Visible = true;
            mnuItemMarketLevel.Visible = true;
            mnuItemGradeLevel.Visible = true;
            mnuItemStoreLevel.Visible = true;

            mnuStyleLevel.Visible = true;
            mnuStyleMarketLevel.Visible = true;
            mnuStyleGradeLevel.Visible = true;
            mnuStyleStoreLevel.Visible = true;
            
            switch (base.Instance.Parameter)
            {

                case DetailedWorkbenchInfo.Parameters.ItemLevel:
                    mnuItemLevel.Visible = false;
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleLevel:
                    mnuStyleLevel.Visible=false;
                    break;

                case DetailedWorkbenchInfo.Parameters.ItemMarketLevel:
                    mnuItemMarketLevel.Visible = false;
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleMarketLevel:
                    mnuStyleMarketLevel.Visible=false;
                    break;

                case DetailedWorkbenchInfo.Parameters.ItemGradeLevel:
                    mnuItemGradeLevel.Visible = false;
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleGradeLevel:
                    mnuStyleGradeLevel.Visible=false;
                    break;

                case DetailedWorkbenchInfo.Parameters.ItemStoreLevel:
                    mnuItemStoreLevel.Visible = false;
                    break;

                case DetailedWorkbenchInfo.Parameters.StyleStoreLevel:
                    mnuStyleStoreLevel.Visible=false;
                    break;
            }
        }

        private void UpdateCells(string lever, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e, bool isValid = true, string errorText = "")
        {
			var atThislevelFieldname = lever + "THISLEVEL";
			var statusFieldname = lever + "STATUS";
			var hierarchyLevelFieldname = lever + "HIERARCHYLEVEL";
			var atThislevelValue = (viewLevers.GetFocusedRowCellValue(atThislevelFieldname) ?? "N").ToString();
			var statusValue = viewLevers.GetFocusedRowCellValue(statusFieldname).ToString();

            try
            {
                if (atThislevelValue == string.Empty)
                    atThislevelValue = "N";

                // Has user has blanked out/deleted cell contents?
                if (e.Value == null || e.Value.ToString().Trim() == string.Empty)
                {
                    // Get the inherited value for the cell
                    var inherittedValue = base.Instance.GetInherittedValue(viewLevers.GetFocusedDataRow(), base.Instance.Parameter);
                    if (inherittedValue.Exists)
                    {
                        switch (viewLevers.FocusedColumn.FieldName)
                        {
                            case DetailedWorkbenchInfo.colNewAllocFlag:
                                e.Value = inherittedValue.AllocationLever.Value;
                                viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, inherittedValue.AllocationLever.HierarchyLevel);
                                break;

                            case DetailedWorkbenchInfo.colNewCutOff:
                                e.Value = inherittedValue.CutOffLever.Value;
                                viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, inherittedValue.CutOffLever.HierarchyLevel);
                                break;

                            case DetailedWorkbenchInfo.colNewPattern:
                                e.Value = inherittedValue.PatternLever.Value;
                                viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, inherittedValue.PatternLever.HierarchyLevel);
                                break;

                            case DetailedWorkbenchInfo.colNewSmoothingFactor:
                                e.Value = inherittedValue.SmoothingLever.Value;
                                viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, inherittedValue.SmoothingLever.HierarchyLevel);
                                break;

                            case DetailedWorkbenchInfo.colNewUpliftFactor:
                                e.Value = inherittedValue.UpliftLever.Value;
                                viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, inherittedValue.UpliftLever.HierarchyLevel);
                                break;
                        }

                        // Indicate we have deleted the 'actual' value for the cell
                        if (statusValue == DetailedWorkbenchInfo.kStatusAdded || atThislevelValue == "N")
                            viewLevers.SetFocusedRowCellValue(statusFieldname, DetailedWorkbenchInfo.kStatusUnchanged);
                        else
                            viewLevers.SetFocusedRowCellValue(statusFieldname, DetailedWorkbenchInfo.kStatusDeleted);

                        // Set 'at this level' flag to 'N' to indicate the value will be inheritted.
                        viewLevers.SetFocusedRowCellValue(atThislevelFieldname, "N");
                    }
                }
                else if (isValid)
                {
                    //if (oldValue != currentValue || viewLevers.ActiveEditor.ForeColor.Name == "ff000000") ;
                    if (_cellChanging == true)
                    {
                        // If the cell was previously 'not at this level', i.e. inheritted then indicate we are adding a new value
                        // otherwise indicate we are changing a value at this level
                        if (atThislevelValue == "N")
                            viewLevers.SetFocusedRowCellValue(statusFieldname, DetailedWorkbenchInfo.kStatusAdded);
                        else if (statusValue != DetailedWorkbenchInfo.kStatusAdded)
                            viewLevers.SetFocusedRowCellValue(statusFieldname, DetailedWorkbenchInfo.kStatusChanged);

                        // Set the lever to 'at this level' and update the hierarchy value
                        viewLevers.SetFocusedRowCellValue(atThislevelFieldname, "Y");

                        viewLevers.SetFocusedRowCellValue(hierarchyLevelFieldname, base.Instance.Parameter);
                    }
                }
                else
                {
                    e.Valid = false;
                    e.ErrorText = errorText;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _cellChanging = false;
            }
        }

        private void SetupColumns()
        {
            var unknownColumns = new List<string>();

			gridLevers.SuspendLayout();

			bandFixed.Columns.Clear();
			bandLeft.Columns.Clear();

			bandAllocation.Columns.Clear();		
			bandCutOff.Columns.Clear();
			bandSmoothing.Columns.Clear();
			bandPattern.Columns.Clear();
			bandUplift.Columns.Clear();
			bandRight.Columns.Clear();

            foreach (BandedGridColumn col in viewLevers.Columns)
            {
                col.OptionsColumn.AllowEdit = false;
                col.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
				col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;			

                switch (col.FieldName.ToUpper())
                {
                    case DetailedWorkbenchInfo.colItem:
						bandFixed.Columns.Add(col);
						if (DetailedWorkbenchInfo.GetParameterName(base.Instance.Parameter).Contains("Style"))
							col.Caption = "Style No.";
						else
							col.Caption = "Item";
                        break;

                    case DetailedWorkbenchInfo.colClass:
						bandFixed.Columns.Add(col);
                        col.Caption = "Class";
                        col.DisplayFormat.FormatType = FormatType.Custom;
                        col.DisplayFormat.FormatString = "{0:0000}";
						col.Visible = false;
                        break;

                    case DetailedWorkbenchInfo.colVendor:
						bandFixed.Columns.Add(col);
                        col.Caption = "Vendor";
                        col.DisplayFormat.FormatType = FormatType.Custom;
                        col.DisplayFormat.FormatString = "{0:00000}";
						col.Visible = false;
                        break;

                    case DetailedWorkbenchInfo.colStyle:
						bandFixed.Columns.Add(col);
                        col.Caption = "Style";
                        col.DisplayFormat.FormatType = FormatType.Custom;
                        col.DisplayFormat.FormatString = "{0:0000}";
						col.Visible = false;
                        break;

                    case DetailedWorkbenchInfo.colColour:
						bandFixed.Columns.Add(col);
                        col.Caption = "Colour";
                        col.DisplayFormat.FormatType = FormatType.Custom;
                        col.DisplayFormat.FormatString = "{0:000}";
						col.Visible = false;
                        break;

                    case DetailedWorkbenchInfo.colSize:
						bandFixed.Columns.Add(col);
                        col.Caption = "Size";
                        col.DisplayFormat.FormatType = FormatType.Custom;
                        col.DisplayFormat.FormatString = "{0:0000}";
						col.Visible = false;
                        break;

                    case DetailedWorkbenchInfo.colDescription:
						bandFixed.Columns.Add(col);
                        col.Caption = "Description";
                        break;

					case DetailedWorkbenchInfo.colStoreType:
						bandFixed.Columns.Add(col);
						col.Caption = "Store Type";
						break;

					case DetailedWorkbenchInfo.colWorkBench:
						bandFixed.Columns.Add(col);
						col.Caption = "Workbench";
						break;

                    case DetailedWorkbenchInfo.colMarket:
                        col.Caption = "Market";
                        col.Visible = Instance.Parameter == DetailedWorkbenchInfo.Parameters.ItemMarketLevel || Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleMarketLevel;
						col.OptionsColumn.ShowInCustomizationForm = col.Visible;
						if (col.Visible)
							bandFixed.Columns.Add(col);
                        break;

                    case DetailedWorkbenchInfo.colGrade:
                        col.Caption = "Grade";
                        col.Visible = Instance.Parameter == DetailedWorkbenchInfo.Parameters.ItemGradeLevel || Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleGradeLevel;
						col.OptionsColumn.ShowInCustomizationForm = col.Visible;
						if (col.Visible)
							bandFixed.Columns.Add(col);
						break;

                    case DetailedWorkbenchInfo.colGradeDescription:
                        col.Caption = "Grade Desc.";
                        col.Visible = Instance.Parameter == DetailedWorkbenchInfo.Parameters.ItemGradeLevel || Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleGradeLevel;
						col.OptionsColumn.ShowInCustomizationForm = col.Visible;
						if (col.Visible)
							bandFixed.Columns.Add(col);
						break;

                    case DetailedWorkbenchInfo.colStore:
                        col.Caption = "Store";
                        col.Visible = Instance.Parameter == DetailedWorkbenchInfo.Parameters.ItemStoreLevel || Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleStoreLevel;
						col.OptionsColumn.ShowInCustomizationForm = col.Visible;
						if (col.Visible)
							bandFixed.Columns.Add(col);
						break;

                    case DetailedWorkbenchInfo.colStoreName:
                        col.Caption = "Store Name";
                        col.Visible = Instance.Parameter == DetailedWorkbenchInfo.Parameters.ItemStoreLevel || Instance.Parameter == DetailedWorkbenchInfo.Parameters.StyleStoreLevel;
						col.OptionsColumn.ShowInCustomizationForm = col.Visible;
						if (col.Visible)
							bandFixed.Columns.Add(col);
						break;

                    case DetailedWorkbenchInfo.colSalesLW:
						bandLeft.Columns.Add(col);
                        col.Caption = "Sales LW";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colSalesTW:
						bandLeft.Columns.Add(col);
                        col.Caption = "Sales TW";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colSmoothedRateOfSale:
						bandLeft.Columns.Add(col);
                        col.Caption = "Smoothed rate of sale";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N1";					
                        break;

					case DetailedWorkbenchInfo.colNewSmoothedRateOfSale:
						bandLeft.Columns.Add(col);
                        col.Caption = "New smoothed rate of sale";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N1";
                        break;

                    case DetailedWorkbenchInfo.colStoreSOH:
						bandLeft.Columns.Add(col);
                        col.Caption = "Store SOH";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

					case DetailedWorkbenchInfo.colSmoothedStoreCover:
						bandLeft.Columns.Add(col);
						col.Caption = "Smoothed Store Cover";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
						col.DisplayFormat.FormatString = "N1";
						break;
					
					case DetailedWorkbenchInfo.colTotalSmoothedStoreCover:
						bandLeft.Columns.Add(col);
                        col.Caption = "Total Smoothed store cover";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
						col.DisplayFormat.FormatString = "N1";
                        break;

					case DetailedWorkbenchInfo.colOriginalStockRequired:
						bandLeft.Columns.Add(col);
						col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
						col.DisplayFormat.FormatString = "N2";
						break;

					case DetailedWorkbenchInfo.colTotalStockRequired:
						bandLeft.Columns.Add(col);
                        col.Caption = "Total stock required";
						col.AppearanceCell.BackColor = Color.Aquamarine;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colRingFenced:
						bandLeft.Columns.Add(col);
                        col.Caption = "Ring fenced";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
						col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colAllocated:
						bandLeft.Columns.Add(col);
                        col.Caption = "Allocated";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colGiveItBack:
						bandLeft.Columns.Add(col);
                        col.Caption = "Give it back";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        col.ColumnEdit = riYesNo;
                        col.OptionsColumn.AllowEdit = true;
                        break;

                    case DetailedWorkbenchInfo.colShipped:
						bandLeft.Columns.Add(col);
                        col.Caption = "Shipped";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colMinDispQty:
						bandLeft.Columns.Add(col);
                        col.Caption = "Min. Disp. Qty";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colIdealAllocQty:
						bandLeft.Columns.Add(col);
                        col.Caption = "Ideal Alloc. Qty";
						col.AppearanceCell.BackColor = Color.Aquamarine;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colProposedAllocQty:
						bandLeft.Columns.Add(col);
                        col.Caption = "Proposed Alloc. Qty";
						col.AppearanceCell.BackColor = Color.Aquamarine;
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colHideQuantities:
                        bandLeft.Columns.Add(col);
                        col.Caption = "Hide Quantities";
                        col.Visible = false;
                        col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colDCStock:
						bandLeft.Columns.Add(col);
                        col.Caption = "DC Stock";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colEDCAPPStock:
						bandLeft.Columns.Add(col);
                        col.Caption = "DC APP Stock";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colSmoothedEDCCover:
						bandLeft.Columns.Add(col);
                        col.Caption = "Smoothed DC Cover";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colNextOrderDate:
						bandLeft.Columns.Add(col);
                        col.Caption = "Next Order Date";
                        col.DisplayFormat.FormatType = FormatType.DateTime;
                        col.DisplayFormat.FormatString = System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern;                      
                        break;

                    case DetailedWorkbenchInfo.colNextOrderQty:
						bandLeft.Columns.Add(col);
                        col.Caption = "Next Order Qty";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N0";
                        break;

                    case DetailedWorkbenchInfo.colCurPattern:
						bandPattern.Columns.Add(col);
                        col.Caption = "Current";
                        col.ColumnEdit = riPatternId;
                        col.OptionsColumn.ShowInCustomizationForm = true;
                        col.OptionsColumn.AllowEdit = false;
                        break;

                    case DetailedWorkbenchInfo.colNewPattern:
						bandPattern.Columns.Add(col);
                        col.Caption = "New";
                        col.ColumnEdit = riPatternId;
                        col.OptionsColumn.ShowInCustomizationForm = true;
                        col.OptionsColumn.AllowEdit = true;
                        break;

                    case DetailedWorkbenchInfo.colPatternHierarchyLevel:
						bandPattern.Columns.Add(col);
                        col.Caption = "Hierarchy Level";
                        col.ColumnEdit = riHierarchyLevel;
                        col.OptionsColumn.ShowInCustomizationForm = true;
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        break;

                    case DetailedWorkbenchInfo.colPatternThisLevel:
						bandPattern.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
						break;

                    case DetailedWorkbenchInfo.colPatternStatus:
						bandPattern.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colCurUpliftFactor:
						bandUplift.Columns.Add(col);
                        col.Caption = "Current";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N2";
                        break;

                    case DetailedWorkbenchInfo.colNewUpliftFactor:
						bandUplift.Columns.Add(col);
                        col.Caption = "New";
                        col.OptionsColumn.AllowEdit = true;
                        col.ColumnEdit = riUpliftFactor;
                        break;
                    
                    case DetailedWorkbenchInfo.colUpliftHierarchyLevel:
						bandUplift.Columns.Add(col);
                        col.Caption = "Hierarchy Level";
                        col.ColumnEdit = riHierarchyLevel;
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        break;

                    case DetailedWorkbenchInfo.colUpliftThisLevel:
						bandUplift.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colUpliftStatus:
						bandUplift.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colCurCutOff:
						bandCutOff.Columns.Add(col);
                        col.Caption = "Current";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N2";
                        break;

                    case DetailedWorkbenchInfo.colNewCutOff:
						bandCutOff.Columns.Add(col);
                        col.Caption = "New";
                        col.OptionsColumn.AllowEdit = true;
                        col.ColumnEdit = riCutOff;
                        break;

                    case DetailedWorkbenchInfo.colCutOffHierarchyLevel:
						bandCutOff.Columns.Add(col);
                        col.Caption = "Hierarchy Level";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        col.ColumnEdit = riHierarchyLevel;
                        break;

                    case DetailedWorkbenchInfo.colCutOffThisLevel:
						bandCutOff.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colCutOffStatus:
						bandCutOff.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colCurAllocFlag:
						bandAllocation.Columns.Add(col);
                        col.Caption = "Current";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        break;

                    case DetailedWorkbenchInfo.colNewAllocFlag:
						bandAllocation.Columns.Add(col);
                        col.Caption = "New";
                        col.OptionsColumn.AllowEdit = true;
                        col.ColumnEdit = riYesNo;
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        break;

                    case DetailedWorkbenchInfo.colAllocFlagHierarchyLevel:
						bandAllocation.Columns.Add(col);
                        col.Caption = "Hierarchy Level";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        col.ColumnEdit = riHierarchyLevel;
                        break;

                    case DetailedWorkbenchInfo.colAllocFlagThisLevel:
						bandAllocation.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colAllocFlagStatus:
						bandAllocation.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colCurSmoothingFactor:
						bandSmoothing.Columns.Add(col);
                        col.Caption = "Current";
                        col.DisplayFormat.FormatType = FormatType.Numeric;
                        col.DisplayFormat.FormatString = "N2";
                        break;

                    case DetailedWorkbenchInfo.colNewSmoothingFactor:
						bandSmoothing.Columns.Add(col);
                        col.Caption = "New";
                        col.OptionsColumn.AllowEdit = true;
                        col.ColumnEdit = riSmoothingFactor;
                        break;

                    case DetailedWorkbenchInfo.colSmoothingFactorHierarchyLevel:
						bandSmoothing.Columns.Add(col);
                        col.Caption = "Hierarchy Level";
                        col.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        col.ColumnEdit = riHierarchyLevel;
                        break;

                    case DetailedWorkbenchInfo.colSmoothingFactorThisLevel:
						bandSmoothing.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colSmoothingFactorStatus:
						bandSmoothing.Columns.Add(col);
                        col.Visible = false;
						col.OptionsColumn.ShowInCustomizationForm = false;
                        break;

                    case DetailedWorkbenchInfo.colDaysOutOfStock:
						bandRight.Columns.Add(col);
                        col.Caption = "Days out of stock";
                        col.Visible = Instance.Parameter != DetailedWorkbenchInfo.Parameters.StyleLevel;
                        break;

                    case DetailedWorkbenchInfo.colUPC:
						bandRight.Columns.Add(col);
                        col.Caption = "UPC";
                        break;

					case DetailedWorkbenchInfo.colPack:
						bandRight.Columns.Add(col);
						col.Caption = "Pack";
						col.Visible=false;
						col.OptionsColumn.ShowInCustomizationForm = false;
						break;
					
					case DetailedWorkbenchInfo.colAPPItemNo:
						bandRight.Columns.Add(col);
                        col.Caption = "APP Item No.";
                        break;

					case DetailedWorkbenchInfo.colError:
						bandRight.Columns.Add(col);
						col.Caption = "Error";
						col.OptionsColumn.AllowEdit = false;
						break;

                    default:
						if (col.FieldName.EndsWith("ORIGINAL"))
						{
							col.Visible = false;
							col.OptionsColumn.ShowInCustomizationForm = false;
						}
						else
							unknownColumns.Add(col.FieldName);
                        break;
                }
              
				if (col.OptionsColumn.AllowEdit && this.EditMode)
                    col.AppearanceCell.BackColor = Properties.Settings.Default.EditableColumnBackcolor;
            }

			SetupBand(bandAllocation);
			SetupBand(bandCutOff);
			SetupBand(bandPattern);
			SetupBand(bandSmoothing);
			SetupBand(bandUplift); 
			
			if (unknownColumns.Count > 0)
                MessageBox.Show(string.Join("\n", unknownColumns), "Unknown Columns", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                viewLevers.OptionsView.ColumnAutoWidth = false;
				// limit the number of rows used to calculate the best col width otherwise it takes too long
				viewLevers.BestFitMaxRowCount = 25; 
				viewLevers.BestFitColumns();
            }
			gridLevers.ResumeLayout();
        }

		private void SetupBand(GridBand band)
		{
			foreach (BandedGridColumn bandCol in band.Columns)
			{
				bandCol.OptionsColumn.AllowGroup = DefaultBoolean.False;
				bandCol.OptionsColumn.AllowMove = false;
                bandCol.OptionsColumn.AllowShowHide = false;
			}
		}

        private void SwitchLevel(DetailedWorkbenchInfo.Parameters newParameter)
        {
            var switchLevel = false;

            if (IsDirty)
                switch (Question.YesNoCancel("Changes have been made to these items, save them before switching levels", this.Text))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        base.Instance._giveItBackChanged = _giveItBackChanged;
                        switchLevel = base.Instance.ApplyChanges();
                        _giveItBackChanged = false;
                        base.Instance._giveItBackChanged = false;
                        
                        break;

                    case System.Windows.Forms.DialogResult.No:
                        base.Instance.DiscardChanges();
                        switchLevel = true;
                        break;

                    case System.Windows.Forms.DialogResult.Cancel:
                        switchLevel = false;
                        break;
                }
            else
                switchLevel = true;

            if (switchLevel)
            {
				// Get rid of any existing locks
				base.Instance.ReleaseLocks();
				
				// not switching levels, just showing all values for this level as originally selected.
				if (base.Instance.Parameter == newParameter)
				{
					base.Instance.SelectedItem.Clear();
					RefreshGrid(true);
				}
				else
				{
					var curParameter = base.Instance.Parameter;
					if (base.Instance.Parameter.ToString().ToLower().Contains("style") || newParameter.ToString().ToLower().Contains("style"))
						base.Instance.SelectedItem.SetItem(Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colClass)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colVendor)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colStyle)));
					else
						base.Instance.SelectedItem.SetItem(Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colClass)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colVendor)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colStyle)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colColour)),
						Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colSize)));

                    base.Instance.SelectedItem.SetGradeId(null);
                    base.Instance.SelectedItem.SetMarket(null);
                    base.Instance.SelectedItem.SetStoreId(null);

                    var storeId = Convert.ToDecimal(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colStore));

                    switch (curParameter)
                    {
                        case DetailedWorkbenchInfo.Parameters.ItemMarketLevel:
                        case DetailedWorkbenchInfo.Parameters.StyleMarketLevel:
                            if (newParameter == DetailedWorkbenchInfo.Parameters.ItemLevel || newParameter == DetailedWorkbenchInfo.Parameters.StyleLevel)
                                base.Instance.Promotions.Clear();
                            else
                                base.Instance.SelectedItem.SetMarket(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colMarket));
                            break;

                        case DetailedWorkbenchInfo.Parameters.ItemGradeLevel:
                        case DetailedWorkbenchInfo.Parameters.StyleGradeLevel:
                            if (newParameter == DetailedWorkbenchInfo.Parameters.ItemLevel || newParameter == DetailedWorkbenchInfo.Parameters.StyleLevel)
                                base.Instance.Promotions.Clear();
                            else
                                base.Instance.SelectedItem.SetGradeId(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colGrade));
                            break;

                        case DetailedWorkbenchInfo.Parameters.ItemStoreLevel:
                        case DetailedWorkbenchInfo.Parameters.StyleStoreLevel:
                            switch (newParameter)
                            {
                                case DetailedWorkbenchInfo.Parameters.ItemLevel:
                                case DetailedWorkbenchInfo.Parameters.StyleLevel:
                                    base.Instance.Promotions.Clear();
                                    break;

                                case DetailedWorkbenchInfo.Parameters.ItemMarketLevel:
                                case DetailedWorkbenchInfo.Parameters.StyleMarketLevel:
                                    base.Instance.SelectedItem.SetMarket(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colMarket));
                                    break;

                                case DetailedWorkbenchInfo.Parameters.ItemGradeLevel:
                                case DetailedWorkbenchInfo.Parameters.StyleGradeLevel:
                                    base.Instance.SelectedItem.SetGradeId(viewLevers.GetFocusedRowCellDisplayText(DetailedWorkbenchInfo.colGrade));
                                    break;

                                case DetailedWorkbenchInfo.Parameters.ItemStoreLevel:
                                case DetailedWorkbenchInfo.Parameters.StyleStoreLevel:
                                    base.Instance.SelectedItem.SetStoreId(storeId);
                                    break;
                            }
                            break;
                    }

                    base.Instance.PreviousParameter = curParameter;
					base.Instance.Parameter = newParameter;
					RefreshGrid(true, curParameter);
				}
            }
        }

        private void viewLevers_RowCellClick(object sender, RowCellClickEventArgs e)
        {
        }

        private void viewLevers_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            
        }

        private void viewLevers_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            _cellChanging = true;

            if (e.Column.Name == "colGIVEITBACK")
            {
                _giveItBackChanged = true;
            }                
        }

        
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
