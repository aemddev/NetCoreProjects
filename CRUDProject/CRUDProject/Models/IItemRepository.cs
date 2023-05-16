using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProject.Models
{
    public interface IItemRepository
    {
        void Add(ItemModel itemModel);
        void Edit(ItemModel itemModel);
        void Delete(int id);
        IEnumerable<ItemModel> GetAll();
        IEnumerable<ItemModel> GetByValue(string value); // Search
    }
}
