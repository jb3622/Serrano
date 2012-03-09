using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Disney.iDash.Shared;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.Controls
{
    public partial class StockItemControl : DevExpress.XtraEditors.XtraUserControl
    {
        private List<string> _validationErrors = new List<string>();

        public event EventHandler EditValueChanged;

        public StockItemControl()
        {
            InitializeComponent();
        }

        public decimal DepartmentId { get; set; }
        public bool ShowZeros { get; set; }

        internal void Clear()
        {
            ItemClass = 0;
            ItemVendor = 0;
            ItemStyle = 0;
            ItemColour = 0;
            ItemSize = 0;
        }

        public decimal? ItemClass
        {
            get
            {
				if (txtItemClass.EditValue == null || txtItemClass.EditValue.ToString() == string.Empty)
                    return null;
                else
					return Convert.ToDecimal(txtItemClass.EditValue);
            }
            set
            {
                txtItemClass.EditValue = (value == 0 && !ShowZeros ? null : value);
            }
        }

        public decimal? ItemVendor
        {
            get
            {
				if (txtItemVendor.EditValue == null || txtItemVendor.EditValue.ToString() == string.Empty)
                    return null;
                else
					return Convert.ToDecimal(txtItemVendor.EditValue);
            }
            set
            {
                txtItemVendor.EditValue = (value == 0 && !ShowZeros ? null : value); 
            }
        }

        public bool ItemStyleVisible
        {
            get { return txtItemStyle.Visible; }
            set 
            { 
                txtItemStyle.Visible = value;
                if (!value)   
                    ItemColourVisible = false;
				MoveClearButton();
           }
        }

        public decimal? ItemStyle
        {
            get
            {
				if (txtItemStyle.EditValue == null || txtItemStyle.EditValue.ToString() == string.Empty)
                    return null;
                else
                    return Convert.ToDecimal(txtItemStyle.EditValue);
            }
            set
            {
                txtItemStyle.EditValue = (value == 0 && !ShowZeros ? null : value);
            }
        }

        public bool ItemColourVisible
        {
            get { return txtItemColour.Visible; }
            set 
            { 
                txtItemColour.Visible = value;
                if (value)
                    ItemStyleVisible = true;
                else
                    ItemSizeVisible = false;
				MoveClearButton();
            }
        }

        public decimal? ItemColour
        {
            get
            {
				if (txtItemColour.EditValue == null || txtItemColour.EditValue.ToString() == string.Empty)
                    return null;
                else
                    return Convert.ToDecimal(txtItemColour.EditValue);
            }
            set
            {
                txtItemColour.EditValue = (value == 0 && !ShowZeros ? null : value);
            }
        }

        public bool ItemSizeVisible
        {
            get { return txtItemSize.Visible; }
            set 
            { 
                txtItemSize.Visible = value;
                if (value)
                    ItemColourVisible = true;
				MoveClearButton();
            }
        }

        public decimal? ItemSize
        {
            get
            {
				if (txtItemSize.EditValue == null || txtItemSize.EditValue.ToString() == string.Empty)
                    return null;
                else
                    return Convert.ToDecimal(txtItemSize.EditValue);
            }
            set
            {
                txtItemSize.EditValue = (value == 0 && !ShowZeros ? null : value);
            }
        }

        public StockItem StockItem
        {
            get
            {
				var stockItem = new StockItem(this.ItemClass, this.ItemVendor, this.ItemStyle, this.ItemColour, this.ItemSize);
				stockItem.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
					{
						ErrorDialog.Show(ex, extraInfo, terminateApplication);
					});

				return stockItem;
            }
        }

        public bool HasValue
        {
            get { return this.ItemClass.HasValue || this.ItemVendor.HasValue || this.ItemStyle.HasValue || this.ItemColour.HasValue || this.ItemSize.HasValue; }
        }

        public IStyleController StyleController
        {
            get { return txtItemClass.StyleController; }
            set 
            { 
                txtItemClass.StyleController = value;
                txtItemColour.StyleController = value;
                txtItemSize.StyleController = value;
                txtItemStyle.StyleController = value;
                txtItemVendor.StyleController = value;
				lookupItemClass.StyleController = value;
				lookupItemVendor.StyleController = value;
            }
        }

        public void RefreshCombos()
        {
			var source = new LookupSource();
			var items = source.GetItems(LookupSource.LookupTypes.ItemClass, "CDPT=" + this.DepartmentId.ToString());
			lookupItemClass.Properties.DataSource = items;
			lookupItemClass.Enabled = items.Count > 0;

			lookupItemVendor.Properties.DataSource = source.GetItems(LookupSource.LookupTypes.ItemVendor, "VENDPT=" + this.DepartmentId.ToString());
			lookupItemVendor.Enabled = false;

			EnableVendorLookup();
        }

        public bool IsValid()
        {
            _validationErrors.Clear();
            if (txtItemClass.EditValue == null && txtItemVendor.EditValue != null)
                _validationErrors.Add("Please enter a class");

            if (txtItemVendor.EditValue == null && !IsObjectNullOrEmpty(txtItemStyle.EditValue))
               _validationErrors.Add("Please enter a vendor");

            if (IsObjectNullOrEmpty(txtItemStyle.EditValue) && !IsObjectNullOrEmpty(txtItemColour.EditValue))
               _validationErrors.Add("Please enter a style");

            if (IsObjectNullOrEmpty(txtItemColour.EditValue) && !IsObjectNullOrEmpty(txtItemSize.EditValue))
                _validationErrors.Add("Please enter a colour");

            return _validationErrors.Count == 0;
        }
        
        public string GetValidationErrors()
        {
            return string.Join("\n", _validationErrors);
        }

        public new string ToString()
        {
            var result = new List<string>();
            AddElement(result, txtItemClass.EditValue, txtItemClass.Properties.MaxLength);
            AddElement(result, txtItemVendor.EditValue, txtItemVendor.Properties.MaxLength);
            AddElement(result, txtItemStyle.EditValue, txtItemStyle.Properties.MaxLength);
            AddElement(result, txtItemColour.EditValue, txtItemColour.Properties.MaxLength);
            AddElement(result, txtItemSize.EditValue, txtItemSize.Properties.MaxLength);
            return string.Join("-", result.ToArray());
        }

		private void MoveClearButton()
		{
			if (this.Visible)
			{
				if (ItemStyleVisible)
					if (ItemColourVisible)
						if (ItemSizeVisible)
							btnClear.Left = 260;
						else
							btnClear.Left = txtItemSize.Left;
					else
						btnClear.Left = txtItemColour.Left;
				else
					btnClear.Left = txtItemStyle.Left;
			}
		}

        private bool IsObjectNullOrEmpty(object value)
        {
            return (value == null || (value.GetType() == typeof(string) && value.ToString() == string.Empty));
        }

        private void AddElement(List<string> elements, object value, int length)
        {
            var result = string.Empty;
            if (value != null && value.ToString() != string.Empty)
                result = value.ToString().PadLeft(length, '0');
            else
                result = result.PadLeft(length, '0');
            elements.Add(result);
        }
		
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var ctrl = (TextEdit)sender;
            if (ctrl.SelectionStart == ctrl.Properties.MaxLength)
                SelectNextControl(ctrl, true, false, false, false);
        }

        private void TextEdit_Enter(object sender, EventArgs e)
        {
            TextEdit textedit = (TextEdit)sender;
            textedit.SelectAll();
        }

        private void ItemControl_Validating(object sender, CancelEventArgs e)
        {
            if (!IsValid())
            {
                MessageBox.Show(this.GetValidationErrors(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Cancel = true;
            }
        }

        private void txtItemClass_EditValueChanged(object sender, EventArgs e)
        {
			if (txtItemClass.EditValue == null || txtItemClass.EditValue.ToString() == string.Empty)
			{
				txtItemVendor.Enabled = false;
				this.Clear();
			}
			else
				txtItemVendor.Enabled = true;

			EnableVendorLookup();

            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

        private void txtItemVendor_EditValueChanged(object sender, EventArgs e)
        {
			txtItemStyle.Enabled = (txtItemVendor.EditValue != null && txtItemVendor.EditValue.ToString() != string.Empty);

            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

        private void txtItemStyle_EditValueChanged(object sender, EventArgs e)
        {
			txtItemColour.Enabled = (txtItemStyle.EditValue != null && txtItemStyle.EditValue.ToString() != string.Empty);
            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

        private void txtItemColour_EditValueChanged(object sender, EventArgs e)
        {
			txtItemSize.Enabled = (txtItemColour.EditValue != null && txtItemColour.EditValue.ToString() != string.Empty);
            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

        private void txtItemSize_EditValueChanged(object sender, EventArgs e)
        {
            if (EditValueChanged != null)
                EditValueChanged(sender, e);
        }

		private void btnClear_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void lookupItemClass_EditValueChanged(object sender, EventArgs e)
		{
			txtItemClass.EditValue = lookupItemClass.EditValue;
		}

		private void lookupItemClass_Leave(object sender, EventArgs e)
		{
			lookupItemClass.ClosePopup();
		}

		private void lookupItemVendor_EditValueChanged(object sender, EventArgs e)
		{
			txtItemVendor.EditValue = lookupItemVendor.EditValue;
		}

		private void lookupItemVendor_Leave(object sender, EventArgs e)
		{
			lookupItemVendor.ClosePopup();
		}

		private void lookupItemClass_Popup(object sender, EventArgs e)
		{
			lookupItemClass.Properties.View.ClearColumnsFilter();
			lookupItemClass.Properties.View.BestFitColumns();
		}

		private void lookupItemVendor_Popup(object sender, EventArgs e)
		{
			lookupItemVendor.Properties.View.ClearColumnsFilter();
			lookupItemVendor.Properties.View.BestFitColumns();
		}

		private void txtItemVendor_EnabledChanged(object sender, EventArgs e)
		{
			EnableVendorLookup();
		}

		private void EnableVendorLookup()
		{
			lookupItemVendor.Enabled = txtItemVendor.Enabled && lookupItemVendor.Properties.DataSource != null && ((List<LookupItem>)lookupItemVendor.Properties.DataSource).Count > 0;
		}
    }
}
