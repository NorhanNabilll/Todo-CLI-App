using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo_CLI_App.Models;

namespace Todo_CLI_App.Services
{
    // JSON file-based implementation of persistence service
    public class JsonTaskStorage : ITaskStorage
    {
        private readonly string _filePath;
        public JsonTaskStorage(string filePath = "tasks.json")
        {
            _filePath = filePath;
        }

        // Checks if the JSON file exists
        public async Task<bool> ExistsAsync()
        {
            return await Task.FromResult(File.Exists(_filePath));
        }


        // Loads todo items from JSON file, handles corrupted files gracefully
        public async Task<List<TodoItem>> LoadAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<TodoItem>();
                }

                var json = await File.ReadAllTextAsync(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<TodoItem>();
                }

                var items = JsonConvert.DeserializeObject<List<TodoItem>>(json);
                return items ?? new List<TodoItem>();
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Corrupted JSON file: {_filePath}. Error: {ex.Message}", ex);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Unable to read file: {_filePath}. Error: {ex.Message}", ex);
            }
        }


        // Saves todo items to JSON file with proper formatting
        public async Task SaveAsync(List<TodoItem> items)
        {
            try
            {
                var json = JsonConvert.SerializeObject(items, Formatting.Indented);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException($"Unable to write to file: {_filePath}. Error: {ex.Message}", ex);
            }
        }
    }
}
