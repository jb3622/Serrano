using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Disney.iDash.Shared.Controls
{
	public partial class ListEditor : DevExpress.XtraEditors.XtraUserControl
	{

		public delegate bool OnAddItemHandler(object dataSource, string item);
		public delegate bool OnDelItemHandler(object dataSource, int index);

		public event OnAddItemHandler OnAddItem;
		public event OnDelItemHandler OnDelItem;

		public ListEditor()
		{
			InitializeComponent();
		}

		public object DataSource
		{
			get { return lstItems.DataSource; }
			set { lstItems.DataSource = value; }
		}

		public string ValueMember
		{
			get { return lstItems.ValueMember; }
			set { lstItems.ValueMember = value; }
		}

		public string DisplayMember
		{
			get { return lstItems.DisplayMember; }
			set { lstItems.DisplayMember = value; }
		}

		public IStyleController StyleControler
		{
			get { return lstItems.StyleController; }
			set 
			{ 
				lstItems.StyleController = value;
				txtItem.StyleController = value;
			}
		}

		private void txtItem_EditValueChanged(object sender, EventArgs e)
		{
			btnDel.Enabled = false;
			btnAdd.Enabled = txtItem.Text.Length > 0;
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			if (OnAddItem != null)
				if (OnAddItem(lstItems.DataSource, txtItem.Text))
				{
					txtItem.Text = string.Empty;
					lstItems.Refresh();
				}
		}

		private void btnDel_Click(object sender, EventArgs e)
		{
			if (OnDelItem != null)
			{
				var lastIndex = lstItems.SelectedIndex;
				if (OnDelItem(lstItems.DataSource, lstItems.SelectedIndex))
				{
					lstItems.Refresh();
					lstItems.SelectedIndex = --lastIndex;
				}
			}
		}

		private void lstItems_Click(object sender, EventArgs e)
		{
			btnDel.Enabled = true;
		}

		private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnDel.Enabled = lstItems.SelectedIndex != -1;
		}
	}
}
