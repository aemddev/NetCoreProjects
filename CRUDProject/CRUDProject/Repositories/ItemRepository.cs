using CRUDProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProject.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        
        //constructor
        public ItemRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //methods
        public void Add(ItemModel itemModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Item VALUES (@name, @description, @stock, @price)";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = itemModel.Name;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = itemModel.Description;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = itemModel.Stock;
                command.Parameters.Add("@price", SqlDbType.Int).Value = itemModel.Price;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Item WHERE Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ItemModel itemModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE Item SET Name=@name, Description=@description, Stock=@stock, Price=@price WHERE Id=@id";
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = itemModel.Name;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = itemModel.Description;
                command.Parameters.Add("@stock", SqlDbType.Int).Value = itemModel.Stock;
                command.Parameters.Add("@price", SqlDbType.Int).Value = itemModel.Price;
                command.Parameters.Add("@id", SqlDbType.Int).Value = itemModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ItemModel> GetAll()
        {
            var itemList = new List<ItemModel>();
            using(var connection = new SqlConnection(connectionString) )
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Item ORDER BY Id DESC";
                using(var  reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var itemModel = new ItemModel();
                        itemModel.Id = (int)reader[0];
                        itemModel.Name = (string)reader[1];
                        itemModel.Description = (string)reader[2];
                        itemModel.Stock = (int)reader[3];
                        itemModel.Price = (int)reader[4];
                        itemList.Add(itemModel);
                    }
                }
            }
            return itemList;
        }

        public IEnumerable<ItemModel> GetByValue(string value)
        {
            var itemList = new List<ItemModel> ();
            int itemId = int.TryParse(value, out _) ? Convert.ToInt32(value): 0;
            string itemName = value;
            using(var connection = new SqlConnection(connectionString))
            using(var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Item
                    WHERE Id=@id or Name like @name+'%' 
                    ORDER BY Id DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = itemId;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = itemName;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var itemModel = new ItemModel();
                        itemModel.Id = (int)reader[0];
                        itemModel.Name = (string)reader[1];
                        itemModel.Description = (string)reader[2];
                        itemModel.Stock = (int)reader[3];
                        itemModel.Price = (int)reader[4];
                        itemList.Add(itemModel);
                    }
                }
            }
            return itemList;
        }
    }
}
