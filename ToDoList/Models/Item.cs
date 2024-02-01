using MySqlConnector;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int Id { get; set; } // Setter for database operations

        // Constructor for a new item without an ID
        public Item(string description)
        {
            Description = description;
        }

        // Overloaded constructor for an item from the database
        public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

        // Updated ClearAll method to interact with the database
        public static void ClearAll()
        {
            using (MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"DELETE FROM items;";
                cmd.ExecuteNonQuery();
            }
        }

        // Method to get all items from the database
        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> { };
            using (MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM items;";
                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                while (rdr.Read())
                {
                    int itemId = rdr.GetInt32(0);
                    string itemDescription = rdr.GetString(1);
                    Item newItem = new Item(itemDescription, itemId);
                    allItems.Add(newItem);
                }
            }
            return allItems;
        }

        // Placeholder for Find() method
        public static Item Find(int searchId)
        {
            Item placeholderItem = new Item("placeholder item");
            return placeholderItem;
        }
    }
}