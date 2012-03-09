using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace Disney.iDash.BaseClasses.Forms
{
    public partial class AddinMenu : DevExpress.XtraBars.Ribbon.RibbonForm
    {

		public int MenuOrder { get; set; }
        public int ApplicationId { get; set; }
		private bool _IsLoading { get; set; }

        public AddinMenu()
        {
            InitializeComponent();
        }

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                this.ParentForm.Cursor = (value ? Cursors.WaitCursor : Cursors.Default);
                Application.DoEvents();
            }
        }

		public List<string> InstallRibbon(RibbonControl parentRibbon)
        {
            var pageTags = new List<string>();
            foreach (RibbonPage page in addinRibbon.Pages)
				if (!pageTags.Contains(page.Text))
					pageTags.Add(page.Text);

			while (this.addinRibbon.Pages.Count>0)
				parentRibbon.Pages.Insert(MenuOrder, this.addinRibbon.Pages[this.addinRibbon.Pages.Count-1]);

            return pageTags;
        }
    }

}