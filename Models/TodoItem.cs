using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;              // <-- ADD THIS
using Newtonsoft.Json.Converters;   // <-- Keep this

namespace Todo_CLI_App.Models
{
    // Represents a single to-do item with its properties and state
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }

        public List<string> Tags { get; set; } = new List<string>();

        // Constructor for creating new todo items
        public TodoItem(string description, Priority priority = Priority.Medium)
        {
            Description = description;
            Priority = priority;
            CreatedAt = DateTime.UtcNow;
            IsCompleted = false;
            Tags = new List<string>();
        }

        public TodoItem()
        {
            Tags = new List<string>();
        }
    }

    // Priority levels for todo items
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}