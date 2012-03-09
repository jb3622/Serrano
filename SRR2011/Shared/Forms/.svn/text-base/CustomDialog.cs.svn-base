using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Disney.iDash.Shared.Forms
{
	internal partial class CustomDialog : Form
	{
		public CustomDialog()
		{
			InitializeComponent();
		}

		private string _selectedOption = string.Empty;

		internal string ShowForm(string prompt, string caption, List<string> options)
		{
			this.Text = caption;

			if (options == null || options.Count == 0 || options.Count > 5)
			{
				lblMessage.Text = "Invalid number of button options. Range is 1 to 5";
				ActivateButton(button3, "&Ok");
			}
			else
			{
				lblMessage.Text = prompt;
				switch (options.Count)
				{
					case 1:
						ActivateButton(button3, options[0]);
						break;
					case 2:
						ActivateButton(button2, options[0]);
						ActivateButton(button4, options[1]);
						break;
					case 3:
						ActivateButton(button2, options[0]);
						ActivateButton(button3, options[1]);
						ActivateButton(button4, options[2]);
						break;

					case 4:
						ActivateButton(button1, options[0]);
						ActivateButton(button2, options[1]);
						ActivateButton(button4, options[2]);
						ActivateButton(button5, options[3]);
						break;

					case 5:
						ActivateButton(button1, options[0]);
						ActivateButton(button2, options[1]);
						ActivateButton(button3, options[2]);
						ActivateButton(button4, options[3]);
						ActivateButton(button5, options[4]);
						break;

				}
			}
			this.ShowDialog();
			return _selectedOption;
		}

		private void ActivateButton(Button button, string caption)
		{
			button.Text = caption;
			button.Visible = true;
			button.Click += ((sender, e) =>
				{
					_selectedOption = button.Text.Replace("&", "");
					this.Hide();
				});
		}
	}
}
