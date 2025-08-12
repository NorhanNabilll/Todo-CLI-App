using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_CLI_App.Models;

namespace Todo_CLI_App.Services
{
    // Core business logic for managing todo items
    public class TodoManager
    {
        private readonly ITaskStorage _taskStorage;
        private List<TodoItem> _items;
        private int _nextId;

        public TodoManager(ITaskStorage persistenceService)
        {
            _taskStorage = persistenceService;
            _items = new List<TodoItem>();
            _nextId = 1;
        }

        // Initializes the manager by loading existing items
        public async Task InitializeAsync()
        {
            _items = await _taskStorage.LoadAsync();
            _nextId = _items.Any() ? _items.Max(x => x.Id) + 1 : 1;
        }


        // Adds a new todo item with optional priority and tags
        public async Task<TodoItem> AddAsync(string description, Priority priority = Priority.Medium, List<string>? tags = null)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Task description cannot be empty");
            }

            var item = new TodoItem(description.Trim(), priority)
            {
                Id = _nextId++,
                Tags = tags ?? new List<string>()
            };

            _items.Add(item);
            try
            {
                await _taskStorage.SaveAsync(_items);
            }
            catch (Exception ex)
            {
                // roll back id counter if saving failed
                _nextId--;
                throw new InvalidOperationException("Failed to save the new task.", ex);
            }

            return item;
        }

        
        // Gets all todo items, optionally filtered by completion status and tags
        public List<TodoItem> GetAll(bool? completed = null, string? tagFilter = null)
        {
            var filtered = _items.AsEnumerable();

            if (completed.HasValue)
            {
                filtered = filtered.Where(x => x.IsCompleted == completed.Value);
            }

            if (!string.IsNullOrWhiteSpace(tagFilter))
            {
                filtered = filtered.Where(x => x.Tags.Contains(tagFilter, StringComparer.OrdinalIgnoreCase));
            }

            return filtered.OrderBy(x => x.Id).ToList();
        }



        // Marks a todo item as completed by its ID
        public async Task<bool> MarkDoneAsync(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return false;
            }

            if (item.IsCompleted)
            {
                throw new InvalidOperationException($"Task {id} is already completed");
            }

            item.IsCompleted = true;
            await _taskStorage.SaveAsync(_items);
            return true;
        }


        // Deletes a todo item by its ID
        public async Task<bool> DeleteAsync(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return false;
            }

            _items.Remove(item);
            await _taskStorage.SaveAsync(_items);
            return true;
        }

    }
}
