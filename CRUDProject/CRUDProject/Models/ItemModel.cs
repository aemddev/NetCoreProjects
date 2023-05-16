using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProject.Models
{
    public class ItemModel
    {
        //Fields
        private int id;
        private string name;
        private string description;
        private int stock;
        private int price;

        //Constructors and Validations
        [DisplayName("Item ID")]
        public int Id 
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "Item Name is required")]
        [StringLength(20, MinimumLength =3, ErrorMessage = "The Item name must be between 3 and 20 characters")]
        public string Name 
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Item description is required")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "The Item description must be between 7 and 50 characters")]
        public string Description 
        {
            get { return description; }
            set { description = value; } 
        }

        [DisplayName("Stock")]
        [Required(ErrorMessage ="Please insert the stock")]
        [Range(1,100,ErrorMessage ="Stock must be between 1 and 99")]
        public int Stock 
        { 
            get => stock; 
            set => stock = value; 
        }

        [DisplayName("Price")]
        [Required(ErrorMessage ="Please insert the price")]
        [Range(1,99999,ErrorMessage = "Price mus be between 1 and 99999")]
        public int Price 
        {
            get { return price; }
            set { price = value; }
        }
    }
}
