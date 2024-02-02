using MySqlConnector;
using System.Collections.Generic;
using ToDoList.Models; // Assuming there are reasons for this, even if it seems redundant in this context.

namespace ToDoList.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int Id { get; set; } // Setter for database operations

        public Item(string description)
        {
            Description = description;
        }

        public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

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

        public static Item Find(int searchId)
        {
            Item placeholderItem = new Item("placeholder item");
            return placeholderItem;
        }

        public override bool Equals(System.Object? otherItem)
{
    if (otherItem == null || !(otherItem is Item newItem))
    {
        return false;
    }
    return this.Id == newItem.Id && this.Description == newItem.Description;
}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}