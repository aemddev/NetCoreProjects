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
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();
            btnItems.Click += delegate { ShowItemView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowItemView;
        public event EventHandler ShowBrandView;
        public event EventHandler ShowSupplierView;

    }
}
