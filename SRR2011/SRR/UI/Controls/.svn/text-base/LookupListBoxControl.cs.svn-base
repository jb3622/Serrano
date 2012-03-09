using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.Shared;

namespace Disney.iDash.SRR.Controls
{
    public partial class LookupListBoxControl : DevExpress.XtraEditors.XtraUserControl
    {
        public LookupSource.LookupTypes _lookupType = LookupSource.LookupTypes.None;
        public ExceptionHandler ExceptionHandler = new ExceptionHandler();
        public event DevExpress.XtraEditors.Controls.ItemCheckEventHandler ItemCheck;
        
        private LookupSource _source = new LookupSource();

        public LookupListBoxControl()
        {
            InitializeComponent();
            _source.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
            {
                ExceptionHandler.RaiseException(ex, extraInfo, terminateApplication);
            });

            MaxSelections = 0;
        }

        [Description("Max No. items that can be selected.  Zero = all")]
        public int MaxSelections {get; set;}

        public LookupSource.LookupTypes LookupType
        {
            get { return _lookupType; }
            set { _lookupType = value; }
        }

        public IStyleController StyleController
        {
            get { return lstLookup.StyleController; }
            set {lstLookup.StyleController = value;}
        }

        public new bool Enabled
        {
            get { return lstLookup.Enabled; }
            set 
            { 
                lstLookup.Enabled = value;
                this.TabStop = value;
            }
        }

		public bool RefreshControl()
		{
			var result = false;
			try
			{
				if (_lookupType == LookupSource.LookupTypes.None)
				{
					lstLookup.DataSource = null;
					lstLookup.ValueMember = string.Empty;
					lstLookup.DisplayMember = string.Empty;
				}
				else if (!this.DesignMode)
				{
					lstLookup.ValueMember = "Id";
					lstLookup.DisplayMember = "Description";
					lstLookup.DataSource = _source.GetItems(_lookupType);
					result = true;
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.RaiseException(ex, "RefreshControl");
			}
			return result;
		}

		public void UnCheckAll()
        {
            lstLookup.UnCheckAll();
        }

        public int CheckedItemsCount()
        {
            return lstLookup.CheckedItems.Count;
        }

        public int SelectedItemCount()
        {
            return lstLookup.SelectedItems.Count;
        }

        public int ItemCount()
        {
            return lstLookup.ItemCount;
        }
        
        public BaseCheckedListBoxControl.CheckedItemCollection CheckItems
        {
            get { return lstLookup.CheckedItems; }
        }

        public BaseCheckedListBoxControl.CheckedIndexCollection CheckedIndexes
        {
            get { return lstLookup.CheckedIndices; }
        }

        public int SelectedIndex
        {
            get { return lstLookup.SelectedIndex; }
            set { lstLookup.SelectedIndex = value; }
        }

        public BaseListBoxControl.SelectedIndexCollection SelectedIndicies
        {
            get { return lstLookup.SelectedIndices; }
        }

        public object SelectedItem
        {
            get { return lstLookup.SelectedItem; }
            set { lstLookup.SelectedItem = value; }
        }

        public BaseListBoxControl.SelectedItemCollection SelectedItems
        {
            get { return lstLookup.SelectedItems; }
        }

        public object SelectedValue
        {
            get { return lstLookup.SelectedValue; }
            set 
            { 
                if (lstLookup.ValueMember != string.Empty)
                    lstLookup.SelectedValue = value; 
            }
        }

		public void SetCheckedItems(List<LookupItem> items)
        {
            lstLookup.UnCheckAll();
            foreach (var item in items)
            {
                var index = lstLookup.FindItem(item);
                if (index >= 0)
                    lstLookup.SetItemChecked(index, true);
            }
        }

		public List<LookupItem> GetCheckedItems()
        {
			var items = new List<LookupItem>();
			foreach (LookupItem item in lstLookup.CheckedItems)
                items.Add(item);

            return items;
        }

		public List<string> GetCheckedIds()
		{
			var result = new List<string>();
			foreach (LookupItem item in lstLookup.CheckedItems)			
				result.Add(item.Id.ToString());
			return result;
		}

        private void lstLookup_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            if (ItemCheck != null)
                ItemCheck(sender, e);
        }

        private void lstLookup_ItemChecking(object sender, DevExpress.XtraEditors.Controls.ItemCheckingEventArgs e)
        {
            if (MaxSelections > 0 && lstLookup.CheckedItems.Count == MaxSelections && e.NewValue == CheckState.Checked)
                e.Cancel = true;
        }

        private void mnuClear_Click(object sender, EventArgs e)
        {
            lstLookup.UnCheckAll();
            lstLookup.UnSelectAll();
        }
    }
}
