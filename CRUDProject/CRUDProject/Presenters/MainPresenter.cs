using CRUDProject.Models;
using CRUDProject.Repositories;
using CRUDProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDProject.Presenters
{
    internal class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowItemView += ShowItemsView;
        }

        private void ShowItemsView(object sender, EventArgs e)
        {
            IItemView view = ItemView.GetInstance((Form)mainView);
            IItemRepository repository = new ItemRepository(sqlConnectionString);
            new ItemPresenter(view, repository);
        }
    }
}
