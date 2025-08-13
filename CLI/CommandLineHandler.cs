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


        // Handles the 'add' command with priority and tags support
        private async Task<int> HandleAddCommand(string[] args)
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
    }
}
