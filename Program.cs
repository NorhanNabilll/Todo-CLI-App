using Todo_CLI_App.Models;
using Todo_CLI_App.Services;

class Program
{
    static async Task Main()
    {
        var storage = new JsonTaskStorage("tasks.json"); 
        var manager = new TodoManager(storage);

        await manager.InitializeAsync();

  
       // var newTask = await manager.AddAsync("Buy milkmmmmmmmm", Priority.High, new List<string> { "shopping", "urgent" });
     //   Console.WriteLine($"Added: {newTask.Description} with ID {newTask.Id}");

  
        var allTasks = manager.GetAll();
        foreach (var task in allTasks)
        {
            Console.WriteLine($"{task.Id}: {task.Description} - Completed: {task.IsCompleted}");
        }


       // await manager.MarkDoneAsync(1);


        await manager.DeleteAsync(1);
    }
}
