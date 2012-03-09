using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Disney.iDash.Shared
{
	public class ControlStateCollection
	{
		private List<ControlState> _controls = new List<ControlState>();

		public void SaveState(params Control[] controls)
		{		
			_controls = new List<ControlState>();
			foreach (var control in controls)
				_controls.Add(new ControlState(control));
		}

		public void Disable()
		{
			foreach (var control in _controls)
				control.Control.Enabled = false;
		}

		public void Enable()
		{
			foreach (var control in _controls)
				control.Control.Enabled = true;
		}

		public void Hide()
		{
			foreach (var control in _controls)
				control.Control.Visible = false;
		}

		public void Show()
		{
			foreach (var control in _controls)
				control.Control.Visible = true;
		}

		public void RestoreState()
		{
			foreach (var control in _controls)
			{
				control.Control.Visible = control.Visible;
				control.Control.Enabled = control.Enabled;
			}
		}
	}

	public class ControlState
	{
		public Control Control { get; set; }
		public bool Visible { get; set; }
		public bool Enabled { get; set; }

		public ControlState()
		{
		}

		public ControlState(Control control)
		{
			Control = control;
			Visible = control.Visible;
			Enabled = control.Enabled;
		}
	}
}
