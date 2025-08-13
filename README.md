# Todo CLI App

A command-line todo application built with **C#** and **.NET 8.0** for managing tasks efficiently from the terminal.  
This project was developed as part of the **eT3 Internship Challenge 2025**, implementing all **core requirements** plus comprehensive **bonus features**.

---

## ✅ Core Requirements Implemented
- **Add Tasks**: `add "Buy milk"` → adds a new task  
- **List Tasks**: `list` → displays all tasks with index numbers  
- **Mark Complete**: `done 2` → marks task #2 as completed  
- **Delete Tasks**: `delete 3` → removes task #3  
- **File Storage**: Tasks persist in `tasks.json` file  

---

## 🚀 Bonus Features Implemented
- **Creation Timestamps**: Automatic timestamp tracking for each task  
- **Priority Levels**: High, Medium, Low  
- **Tags/Categories**: Organize tasks with multiple tags  
- **Task Statistics**: View completion rates and task counts  
- **Advanced Filtering**: Filter by completion status and tags  
- **Robust Error Handling**: Graceful handling of invalid inputs and file errors  
- **Clean Architecture**: Modular design with separation of concerns  

---

## 🛠️ Tech Stack
- **Language**: C# 12  
- **Framework**: .NET 8.0  
- **JSON Library**: Newtonsoft.Json 13.0.3  
- **Architecture**: Clean Architecture with Dependency Injection  

---

## 📁 Project Structure
```
Todo_CLI_App/
├── Models/
│   ├── TodoItem.cs              # Task data model
│   └── TodoStatistics.cs        # Statistics calculation model
├── Services/
│   ├── ITaskStorage.cs          # Interface for data storage
│   ├── JsonTaskStorage.cs       # JSON file storage implementation
│   └── TodoManager.cs           # Core business logic layer
├── CLI/
│   └── CommandLineHandler.cs    # Command parsing and user interface
├── Program.cs                   # Application entry point
├── Todo CLI App.csproj          # Project configuration
└── tasks.json                   # Auto-created data file
```

---

## 🚀 How to Build and Run

### Prerequisites
- Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Clone and Build
```bash
git clone <repository-url>
cd Todo_CLI_App
dotnet build
```

### Run from Source
```bash
dotnet run -- <command> [arguments]
```

### Or Run from Executable
```bash
dotnet build
cd bin/Debug/net8.0
./Todo_CLI_App <command> [arguments]
```

---

## 📖 Usage Examples

**Add a task**
```bash
dotnet run -- add "Buy milk"
```

**Add with priority and tags**
```bash
dotnet run -- add "Finish report" --priority high --tags work,urgent
```

**List all tasks**
```bash
dotnet run -- list
```

**Mark task as done**
```bash
dotnet run -- done 1
```

**Delete a task**
```bash
dotnet run -- delete 2
```

**List only pending**
```bash
dotnet run -- list --pending
```

**Filter by tag**
```bash
dotnet run -- list --tag work
```

**View statistics**
```bash
dotnet run -- stats
```

**Get help**
```bash
dotnet run -- help
```

---

## 📋 Complete Command Reference
| Command  | Description         | Usage |
|----------|--------------------|-------|
| `add`    | Create new task    | `add "description" [--priority high\|medium\|low] [--tags tag1,tag2]` |
| `list`   | Display tasks      | `list [--pending\|--completed] [--tag <tag_name>]` |
| `done`   | Mark as completed  | `done <task_id>` |
| `delete` | Remove task        | `delete <task_id>` |
| `stats`  | Show statistics    | `stats` |
| `help`   | Show help          | `help` |

---

## 💾 Data Storage
Tasks are stored in `tasks.json` in JSON format:
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

---

## 👩‍💻 Author
Developed by **Norhan Nabil** for the eT3 Internship Challenge 2025.
