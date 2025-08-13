# Todo CLI App

A command-line todo application built with C# and .NET 8.0 for managing tasks efficiently from the terminal. This project was developed as part of a technical internship challenge, implementing all core requirements plus comprehensive bonus features.

## ✅ Core Requirements Implemented

- ✅ **Add Tasks**: `add "Buy milk"` → adds a new task
- ✅ **List Tasks**: `list` → displays all tasks with index numbers  
- ✅ **Mark Complete**: `done 2` → marks task #2 as completed
- ✅ **Delete Tasks**: `delete 3` → removes task #3
- ✅ **File Storage**: Tasks persist in `tasks.json` file

## 🚀 Bonus Features Implemented

- ✅ **Creation Timestamps**: Automatic timestamp tracking for each task
- ✅ **Priority Levels**: Support for High, Medium, Low priority tasks
- ✅ **Tags/Categories**: Organize tasks with multiple tags
- ✅ **Task Statistics**: View completion rates and task counts
- ✅ **Advanced Filtering**: Filter by completion status and tags
- ✅ **Robust Error Handling**: Graceful handling of invalid inputs and file errors
- ✅ **Clean Architecture**: Modular design with separation of concerns

## 🛠️ Tech Stack

- **Language**: C# 12
- **Framework**: .NET 8.0
- **JSON Library**: Newtonsoft.Json 13.0.3
- **Architecture**: Clean Architecture with Dependency Injection pattern

## 📁 Project Structure

```
Todo_CLI_App/
├── Models/
│   ├── TodoItem.cs              # Task data model with properties
│   └── TodoStatistics.cs        # Statistics calculation model
├── Services/
│   ├── ITaskStorage.cs         # Interface for data storage
│   ├── JsonTaskStorage.cs      # JSON file storage implementation
│   └── TodoManager.cs          # Core business logic layer
├── CLI/
│   └── CommandLineHandler.cs    # Command parsing and user interface
├── Program.cs                   # Application entry point
├── TodoApp.csproj              # Project configuration
└── tasks.json                  # Data storage file (auto-created)

```

## 🚀 How to Build and Run

### Prerequisites
- .NET 8.0 SDK installed on your system

### Building the Application
```bash
# Clone the repository
git clone <repository-url>
cd Todo_CLI_App

# Build the project
dotnet build

# Run tests (optional)
dotnet test
```

### Running the Application
```bash
# Basic usage
dotnet run -- <command> [arguments]

# Or build and run executable
dotnet build
cd bin/Debug/net8.0
./TodoApp <command> [arguments]
```

## 📖 Usage Examples

### Basic Commands

**Add a simple task:**
```bash
dotnet run -- add "Buy milk"
```
*Output:*
```
Added task #1: Buy milk
```

**Add task with priority and tags:**
```bash
dotnet run -- add "Finish project report" --priority high --tags work,urgent
```
*Output:*
```
Added task #2: Finish project report
```

**List all tasks:**
```bash
dotnet run -- list
```
*Output:*
```
1. [ ] Buy milk [Medium] - Created: 2024-01-15 10:30
2. [ ] Finish project report [High] - Created: 2024-01-15 10:35 (Tags: work, urgent)
```

**Mark task as completed:**
```bash
dotnet run -- done 1
```
*Output:*
```
Task #1 marked as completed
```

**Delete a task:**
```bash
dotnet run -- delete 2
```
*Output:*
```
Task #2 deleted
```

### Advanced Features

**List only pending tasks:**
```bash
dotnet run -- list --pending
```

**Filter by tags:**
```bash
dotnet run -- list --tag work
```

**View statistics:**
```bash
dotnet run -- stats
```
*Output:*
```
Task Statistics:
  Total: 5
  Completed: 2
  Pending: 3
  Completion Rate: 40.0%
```

**Get help:**
```bash
dotnet run -- help
```

## 📋 Complete Command Reference

| Command | Description | Usage |
|---------|-------------|-------|
| `add` | Create new task | `add "description" [--priority high\|medium\|low] [--tags tag1,tag2]` |
| `list` | Display tasks | `list [--pending\|--completed] [--tag <tag_name>]` |
| `done` | Mark as completed | `done <task_id>` |
| `delete` | Remove task | `delete <task_id>` |
| `stats` | Show statistics | `stats` |
| `help` | Show help | `help` |

## 🔧 Extension Notes

The application is designed with a clean, modular architecture that makes it easy to extend:

- **Storage Backend**: The `ITaskStorage` interface allows easy swapping to databases, APIs, or cloud storage
- **New Commands**: Add new functionality by extending `CommandLineHandler`
- **Data Model**: `TodoItem` can be extended with additional properties
- **Business Logic**: `TodoManager` centralizes all task operations


## 💾 Data Storage

Tasks are automatically saved to `tasks.json` in the application directory. The JSON format ensures data is human-readable and easily portable:

```json
[
  {
    "Id": 1,
    "Description": "Buy milk",
    "IsCompleted": false,
    "CreatedAt": "2024-01-15T10:30:00Z",
    "Priority": "Medium",
    "Tags": ["shopping"]
  }
]
```

## 📊 Key Features Highlights

- **Persistent Storage**: All tasks automatically saved and restored
- **Flexible Organization**: Use priorities and tags to organize tasks your way  
- **Powerful Filtering**: Find exactly what you're looking for quickly
- **Statistics Tracking**: Monitor your productivity with completion metrics
- **Error Resilience**: Handles corrupted files and invalid inputs gracefully

---

