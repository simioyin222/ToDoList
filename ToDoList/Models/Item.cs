using MySqlConnector;

namespace ToDoList.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int Id { get; set; }

        public Item(string description)
        {
            Description = description;
        }

        // Overloaded constructor for database operations
        public Item(string description, int id)
        {
            Description = description;
            Id = id;
        }

        // Placeholder for other methods like Find() and ClearAll()
    }
}