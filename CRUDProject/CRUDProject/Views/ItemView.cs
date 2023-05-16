using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDProject.Views
{
    public partial class ItemView : Form, IItemView
    {
        //fields
        private readonly string itemId;
        private string itemName;
        private string itemDescription;
        private string itemStock;
        private string itemPrice;
        private string searchValue;
        private bool isEdit;
        private bool isSuccessfull;
        private string message;

        //constructors
        public ItemView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPageItemDetails);
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvents()
        {
            //Search
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) 
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            //AddNew
            btnAddNew.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageItemList);
                tabControl1.TabPages.Add(tabPageItemDetails);
                txtItemId.ReadOnly = true;
                tabPageItemDetails.Text = "Add new item";
            };
            //Edit
            btnEdit.Click += delegate 
            { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageItemList);
                tabControl1.TabPages.Add(tabPageItemDetails);
                tabPageItemDetails.Text = "Edit item";
            };
            //Save Change
            btnSave.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessfull)
                {
                tabControl1.TabPages.Remove(tabPageItemDetails);
                tabControl1.TabPages.Add(tabPageItemList);
                }
                MessageBox.Show(Message);
            };
            //Cancel
            btnCancel.Click += delegate 
            { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageItemDetails);
                tabControl1.TabPages.Add(tabPageItemList);
            };
            //Delete
            btnDelete.Click += delegate 
            { 
                var result = MessageBox.Show("Are you sure you want to delete the selected item?","Warning",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }


        //properties
        public string ItemId 
        {
            get { return txtItemId.Text; } 
            set { txtItemId.Text = value; }
        }
        public string ItemName 
        { 
            get { return txtItemName.Text; }
            set { txtItemName.Text = value; } 
        }
        public string ItemDescription 
        {
            get { return txtItemDesciption.Text; }
            set { txtItemDesciption.Text = value; }
        }
        public string ItemStock 
        { 
            get { return txtItemStock.Text; }
            set { txtItemStock.Text = value; }
        }
        public string ItemPrice 
        {
            get { return txtItemPrice.Text; } 
            set { txtItemPrice.Text = value; }
        }
        public string SearchValue 
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; } 
        }
        public bool IsEdit 
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccessfull 
        { 
            get { return isSuccessfull; }
            set { isSuccessfull = value; }
        }
        public string Message 
        {
            get { return message; }
            set { message = value; }
        }

        //events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler CancelEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;

        //methods
        public void SetItemListBindingSource(BindingSource itemList)
        {
            dataGridView1.DataSource = itemList;
        }

        //Singleton Pattern (Open a single form instance)
        private static ItemView instance;
        public static ItemView GetInstance(Form parentContainer)
        {
            if(instance== null || instance.IsDisposed)
            {
                instance = new ItemView();
                instance.MdiParent= parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if(instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
