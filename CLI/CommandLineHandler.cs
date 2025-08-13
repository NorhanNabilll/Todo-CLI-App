using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo_CLI_App.Models;
using Todo_CLI_App.Services;

namespace Todo_CLI_App.CLI
{
    public class CommandLineHandler
    {
        private readonly TodoManager _todoManager;

        public CommandLineHandler(TodoManager todoManager)
        {
            _todoManager = todoManager;
        }
 
        // Processes command line arguments and executes actions
        public async Task<int> HandleCommandAsync(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    
                    return 0;
                }

                var command = args[0].ToLower();

                return command switch
                {
                    "add" => await HandleAddCommand(args),
                    "list" => HandleListCommand(args),
                    _ => HandleInvalidCommand(command)

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        }

        // Handles the 'add' command with priority and tags support
        public async Task<int> HandleAddCommand(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Task description is required");
                Console.WriteLine("Usage: add \"Task description\" [--priority high|medium|low] [--tags tag1,tag2]");
                return 1;
            }

            var description = args[1];
            var priority = Priority.Medium;
            var tags = new List<string>();

            // Parse optional arguments
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i] == "--priority" && i + 1 < args.Length)
                {
                    if (Enum.TryParse<Priority>(args[i + 1], true, out var parsedPriority))
                    {
                        priority = parsedPriority;
                    }
                    i++; // skip priority value
                }
                else if (args[i] == "--tags" && i + 1 < args.Length)
                {
                    tags = args[i + 1].Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    i++; // skip tags value
                }
            }

            var item = await _todoManager.AddAsync(description, priority, tags);
            Console.WriteLine($"Added task #{item.Id}: {item.Description}");
            return 0;
        }


        /// Handles the 'list' command with filtering options
        public int HandleListCommand(string[] args)
        {
            bool? completedFilter = null;
            string? tagFilter = null;

            // Parse optional arguments
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--pending")
                {
                    completedFilter = false;
                }
                else if (args[i] == "--completed")
                {
                    completedFilter = true;
                }
                else if (args[i] == "--tag" && i + 1 < args.Length)
                {
                    tagFilter = args[i + 1];
                    i++; // Skip the next argument
                }
            }

            var items = _todoManager.GetAll(completedFilter, tagFilter);

            if (!items.Any())
            {
                if (completedFilter == false)
                    Console.WriteLine("No pending tasks found");
                else if (completedFilter == true)
                    Console.WriteLine("No completed tasks found");
                else if (!string.IsNullOrWhiteSpace(tagFilter))
                    Console.WriteLine($"No tasks found with tag '{tagFilter}'");
                else
                    Console.WriteLine("No tasks found");
                return 0;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id}. {item}");
            }

            return 0;
        }
        private int HandleInvalidCommand(string command)
        {
            Console.WriteLine($"Error: Unknown command '{command}'");
            return 1;
        }

    }
}
