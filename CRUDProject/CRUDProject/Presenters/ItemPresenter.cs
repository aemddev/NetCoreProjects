using CRUDProject.Models;
using CRUDProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDProject.Presenters
{
    public class ItemPresenter
    {
        //Fields
        private IItemView view;
        private IItemRepository repository;
        private BindingSource itemsBindingSource;
        private IEnumerable<ItemModel> itemList;

        //constructor
        public ItemPresenter(IItemView view, IItemRepository repository)
        {
            this.itemsBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;
            //subscribe event handler methods to view events
            this.view.SearchEvent += SearchItem;
            this.view.AddNewEvent += AddItem;
            this.view.EditEvent += LoadSelectedItemToEdit;
            this.view.DeleteEvent += DeleteSelectedItem;
            this.view.SaveEvent += SaveItem;
            this.view.CancelEvent += CancelAction;
            //set Pet binding source
            this.view.SetItemListBindingSource(itemsBindingSource);
            //Load Item List View
            LoadAllItemList();
            //Show the View
            this.view.Show();
        }
        //method
        private void LoadAllItemList()
        {
            itemList = repository.GetAll();
            itemsBindingSource.DataSource = itemList; // set data source.
        }
        private void SearchItem(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrEmpty(this.view.SearchValue);
            if (!emptyValue)
            {
                itemList = repository.GetByValue(this.view.SearchValue);
            }
            else
                itemList = repository.GetAll();
            itemsBindingSource.DataSource = itemList;
        }
        private void AddItem(object sender, EventArgs e)
        {
            CleanViewFields();
            view.IsEdit = false;
        }
        private void LoadSelectedItemToEdit(object sender, EventArgs e)
        {
            var item = (ItemModel)itemsBindingSource.Current;
            view.ItemId = item.Id.ToString();
            view.ItemName = item.Name;
            view.ItemDescription = item.Description;
            view.ItemStock = item.Stock.ToString();
            view.ItemPrice = item.Price.ToString();
            view.IsEdit = true;
        }
        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }
        private void SaveItem(object sender, EventArgs e)
        {
            var model = new ItemModel();
            model.Id = Convert.ToInt32(view.ItemId);
            model.Name = view.ItemName;
            model.Description = view.ItemDescription;
            model.Stock = Convert.ToInt32(view.ItemStock);
            model.Price = Convert.ToInt32(view.ItemPrice);
            try
            {
                new Common.ModelDataValidation().Validate(model);
                if(view.IsEdit) // Edit model
                {
                    repository.Edit(model);
                    view.Message = "Item edited successfully";
                }
                else
                {
                    repository.Add(model);
                    view.Message = "Item added successfully";
                }
                view.IsSuccessfull = true;
                LoadAllItemList();
                CleanViewFields();
            }
            catch(Exception ex)
            {
                view.IsSuccessfull = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.ItemName = "";
            view.ItemDescription = "";
            view.ItemStock = "0";
            view.ItemPrice = "0";
        }

        private void DeleteSelectedItem(object sender, EventArgs e)
        {
            try
            {
                var item = (ItemModel)itemsBindingSource.Current;
                repository.Delete(item.Id);
                view.IsSuccessfull = true;
                view.Message = "Item deleted successfully";
                LoadAllItemList();
            }
            catch (Exception)
            {
                view.IsSuccessfull = false;
                view.Message = "An error ocurred, could nor delete item";
            }
        }

    }
}
