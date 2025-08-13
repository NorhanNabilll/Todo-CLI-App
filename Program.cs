using System;
using System.IO;
using System.Threading.Tasks;
using Todo_CLI_App.CLI;
using Todo_CLI_App.Services;

class Program
{
    static async Task<int> Main(string[] args)
    {
        try
        {
            // Initialize dependencies with dependency injection pattern
            var persistenceService = new JsonTaskStorage("tasks.json");
            var todoManager = new TodoManager(persistenceService);
            var commandHandler = new CommandLineHandler(todoManager);

            // Initialize the todo manager (loads existing tasks)
            await todoManager.InitializeAsync();

            // Process the command and return appropriate exit code
            return await commandHandler.HandleCommandAsync(args);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            return 1;
        }
    }
}
