using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Disney.iDash.SRR.BusinessLayer;

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
	public partial class GiveItBackConfirmationDialog : Disney.iDash.SRR.UI.Forms.Common.BaseForm
	{
		public GiveItBackConfirmationDialog()
		{
			InitializeComponent();
		}

		public void ShowForm(GiveItBackCollection giveItBackCollection)
		{
			if (giveItBackCollection != null && giveItBackCollection.Count() > 0)
			{
				gridControl1.DataSource = giveItBackCollection.ToList();

				switch (GiveItBackCollection.GiveItBackMethod)
				{
					case GiveItBackCollection.GiveItBackMethods.None:
						bandQuantities.Caption = "GiveItBackMethod Not Set - Contact Support";
						break;

					case GiveItBackCollection.GiveItBackMethods.RingFenced:
						bandQuantities.Caption = "Ring Fenced Quantities";
						break;

					case GiveItBackCollection.GiveItBackMethods.StoreSOH:
						bandQuantities.Caption = "Pick Face SOH Quantities";
						break;
				}

				this.ShowDialog();
			}
			else
				this.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
