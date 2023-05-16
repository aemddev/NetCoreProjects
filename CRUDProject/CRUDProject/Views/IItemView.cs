using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDProject.Views
{
    public interface IItemView
    {
        //Fields and Properties
        string ItemId { get; set; }
        string ItemName { get; set; }
        string ItemDescription { get; set; }
        string ItemStock { get; set; }
        string ItemPrice { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessfull { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler CancelEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;

        //Methods
        void SetItemListBindingSource(BindingSource itemList);
        void Show();
    }
}
