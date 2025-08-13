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
                    ShowHelp();
                    return 0;
                }

                var command = args[0].ToLower();

                return command switch
                {
                    "add" => await HandleAddCommand(args),
                    "list" => HandleListCommand(args),
                    "done" => await HandleDoneCommand(args),
                    "delete" => await HandleDeleteCommand(args),
                    "help" => HandleHelpCommand(),
                    "stats" => HandleStatsCommand(),
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
                    //  validation for invalid priority values
                    else
                    {
                        Console.WriteLine($"Error: Invalid priority '{args[i + 1]}'. Valid options: high, medium, low");
                        Console.WriteLine("Usage: add \"Task description\" [--priority high|medium|low] [--tags tag1,tag2]");
                        return 1;
                    }
                    i++; // skip priority value
                }
                else if (args[i] == "--tags" && i + 1 < args.Length)
                {
                    tags = args[i + 1].Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
                    i++; // skip tags value
                }
                //  Handle unknown options starting with --
                else if (args[i].StartsWith("--"))
                {
                    Console.WriteLine($"Error: Unknown option '{args[i]}' for add command");
                    Console.WriteLine("Usage: add \"Task description\" [--priority high|medium|low] [--tags tag1,tag2]");
                    return 1;
                }
                //  Handle unexpected arguments that don't start with --
                else
                {
                    Console.WriteLine($"Error: Unexpected argument '{args[i]}'");
                    Console.WriteLine("Usage: add \"Task description\" [--priority high|medium|low] [--tags tag1,tag2]");
                    return 1;
                }
            }

            var item = await _todoManager.AddAsync(description, priority, tags);
            Console.WriteLine($"Added task #{item.Id}: {item.Description}");
            return 0;
        }


        // Handles the 'list' command with filtering options
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
                // Handle unknown options starting with --
                else if (args[i].StartsWith("--"))
                {
                    Console.WriteLine($"Error: Unknown option '{args[i]}' for list command");
                    Console.WriteLine("Usage: list [--pending|--completed] [--tag <tag_name>]");
                    return 1;
                }
                // Handle unexpected arguments that don't start with --
                else
                {
                    Console.WriteLine($"Error: Unexpected argument '{args[i]}'");
                    Console.WriteLine("Usage: list [--pending|--completed] [--tag <tag_name>]");
                    return 1;
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



        // Handles the 'done' command to mark tasks as completed
        public async Task<int> HandleDoneCommand(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Task ID is required");
                Console.WriteLine("Usage: done <task_id>");
                return 1;
            }

            if (!int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Error: Invalid task ID");
                Console.WriteLine("Usage: done <task_id>");
                return 1;
            }

            // Check for unexpected additional arguments
            if (args.Length > 2)
            {
                Console.WriteLine($"Error: Unexpected argument '{args[2]}'. The done command only accepts a task ID");
                Console.WriteLine("Usage: done <task_id>");
                return 1;
            }

            var success = await _todoManager.MarkDoneAsync(id);
            if (success)
            {
                Console.WriteLine($"Task #{id} marked as completed");
                return 0;
            }
            else
            {
                Console.WriteLine($"Error: Task #{id} not found");
                return 1;
            }
        }

        // Handles the 'delete' command
        public async Task<int> HandleDeleteCommand(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Error: Task ID is required");
                Console.WriteLine("Usage: delete <task_id>");
                return 1;
            }

            if (!int.TryParse(args[1], out int id))
            {
                Console.WriteLine("Error: Invalid task ID");
                Console.WriteLine("Usage: delete <task_id>");
                return 1;
            }

            // Check for unexpected additional arguments
            if (args.Length > 2)
            {
                Console.WriteLine($"Error: Unexpected argument '{args[2]}'. The delete command only accepts a task ID");
                Console.WriteLine("Usage: delete <task_id>");
                return 1;
            }

            var success = await _todoManager.DeleteAsync(id);
            if (success)
            {
                Console.WriteLine($"Task #{id} deleted");
                return 0;
            }
            else
            {
                Console.WriteLine($"Error: Task #{id} not found");
                return 1;
            }
        }

        private int HandleInvalidCommand(string command)
        {
            Console.WriteLine($"Error: Unknown command '{command}'");
            ShowHelp();
            return 1;
        }
        private int HandleHelpCommand()
        {
            ShowHelp();
            return 0;
        }

        public void ShowHelp()
        {
            Console.WriteLine("Todo App - Command Line Task Manager");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  add \"Task description\" [--priority high|medium|low] [--tags tag1,tag2]");
            Console.WriteLine("  list [--pending|--completed] [--tag <tag_name>]");
            Console.WriteLine("  done <task_id>");
            Console.WriteLine("  delete <task_id>");
            Console.WriteLine("  stats");
            Console.WriteLine("  help");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("  add \"Buy milk\"");
            Console.WriteLine("  add \"Finish project\" --priority high --tags work,urgent");
            Console.WriteLine("  list");
            Console.WriteLine("  list --pending");
            Console.WriteLine("  list --tag work");
            Console.WriteLine("  done 2");
            Console.WriteLine("  delete 3");
            Console.WriteLine("  stats");
        }


        // Handles the 'stats' command to show statistics
        public int HandleStatsCommand()
        {
            var stats = _todoManager.GetStatistics();
            Console.WriteLine("Task Statistics:");
            Console.WriteLine($"  Total: {stats.Total}");
            Console.WriteLine($"  Completed: {stats.Completed}");
            Console.WriteLine($"  Pending: {stats.Pending}");
            Console.WriteLine($"  Completion Rate: {stats.CompletionRate}%");
            return 0;
        }
    }
}