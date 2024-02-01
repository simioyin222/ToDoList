using MySqlConnector;
using System.Collections.Generic;
using ToDoList.Models; // Ensures DBConfiguration is accessible

namespace ToDoList.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int Id { get; set; } // Updated to include a setter for database operations

        // Constructor for creating a new item without an ID (before inserting into the database)
        public Item(string description)
        {
            Description = description;
        }

        // Overloaded constructor for creating an item object from database record
        public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

        // Placeholder for Find() method
        public static Item Find(int searchId)
        {
            // Temporarily returning placeholder item until method is updated to interact with the database
            Item placeholderItem = new Item("placeholder item");
            return placeholderItem;
        }

        // Placeholder for ClearAll() method, to be updated for database interaction
        public static void ClearAll()
        {
        }

        // Method to get all items from the database
        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item>();
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int itemId = rdr.GetInt32(0); // Assuming id is the first column
                string itemDescription = rdr.GetString(1); // Assuming description is the second column
                Item newItem = new Item(itemDescription, itemId);
                allItems.Add(newItem);
            }
            rdr.Close();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allItems;
        }
    }
}