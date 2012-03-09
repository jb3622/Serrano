/*
 * Author:  Marc Levine
 * Project: SRR
 * Date:    From March 2011
 * 
 * Description
 * Misc static functions & wrappers.
 *  
*/
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Disney.iDash.Shared
{
    // Wrap up the error dialog box
    public static class ErrorDialog
    {
        public static void Show(Exception Ex, bool terminateApplication = false)
        {
            var eDialog = new Forms.ErrorDialogForm();
            eDialog.ShowError(Ex);
            if (terminateApplication)
                Environment.Exit(-1);
        }

        public static void Show(Exception Ex, string Caption, bool terminateApplication = false)
        {
            var eDialog = new Forms.ErrorDialogForm();
            eDialog.ShowError(Ex, Caption);
            if (terminateApplication)
                Environment.Exit(-1);
        }

        public static void Show(string ErrorMessage, bool terminateApplication = false)
        {
            var eDialog = new Forms.ErrorDialogForm();
            eDialog.ShowError(ErrorMessage);
            if (terminateApplication)
                Environment.Exit(-1);
        }

        public static void Show(string ErrorMessage, string Caption, bool terminateApplication = false)
        {
            var eDialog = new Forms.ErrorDialogForm();
            eDialog.ShowError(ErrorMessage, Caption);
            if (terminateApplication)
                Environment.Exit(-1);
        }

        public static string GetErrorDetails(Exception ex)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("\r\nThis application has encountered an error, the details are as follows:\r\n\r\n{0}\r\n", ex.ToString());
            sb.Append("\r\nFurther Information:\r\n\r\n");
            sb.AppendFormat("Date/Time: {0:dd/MM/yyyy HH:mm:ss}\r\n", DateTime.Now);
            sb.AppendFormat("Version: {0}\r\n", Application.ProductVersion);
            sb.AppendFormat("Machine Name: {0}\r\n", Environment.MachineName);
            sb.AppendFormat("OS: {0}\r\n", Environment.OSVersion);
            sb.AppendFormat("User: {0}\r\n", Environment.UserName);
            return sb.ToString();
        }

        public static void ShowAlert(string message, string caption, ExceptionHandler.AlertType alertType)
        {
            switch (alertType)
            {
                case Shared.ExceptionHandler.AlertType.Error:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case Shared.ExceptionHandler.AlertType.Information:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case Shared.ExceptionHandler.AlertType.Warning:
                    MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }

    public static class Question
    {
        public static bool YesNo(string Prompt)
        {
            return YesNo(Prompt, "Confirm");
        }

        public static bool YesNo(string Prompt, string Caption)
        {
			if (!Prompt.EndsWith("?"))
				Prompt += "?";

            var result = MessageBox.Show(Prompt, Caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2) == DialogResult.Yes;
            Application.DoEvents();
            return result;
        }

        public static DialogResult YesNoCancel(string Prompt)
        {
            return YesNoCancel(Prompt, "Confirm");
        }

        public static DialogResult YesNoCancel(string Prompt, string Caption)
        {
			if (!Prompt.EndsWith("?"))
				Prompt += "?";

            var result = MessageBox.Show(Prompt, Caption,
                                         MessageBoxButtons.YesNoCancel,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2);

            Application.DoEvents();
            return result;
        }

		public static string Custom(string prompt, string caption, List<string> options)
		{
			var frm = new Forms.CustomDialog();
			return frm.ShowForm(prompt, caption, options);
		}
    }

    public static class FormUtils
    {
		/// <summary>
		/// Restore a form location using the values from the registry
		/// </summary>
		/// <param name="form"></param>
		/// <param name="useCaption"></param>
        public static void RestoreFormLocation(Form form, bool useCaption = false)
        {
			var keyName = "Forms\\" + (useCaption ? form.Text : form.Name) + "\\";

            form.SuspendLayout();
            form.Location = new System.Drawing.Point(Math.Max(0, RegUtils.GetCustomSetting(keyName + "Left", form.Location.X)), Math.Max(0, RegUtils.GetCustomSetting(keyName + "Top", form.Location.Y)));

			if (form.FormBorderStyle == FormBorderStyle.Sizable || form.FormBorderStyle == FormBorderStyle.SizableToolWindow)
				form.Size = new System.Drawing.Size(Math.Max(0, RegUtils.GetCustomSetting(keyName + "Width", form.Size.Width)), Math.Max(0, RegUtils.GetCustomSetting(keyName + "Height", form.Size.Height)));
                
            form.ResumeLayout();
        }

		public static void SaveFormLocation(Form form, bool useCaption = false)
        {
			if (form.WindowState != FormWindowState.Minimized)
			{
				var keyName = "Forms\\" + (useCaption ? form.Text : form.Name) + "\\";
                RegUtils.SetCustomSetting(keyName + "Top", form.Location.Y);
                RegUtils.SetCustomSetting(keyName + "Left", form.Location.X);
                RegUtils.SetCustomSetting(keyName + "Width", form.Size.Width);
                RegUtils.SetCustomSetting(keyName + "Height", form.Size.Height);
			}
        }

		/// <summary>
		/// Select an open form or open if closed.
		/// </summary>
		/// <param name="form"></param>
		/// <param name="useCaption"></param>
		/// <returns></returns>
        public static Form SelectInstance(Form form, bool useCaption = false)
        {
            var result = form;
            foreach (Form frm in Application.OpenForms)
                if ((!useCaption && frm.Name == form.Name) || (useCaption && frm.Text == form.Text))
                {
                    result = frm;
                    frm.Activate();
					form.Close();
					form = null;
					break;
                }
                
            return result;
        }

        /// <summary>
        /// Close all open forms, optionally limiting to those where Form.Tag = tag.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="tag"></param>
		public static void CloseAllForms(Form form, string tag = "", bool forceClose = false)
		{
			var openForms = new List<Form>();

			foreach (Form frm in Application.OpenForms)
				if (frm != form && (tag == string.Empty || (frm.Tag != null && frm.Tag.ToString() == tag)))
					openForms.Add(frm);
            
            foreach (var frm in openForms)
                if (frm != null)
                {
                    if (forceClose)
                        AddTag(frm, "ForceClose");
                    frm.Close();
                }
		}

		public static Form FindForm(string caption)
		{
			Form result = null;
			foreach (Form frm in Application.OpenForms)
				if (frm.Text.ToLower() == caption.ToLower())
				{
					result = frm;
					break;
				}
			return result;
		}

		public static Form FindMdiParent()
		{
			Form result = null;
			foreach (Form frm in Application.OpenForms)
				if (frm.IsMdiContainer)
				{
					result = frm;
					break;
				}
			return result;
		}

        public static void AddTag(Form form, string tag)
        {
            tag = tag.ToLower();

            if (form.Tag == null)
                form.Tag = tag;
            else if (!form.Tag.ToString().Contains(tag))
                form.Tag += "|" + tag;
        }

        public static void RemoveTag(Form form, string tag)
        {
            tag = tag.ToLower();

            if (form.Tag != null && form.Tag.ToString().Contains(tag))
            {
                form.Tag = form.Tag.ToString().Replace(tag, string.Empty);
                form.Tag = form.Tag.ToString().Replace("||", "|");
            }
        }

        public static bool TagContains(Form form, string tag)
        {
            tag = tag.ToLower();
            return form.Tag != null && form.Tag.ToString().Contains(tag);
        }
	}

    public static class RegUtils
    {
        public static KeyType RegBasePath(string KeyName)
        {
            var result = new KeyType();
            var assemblyInfo = new AssemblyInfo();
            result.KeyPath = "Software\\" + assemblyInfo.AssemblyCompany + "\\" + assemblyInfo.AssemblyProduct + "\\Settings\\";

            int slashpos = KeyName.LastIndexOf('\\');

            if (slashpos == -1)
                result.KeyName = KeyName;
            else
            {
                result.KeyPath += KeyName.Substring(0, slashpos);
                result.KeyName = KeyName.Substring(slashpos + 1);
            }

            return result;
        }

		public static bool PathExists(string KeyName)
		{
			return PathExists( RegBasePath(KeyName));
		}

		public static bool PathExists(KeyType kt)
		{
			RegistryKey regkey = Registry.CurrentUser.OpenSubKey(kt.KeyPath, true);
			return regkey != null;
		}

        public static void SetCustomSetting<T>(string KeyName, T value)
        {
            KeyType kt = RegBasePath(KeyName);
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(kt.KeyPath, true);
            if (regkey == null)
                regkey = Registry.CurrentUser.CreateSubKey(kt.KeyPath);
            regkey.SetValue(kt.KeyName, value);
        }

        public static T GetCustomSetting<T>(string KeyName, T DefaultValue)
        {
            T result = DefaultValue;
            KeyType kt = RegBasePath(KeyName);
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(kt.KeyPath, true);
            if (regkey != null && regkey.GetValue(kt.KeyName) != null)
				try
				{
					if (DefaultValue.GetType() == typeof(bool))
						result = (T) ((object) (regkey.GetValue(kt.KeyName).ToString().ToLower() == "true"));
					else
						result = (T)regkey.GetValue(kt.KeyName);
				}
				catch
                {
					result = DefaultValue;
				}
            return result;
        }

        public static void DeleteSetting(string KeyName)
        {
            KeyType kt = RegBasePath(KeyName);
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(kt.KeyPath, true);
            if (regkey != null && regkey.OpenSubKey(kt.KeyName) != null)
                regkey.DeleteSubKeyTree(kt.KeyName);
        }

        #region Nested type: KeyType

        public struct KeyType
        {
            public string KeyName;
            public string KeyPath;
            public override string ToString()
            {
                return KeyPath + "\\" + KeyName;
            }
        }

        #endregion

   }

	public static class MiscUtils
	{
		public static bool IsEmail(string emailAddress)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
				  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
		
			Regex re = new Regex(strRegex);
			
			if (string.IsNullOrEmpty(emailAddress) || re.IsMatch(emailAddress))
				return (true);
			else
				return (false);
		}
	}
}