using MySqlConnector;
using System.Collections.Generic;
using ToDoList.Models; // Keep all your directives as is

namespace ToDoList.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int Id { get; set; } // Keep setters as you've defined

        public Item(string description)
        {
            Description = description;
        }

        public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

        public void Save()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO items (description) VALUES (@ItemDescription);";
            cmd.Parameters.AddWithValue("@ItemDescription", this.Description);
            cmd.ExecuteNonQuery();
            Id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void ClearAll()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM items;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Item> GetAll()
        {
            List<Item> allItems = new List<Item> { };
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM items;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int itemId = rdr.GetInt32(0);
                string itemDescription = rdr.GetString(1);
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

        public static Item Find(int id)
{
    MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
    conn.Open();
    MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"SELECT * FROM items WHERE iditems = @ThisId;"; // Adjusted to iditems
    cmd.Parameters.AddWithValue("@ThisId", id);
    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    
    int itemId = 0;
    string itemDescription = "";
    
    while (rdr.Read())
    {
        itemId = rdr.GetInt32(0); // Assuming iditems is the first column
        itemDescription = rdr.GetString(1); // Assuming description is the second column
    }
    Item foundItem = new Item(itemDescription, itemId);
    
    rdr.Close();
    conn.Close();
    if (conn != null)
    {
        conn.Dispose();
    }

    return foundItem;
}

        public override bool Equals(System.Object? otherItem) 
{
    if (!(otherItem is Item newItem))
    {
        return false;
    }
    return Id == newItem.Id && Description == newItem.Description;
}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}