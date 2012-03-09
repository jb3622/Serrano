using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    public partial class DeptStoreGradeForm : Disney.iDash.SRR.UI.Forms.Common.BaseDataForm
    {

        private StoreInfo _storeInfo = new StoreInfo();
        private Mover _mover = new Mover();

        #region Public methods
        //-----------------------------------------------------------------------------------------
        public DeptStoreGradeForm()
        {
            InitializeComponent();
        }

		public void Setup()
		{
			viewDeptGradeStores.Appearance.EvenRow.BackColor = Properties.Settings.Default.EvenRowBackColor;
			viewDeptGradeStores.Appearance.OddRow.BackColor = Properties.Settings.Default.OddRowBackColor;
			viewDeptGradeStores.Appearance.FocusedRow.BackColor = Properties.Settings.Default.FocusedRowBackColor;

			_storeInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
			{
				ErrorDialog.Show(ex, extraInfo, terminateApplication);
			});

			_storeInfo.ChangedEvent += ((sender, e) =>
			{
				base.FormState = FormStates.Dirty;
			});

			_storeInfo.ProgressEvent += ((message, percentageComplete)=>
			{
				base.UpdateProgress(message, percentageComplete);
			});
		}
		
		public void ShowForm()
        {
            this.MdiParent = FormUtils.FindMdiParent();
            this.Show();
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private event handlers
        //-----------------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
			if (Save())
				this.Close();
        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeList1_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            RefreshGrades(e.Node);
        }

        private void treeDeptGrades_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
			if (e.Node != null && e.Node.ParentNode != null)
				RefreshGrid(Convert.ToDecimal(e.Node.ParentNode.GetValue(colId)), e.Node.GetValue(colId).ToString());
			else
				ClearGrid();
        }

        private void viewDeptGradeStores_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
			btnCut.Enabled = viewDeptGradeStores.SelectedRowsCount > 0;
        }

        private void mnuCut_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void mnuPaste_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            CutSelection();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            PasteSelection();
        }

        void CutSelection()
        {
            _mover.Cut(viewDeptGradeStores, treeDeptGrades, colId);
        }

        void PasteSelection()
        {
			if (_mover.Paste(viewDeptGradeStores, treeDeptGrades, colId, _storeInfo))
			{
				RefreshGrid(Convert.ToDecimal(treeDeptGrades.FocusedNode.ParentNode.GetValue(colId)), treeDeptGrades.FocusedNode.GetValue(colId).ToString());
				btnCut.Enabled = false;
				btnPaste.Enabled = false;
				base.FormState = FormStates.Dirty;
			}
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Overrides
        //-----------------------------------------------------------------------------------------
        internal override void Clear()
        {
            RefreshDepartments(null);
        }

        internal override void RefreshButtons()
        {
            btnSave.Enabled = base.FormState == FormStates.Dirty;
        }

        internal override bool Save()
        {
            base.ShowSavingMessage();
			var saved = false;

			saved = _storeInfo.SaveStoreGrades() && _storeInfo.SaveStoreDeptGrades();

            if (saved)
                base.FormState = FormStates.Idle;

            base.ClearStatusMessage();

            return saved;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region Private methods
        //-----------------------------------------------------------------------------------------
        private void RefreshDepartments(TreeListNode parentNode)
        {
            treeDeptGrades.BeginUnboundLoad();
            TreeListNode node;
            if (parentNode == null)
            {
                treeDeptGrades.Nodes.Clear();
                _storeInfo.Clear();
                treeDeptGrades.FocusedNode = null;
                RefreshGrid(-1, string.Empty);
            }
            foreach (var item in _storeInfo.GetDepartments())
            {
                node = treeDeptGrades.AppendNode(new object[] { item.Id, item.Description, item.Tag }, parentNode);
                node.HasChildren = true;
            }

            treeDeptGrades.EndUnboundLoad();
        }

		private void SetupStoreGradeList()
		{
			var lookup = new LookupSource();
			bsStoreGrades.DataSource = _storeInfo.GetEmptyStoreGradeList();
	
			riStoreLookup.DataSource = lookup.GetItems(LookupSource.LookupTypes.Stores);
			riGradeLookup.DataSource = lookup.GetItems(LookupSource.LookupTypes.StoreGrades);

			_storeInfo.GetGrades();
		}

        private void RefreshGrades(TreeListNode parentNode)
        {
            if (parentNode.Nodes.Count == 0)
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                treeDeptGrades.BeginUnboundLoad();
                TreeListNode node;

                foreach (var item in _storeInfo.GetGrades())
                {
                    node = treeDeptGrades.AppendNode(new object[] { item.Id, item.Description, item.Tag }, parentNode);
                    node.HasChildren = false;
                }

                treeDeptGrades.EndUnboundLoad();
                this.Cursor = Cursors.Default;
            }
        }

		private void ClearGrid()
		{
			gridDeptGradeStores.DataSource = null;
			gridDeptGradeStores.Visible = false;
			btnCut.Enabled = false;
			btnPaste.Enabled = false;
		}

        private void RefreshGrid(decimal deptId, string gradeId)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            viewDeptGradeStores.ClearColumnsFilter();
            viewDeptGradeStores.ClearGrouping();
            viewDeptGradeStores.ClearSorting();
            if (deptId == -1 || gradeId == string.Empty)
				ClearGrid();
			else
			{
                viewDeptGradeStores.ViewCaption = "Dept: " + treeDeptGrades.FocusedNode.ParentNode.GetDisplayText(colDepartments) + " \\ Grade: " + treeDeptGrades.FocusedNode.GetDisplayText(colDepartments);
				gridDeptGradeStores.Visible = true;
				gridDeptGradeStores.DataSource = null;
				gridDeptGradeStores.DataSource = _storeInfo.GetStores(deptId, gradeId);

				btnCut.Enabled = viewDeptGradeStores.RowCount > 0;
				btnPaste.Enabled = _mover.CanPaste(treeDeptGrades, colId);
			}
            this.Cursor = Cursors.Default;
        }

		private void viewStoreGrades_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			var item = (StoreGradeItem)e.Row;
			if (item.GradeId == null)
			{
				e.ErrorText = "You must select a store grade";
				e.Valid = false;
			}

			if (item.StoreId == 0)
			{
				e.ErrorText = "You must select a store";
				e.Valid = false;
			}
		}

		private void DeptStoreGradeForm_Shown(object sender, EventArgs e)
		{
			Application.DoEvents();
			RefreshDepartments(null);
			SetupStoreGradeList();
		}

		private void gridContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			mnuCut.Enabled = btnCut.Enabled;
			mnuPaste.Enabled = btnPaste.Enabled;
		}
        //-----------------------------------------------------------------------------------------
        #endregion
    }

    internal class Mover
    {
        private List<DeptGradeStoreItem> _items = new List<DeptGradeStoreItem>();
        private decimal _sourceDeptId = 0;
        private string _sourceGradeId = string.Empty;

        internal bool Cut(GridView gridView, TreeList treeList, object column)
        {
			if (treeList.FocusedNode.ParentNode == null)
				return false;
			{
				var rowHandles = gridView.GetSelectedRows();
				_sourceDeptId = Convert.ToDecimal(treeList.FocusedNode.ParentNode.GetValue(column));
				_sourceGradeId = treeList.FocusedNode.GetValue(column).ToString();

				_items.Clear();
				foreach (var rowHandle in rowHandles)
				{
					var sourceRow = (DeptGradeStoreItem)gridView.GetRow(rowHandle);
					_items.Add(sourceRow);
				}
				return true;
			}
        }

        internal bool Paste(GridView gridView, TreeList treeList, object column, StoreInfo storeInfo)
		{
			if (treeList.FocusedNode.ParentNode == null)
				return false;
			else
			{
				storeInfo.PasteStoreDeptGrades(_items, Convert.ToDecimal(treeList.FocusedNode.ParentNode.GetValue(column)), treeList.FocusedNode.GetValue(column).ToString());
				_items.Clear();
				return true;
			}
        }

        // Can only paste if we have selected a location within the same department.
        internal bool CanPaste(TreeList treeList, object column)
        {
            return (treeList.FocusedNode != null && 
                treeList.FocusedNode.ParentNode != null && 
                _items.Count> 0 &&
                _sourceDeptId == Convert.ToDecimal(treeList.FocusedNode.ParentNode.GetValue(column)) &&
                    _sourceGradeId != treeList.FocusedNode.GetValue(column).ToString());        
    }
}
}
