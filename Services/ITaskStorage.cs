using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_CLI_App.Models;

namespace Todo_CLI_App.Services
{
    // Interface for Storage operations, allowing future swap to database or API
    public interface ITaskStorage
    {
        /// Loads all todo items from storage
        Task<List<TodoItem>> LoadAsync();


        /// Saves todo items to storage
        Task SaveAsync(List<TodoItem> items);


        /// Checks if storage exists and is accessible
        Task<bool> ExistsAsync();
    }
}
