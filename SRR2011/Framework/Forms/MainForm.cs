﻿using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using Disney.iDash.BaseClasses.Forms;
using Disney.iDash.SRR.BusinessLayer;
using Disney.iDash.LocalData;
using Disney.iDash.Shared;
using System.ComponentModel;
using System.Diagnostics;
using Disney.iDash.DataLayer;

namespace Disney.iDash.Framework.Forms
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        private SysInfo _sysInfo = new SysInfo();
        private List<string> _systemTags = new List<string>();

        #region Public methods and functions
        //------------------------------------------------------------------------------------------
        public MainForm()
        {
            InitializeComponent();
            _sysInfo.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo, terminateApplication);
                });
            Exit = false;
        }

        public bool Setup()
        {
            var result = false;

            SetupStatusBar();
            SetupRibbon();
            SetupSkinGallery();
            SetSkinColour();
            LookupSource.ClearCache();

            if (SetupPermissions())
                result = CheckSystemAvailability();
            else
            {
                Visible = false;
                MessageBox.Show("There are no options available for you to use in this application", "Contact Support", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }

            return result;
        }

        /// <summary>
        /// Set demon interval to a value between 5 and 60 seconds.  
        /// Values outside this range will default to 5.
        /// </summary>
        public int DemonInterval
        {
            get { return Demon.Interval / 1000; }
            set
            {
                if (value < 25 || value > 120)
                    Demon.Interval = 45000;
                else
                    Demon.Interval = value * 1000;
            }
        }

        public bool Exit
        {
            get;
            private set;
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region Private methods and functions
        //------------------------------------------------------------------------------------------
        private void ClearStatusMessage()
        {
            StatusMessage.Caption = string.Empty;
            StatusMessage.Visibility = BarItemVisibility.Never;
        }

        private void UpdateStatusMessage(string message, bool isWarning = false)
        {
            StatusMessage.Caption = message;
            StatusMessage.Visibility = BarItemVisibility.Always;
            StatusMessage.Appearance.ForeColor = (isWarning ? Color.Red : Color.Black);
            Application.DoEvents();
        }

        private void SetupStatusBar()
        {
            var assemblyInfo = new Disney.iDash.Shared.AssemblyInfo();

            this.StatusUsername.Caption = Session.User.DisplayName;
            this.StatusEnvironment.Caption = Session.Environment.EnvironmentName;
            this.StatusDomain.Caption = Session.Environment.Domain;

            if (ApplicationDeployment.IsNetworkDeployed)
                this.StatusVersion.Caption = "v" + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4);
            else
                this.StatusVersion.Caption = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString();
            this.Text = assemblyInfo.AssemblyProduct;
        }

        private void SetupRibbon()
        {
            var di = new DirectoryInfo(Application.StartupPath);
            var menus = new List<AddinMenu>();

            foreach (FileInfo fi in di.GetFiles("Disney.*.dll"))
            {
                try
                {
                    var dll = Assembly.LoadFile(fi.FullName);
                    Type[] types = dll.GetExportedTypes();
                    Type typeFound = null;

                    foreach (Type type in types)
                        if (type.BaseType != null && type.BaseType == typeof(AddinMenu))
                        {
                            typeFound = type;
                            break;
                        }

                    if (typeFound != null && dll.GetType(typeFound.FullName).GetMethod("InstallRibbon").Name != null)
                    {
                        var addinMenu = Activator.CreateInstance(typeFound) as AddinMenu;

                        var qry1 = from apps in Session.User.vApplications
                                   select apps;
                        var data = qry1.ToList<eApplication>();
                        for (int j = 0; j < data.Count(); j++)
                        {
                            if (String.Equals(addinMenu.Tag, data[j].ApplicationName))
                            {
                                menus.Add(addinMenu);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    ErrorDialog.Show(ex, fi.FullName);
                }
            }

            IOrderedEnumerable<AddinMenu> qry = from menu in menus orderby menu.MenuOrder select menu;
            foreach (AddinMenu menu in qry)
            {
                if (this.IsMdiContainer) 
                    menu.MdiParent = this;
                               
                _systemTags = menu.InstallRibbon(this.MasterRibbon);
            }
            if (this.MasterRibbon.Pages.Count > 0)
                this.MasterRibbon.SelectedPage = this.MasterRibbon.Pages[0];


        }

        private void RemoveRibbonItems()
        {
            while (MasterRibbon.Pages.Count > 0)
                MasterRibbon.Pages.RemoveAt(0);

        }

        private bool SetupPermissions()
        {
            var visiblepages = MasterRibbon.Pages.Count;
            var menuCaption = string.Empty;
            var menuControl = new MenuControl();

            // Only enable the developers menu tab when running in the debugger.
            rpgDeveloper.Visible = System.Diagnostics.Debugger.IsAttached;

            menuControl.ExceptionHandler.ExceptionEvent += ((ex, extraInfo, terminateApplication) =>
                {
                    ErrorDialog.Show(ex, extraInfo);
                });

            
            if (menuControl.Refresh(Session.User.NetworkId))
            {
                foreach (RibbonPage page in MasterRibbon.Pages)
                    if (page != rpOptions)
                    {
                        int visiblegroups = page.Groups.Count;

                        foreach (RibbonPageGroup group in page.Groups)
                        {
                            int visiblebars = group.ItemLinks.Count;
                            foreach (BarItemLink bar in group.ItemLinks)
                            {
                                if (bar.Item.Tag != null && bar.Item.Tag.ToString() != string.Empty)
                                    menuCaption = bar.Item.Tag.ToString();
                                else
                                    menuCaption = bar.Caption;

                                // will need to determine which application the tab is on.
                                // currently defaulted to 1.
                                if (!menuControl.IsAuthorised(1, page.Text, menuCaption))
                                {
                                   bar.Visible = false;
                                   visiblebars--;
                                }
                            }

                            // invisible bars must be removed from the menu.
                            var i = 0;
                            while (i < group.ItemLinks.Count)
                            {
                                if (!group.ItemLinks[i].Visible)
                                    group.ItemLinks.RemoveAt(i);
                                else
                                    i++;
                            }

                            // No bars visible then hide the group
                            if (visiblebars == 0)
                            {
                                group.Visible = false;
                                visiblegroups--;
                            }
                        }
                        // no groups visible then hide the page
                        if (visiblegroups == 0)
                        {
                            page.Visible = false;
                            visiblepages--;
                        }
                    }
            }
            else
                visiblepages = 0;

            // no pages visible, then no access to the application
            return visiblepages > 0;
        }

        void SetSkinColour()
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Session.SkinColor);
        }

        /// <summary>
        /// Setup the colour selection gallery
        /// </summary>
        void SetupSkinGallery()
        {
            SimpleButton imageButton = new SimpleButton();
            foreach (SkinContainer cnt in SkinManager.Default.Skins)
            {
                imageButton.LookAndFeel.SetSkinStyle(cnt.SkinName);
                GalleryItem gItem = new GalleryItem();

                int groupIndex = 1;
                if (cnt.SkinName.IndexOf("Office") > -1)
                    groupIndex = 0;

                galleryColours.Gallery.Groups[groupIndex].Items.Add(gItem);
                gItem.Caption = cnt.SkinName;

                gItem.Image = GetSkinImage(imageButton, 32, 17, 2);
                gItem.HoverImage = GetSkinImage(imageButton, 70, 36, 5);
                gItem.Caption = cnt.SkinName;
                gItem.Hint = cnt.SkinName;
                galleryColours.Gallery.Groups[1].Visible = true;
            }
        }

        /// <summary>
        /// Return a button image
        /// </summary>
        /// <param name="button"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="indent"></param>
        /// <returns></returns>
        Bitmap GetSkinImage(SimpleButton button, int width, int height, int indent)
        {
            Bitmap image = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(image))
            {
                StyleObjectInfoArgs info = new StyleObjectInfoArgs(new GraphicsCache(g));
                info.Bounds = new Rectangle(0, 0, width, height);
                button.LookAndFeel.Painter.GroupPanel.DrawObject(info);
                button.LookAndFeel.Painter.Border.DrawObject(info);
                info.Bounds = new Rectangle(indent, indent, width - indent * 2, height - indent * 2);
                button.LookAndFeel.Painter.Button.DrawObject(info);
            }
            return image;
        }

        private bool CheckSystemAvailability()
        {
            var result = true;

            if (_sysInfo.IsSystemAvailable())
                foreach (var tag in _systemTags)
                {
                    var available = _sysInfo.IsSystemAvailable(tag);
                    if (available)
                    {
                        if (!MasterRibbon.Pages[tag].Visible)
                            MasterRibbon.Pages[tag].Visible = true;
                    }
                    else
                    {

                        FormUtils.CloseAllForms(this, tag, true);
                        MasterRibbon.Pages[tag].Visible = false;
                    }
                }
            else
            {
                UpdateStatusMessage("System is unavailable and will now terminate", true);
                result = false;
            }

            return result;
        }

        private AlertControl _alert = new AlertControl();
        private void ShowSystemMessages()
        {
            var messages = _sysInfo.GetMessages(_systemTags);
            if (messages != null)
            {
                foreach (var message in messages)
                {
                    var IsDisplayed = false;

                    foreach (var a in _alert.AlertFormList)
                        if (a.Info.Text == message.Message)
                        {
                            IsDisplayed = true;
                            break;
                        }

                    if (!IsDisplayed)
                    {
                        _alert.FormLoad += ((sender1, e1) =>
                        {
                            e1.Buttons.PinButton.SetDown(true);
                        });

                        _alert.AutoFormDelay = 0;
                        _alert.AppearanceCaption.ForeColor = Color.Red;
                        _alert.AllowHotTrack = false;
                        _alert.AllowHtmlText = false;
                        _alert.ShowPinButton = true;
                        _alert.ShowCloseButton = true;

                        _alert.Show(this, this.Text, message.Message);
                        if (message.Terminate)
                            Exit = true;
                    }
                }
            }
        }
        #endregion

        #region Internal event handlers
        //------------------------------------------------------------------------------------------
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Demon.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !Exit)
                e.Cancel = !Question.YesNo("Do you want to exit this application", this.Text);

            if (!e.Cancel)
            {
                Demon.Enabled = false;
                FormUtils.CloseAllForms(this);
                RemoveRibbonItems();
                DB2Factory.PurgeLogFiles();
            }
        }

        private void barExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barWindows_ItemClick(object sender, ItemClickEventArgs e)
        {
            barWindows.Strings.Clear();
            foreach (Form frm in Application.OpenForms)
                if (frm.MdiParent == null && frm != this && frm.Text != string.Empty)
                    barWindows.Strings.Add(frm.Text);
        }

        /// <summary>
        /// Activate a window that is not in the application's frame container.  A floating dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWindows_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
                if (frm.Text == barWindows.Strings[e.Index])
                {
                    frm.Activate();
                    frm.BringToFront();
                    frm.Focus();
                }
        }

        private void MasterRibbon_Merge(object sender, RibbonMergeEventArgs e)
        {
            MasterRibbon.SelectedPage = e.MergedChild.SelectedPage;
        }

        /// <summary>
        /// Check the system is available
        /// Get any system messages
        /// Check the timeout hasn't occurred.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Demon_Tick(object sender, EventArgs e)
        {
            Demon.Enabled = false;

            // Terminate the application if system is not available.
            if (!Exit)
                Exit = !CheckSystemAvailability();

            // Check for idle timeout.
            if (!Exit)
            {
                var idleTime = new IdleTime();
                if (!System.Diagnostics.Debugger.IsAttached && idleTime.Seconds() > Properties.Settings.Default.IdleTimeout)
                    Exit = true;
            }

            // Build and display a list of system messages
            if (!Exit)
                ShowSystemMessages();

            // Say bye bye?
            if (Exit)
            {
                // bit of a cludge, must reset any 'IsDirty/FormState' properties to false/0 for any open form that has this property 
                // to prevent the user being prompted to save any changes.
                foreach (Form frm in Application.OpenForms)
                {
                    var pi = frm.GetType().GetProperty("IsDirty");
                    if (pi != null && pi.CanWrite)
                        pi.SetValue(frm, false, null);

                    pi = frm.GetType().GetProperty("FormState");
                    if (pi != null && pi.CanWrite)
                        pi.SetValue(frm, 0, null);

                    FormUtils.AddTag(frm, "ForceClose");
                }
                Application.DoEvents();
                Thread.Sleep(5000);
                this.Close();
            }
            else
                Demon.Enabled = true;
        }

        /// <summary>
        /// Change the colours to the selected example.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void galleryColours_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(e.Item.Caption);
            Session.SkinColor = e.Item.Caption;
        }

        /// <summary>
        /// Make the title show the text from the selected ribbon tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterRibbon_SelectedPageChanged(object sender, EventArgs e)
        {
            if (MasterRibbon.SelectedPage != null)
                this.Text = MasterRibbon.SelectedPage.Text;
        }

        private void StatusEnvironment_ItemClick(object sender, ItemClickEventArgs e)
        {
            var info = Session.Environment.ToString();
            Clipboard.Clear();
            Clipboard.SetText(info);
            MessageBox.Show(info + "\nInformation has been copied to the clipboard", "Environment Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void barSQL_ItemClick(object sender, ItemClickEventArgs e)
        {
            var frm = new Forms.SQLQuery();
            frm.MdiParent = this;
            frm.Show();
        }

        //------------------------------------------------------------------------------------------
        #endregion

    }

}