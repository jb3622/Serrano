using System;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Disney.iDash.Shared.Controls
{
    public partial class ListMover : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler OnChanged;
		public delegate bool MoveEventHandler(ListBoxControl sourceList, ListBoxControl targetList, BaseListBoxControl.SelectedItemCollection selectedItems, bool moveAll);
		public event MoveEventHandler OnMoveItem;

        public ListMover()
        {
            InitializeComponent();
            AvailablePrompt = "Available";
            SelectedPrompt = "Selected";
        }

        public string AvailablePrompt { get; set; }
        public string SelectedPrompt { get; set; }

        // simulate readonly attribute
        private bool mReadOnly = false;
        public bool ReadOnly
        {
            get 
            { 
                return mReadOnly; 
            }
            set
            {
                mReadOnly = value;
                lstAvailable.Enabled = value;
                lstSelected.Enabled = value;
                btnMoveLeft.Enabled = value;
                btnMoveRight.Enabled = value;
                btnMoveLeftAll.Enabled = value;
                btnMoveRightAll.Enabled = value;
            }
        }

		public IStyleController StyleControler
		{
			get { return lstAvailable.StyleController ; }
			set 
			{
				lstAvailable.StyleController = value;
				lstSelected.StyleController = value;
			}
		}

        public ListBoxItemCollection AvailableItems
        {
            get
            {
                return lstAvailable.Items;
            }         
            set
            {
                if (value == null)
                    lstAvailable.Items.Clear();
                else
                    foreach (var obj in value)
                        lstAvailable.Items.Add(obj);
                RefreshButtons();
            }
        }

        public object AvailableDataSource
        {
            get
            {
                return lstAvailable.DataSource;
            }
            set
            {
                lstAvailable.DataSource = value;
                RefreshButtons();
            }
        }

        public string AvailableDisplayMember
        {
            get
            {
                return lstAvailable.DisplayMember;
            }
            set
            {
                lstAvailable.DisplayMember = value;
            }
        }

        public string AvailableValueMember
        {
            get
            {
                return lstAvailable.ValueMember;
            }
            set
            {
                lstAvailable.ValueMember = value;
            }
        }

        public ListBoxItemCollection SelectedItems
        {
            get
            {
                return lstSelected.Items;
            }
            set
            {
                if (value == null)
                    lstSelected.Items.Clear();
                else
                    foreach (var obj in value)
                        lstSelected.Items.Add(obj);
                RefreshButtons();
            }
        }

        public object SelectedDataSource
        {
            get
            {
                return lstSelected.DataSource;
            }
            set
            {
                lstSelected.DataSource = value;
                RefreshButtons();
            }
        }

        public string SelectedDisplayMember
        {
            get
            {
                return lstSelected.DisplayMember;
            }
            set
            {
                lstSelected.DisplayMember = value;
            }
        }

        public string SelectedValueMember
        {
            get
            {
                return lstSelected.ValueMember;
            }
            set
            {
                lstSelected.ValueMember = value;
            }
        }
                
        private void btnMoveRightAll_Click(object sender, EventArgs e)
        {
            MoveItems(lstAvailable, lstSelected, true);
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            MoveItems(lstAvailable, lstSelected);
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            MoveItems(lstSelected, lstAvailable);
        }

        private void btnMoveLeftAll_Click(object sender, EventArgs e)
        {
            MoveItems(lstSelected, lstAvailable, true);
        }

        private void lstAvailable_DoubleClick(object sender, EventArgs e)
        {
            MoveItems(lstAvailable, lstSelected);
        }

        private void lstSelected_DoubleClick(object sender, EventArgs e)
        {
            MoveItems(lstSelected, lstAvailable);
        }

        private void MoveItems(ListBoxControl lstSource, ListBoxControl lstTarget)
        {
            MoveItems(lstSource, lstTarget, false);
        }

        private void MoveItems(ListBoxControl lstSource, ListBoxControl lstTarget, bool moveAll)
        {
			var result = false;
			if (OnMoveItem != null)
			{
				result = OnMoveItem(lstSource, lstTarget, lstSource.SelectedItems, moveAll);
				if (result)
				{

					if (OnChanged != null)
						OnChanged(this, null);

					lstSource.UnSelectAll();
					lstSource.Refresh();
					lstTarget.Refresh();
					RefreshButtons();
				}
			}

        }

        public void RefreshButtons()
        {
			lstSelected.Enabled = !mReadOnly && lstSelected.ItemCount > 0;
            lstAvailable.Enabled = !mReadOnly && lstAvailable.ItemCount > 0;
            btnMoveLeft.Enabled = !mReadOnly && lstSelected.SelectedItems.Count > 0;
            btnMoveLeftAll.Enabled = !mReadOnly && lstSelected.ItemCount > 0;
            btnMoveRight.Enabled = !mReadOnly && lstAvailable.SelectedItems.Count > 0;
            btnMoveRightAll.Enabled = !mReadOnly && lstAvailable.ItemCount > 0;

            lblAvailable.Text = string.Format("{0} {1}", lstAvailable.ItemCount, AvailablePrompt);
            lblSelected.Text = string.Format("{0} {1}", lstSelected.ItemCount, SelectedPrompt);
        }

        private void lstAvailable_Click(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void lstSelected_Click(object sender, EventArgs e)
        {
            RefreshButtons();
        }

        private void ListMover_Resize(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Width / 2;
        }

    }
}
