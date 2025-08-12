using Todo_CLI_App.Models;
using Todo_CLI_App.Services;

class Program
{
    static async Task Main()
    {
        // Quiq test for storage
        string filePath = "tasks.json";
        var storage = new JsonTaskStorage(filePath);

        
        var tasks = new List<TodoItem>
        {
            new TodoItem("Buy milk", Priority.High)
            { Tags = new List<string> { "shopping" } },
            new TodoItem("Study C#", Priority.Medium) { Tags = new List<string> { "learning", "coding" } }
        };

 
        await storage.SaveAsync(tasks);
        Console.WriteLine("✅ Tasks saved!");


        var loadedTasks = await storage.LoadAsync();
        Console.WriteLine("📋 Loaded tasks:");
        foreach (var task in loadedTasks)
        {
            Console.WriteLine(task.Description);
        }
    }
}
