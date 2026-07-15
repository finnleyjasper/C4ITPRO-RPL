/*
*   A simple console-based To-Do application that allows users to manage tasks using a SQLite database.
*.  Use dotnet run --project TodoApp/TodoApp.csproj to run
*/

using TodoApp.Data;
using TodoApp.Models;

ITaskDAL taskDAL = new SQLiteTaskDAL("tasks.db"); // make the DAL object
string choice = "";

while (choice != "0") // loop until user chooses to exit
{
    Console.WriteLine("\n1. List All Tasks  \n2. View One Task  \n3. Add Task \n4. Update Task \n5. Delete Task \n6. Add Task (Disconnected Mode) \n7. Import Tasks from CSV \n8. Export Tasks to CSV \n0. Exit");
    Console.Write("Enter Choice: ");

    switch (Console.ReadLine())
    {
        case "1": // list all tasks
            foreach (TaskItem task in taskDAL.GetAllTasks())
            {
                PrintTask(task);
            }
            break;
        case "2": // find task by ID
            TaskItem? foundTask = taskDAL.GetTaskById(ReadId());
            if (foundTask == null)
            {
                Console.WriteLine("Task not found.");
            }
            else
            {
                PrintTask(foundTask);
            }
            break;
        case "3": // add a new task
            TaskItem newTask = ReadTask();
            bool taskWasAdded = taskDAL.AddTask(newTask);

            if (taskWasAdded)
            {
                Console.WriteLine("Task added.");
            }
            else
            {
                Console.WriteLine("Task was not added.");
            }
            break;
        case "4": // update a task
            // asks user for the ID of the task to update
            int updateId = ReadId();
            // creates a new TaskItem object with the updated values from the user
            TaskItem updatedTask = ReadTask();
            // assigns previously read ID to the new TaskItem object
            updatedTask.Id = updateId;
            // update the task in the database using the DAL with the new TaskItem object
            bool taskWasUpdated = taskDAL.UpdateTask(updatedTask);

            if (taskWasUpdated)
            {
                Console.WriteLine("Task updated.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
            break;
        case "5": // delete a task
            int deleteId = ReadId();
            bool taskWasDeleted = taskDAL.DeleteTask(deleteId);

            if (taskWasDeleted)
            {
                Console.WriteLine("Task deleted.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
            break;
        case "6": // add a new task using disconnected mode
            TaskItem disconnectedTask = ReadTask();
            bool disconnectedTaskWasAdded = taskDAL.AddTaskDisconnected(disconnectedTask);

            if (disconnectedTaskWasAdded)
            {
                Console.WriteLine("Task added using disconnected mode.");
            }
            else
            {
                Console.WriteLine("Task was not added.");
            }
            break;
        case "7": // import tasks from a CSV file
            Console.Write("CSV file path: ");
            string csvFilePath = Console.ReadLine() ?? "";
            int importedCount = taskDAL.ImportTasksFromCsv(csvFilePath);

            if (importedCount >= 0) // tell the user how many tasks were successfully inserted from file
            {
                Console.WriteLine($"{importedCount} task(s) imported.");
            }
            else
            {
                Console.WriteLine("CSV import failed.");
            }
            break;
        case "8": // export all tasks to a new CSV file
            Console.Write("New CSV file path: ");
            string exportFilePath = Console.ReadLine() ?? "";
            bool exportSucceeded = taskDAL.ExportTasksToCsv(exportFilePath);

            if (exportSucceeded)
            {
                Console.WriteLine("Tasks exported successfully.");
            }
            else
            {
                Console.WriteLine("CSV export failed.");
            }
            break;
        case "0":
            Console.WriteLine("Exiting To-Do App. Have a great day :)");
            return;
        default:
            Console.WriteLine("Oops! Invalid choice.");
            break;
    }
}

static int ReadId() // helper function to read an integer from the console, returning -1 if invalid
{
    Console.Write("Enter Task ID: ");
    string? input = Console.ReadLine();
    int id;

    bool isValidId = int.TryParse(input, out id);

    if (isValidId)
    {
        return id;
    }
    else
    {
        return -1;
    }
}

static TaskItem ReadTask() // helper function to read task details from the console and return a new TaskItem object
{
    // if user provides null input, use ""
    Console.Write("Title: ");
    string title = Console.ReadLine() ?? "";
    Console.Write("Description: ");
    string description = Console.ReadLine() ?? "";

    DateTime dueDate = DateTime.MinValue;
    bool validDate = false;

    while (!validDate) // keep prompting user for a valid date
    {
        Console.Write("Due date (yyyy-MM-dd): ");
        string dateInput = Console.ReadLine() ?? "";

        try
        {
            dueDate = DateTime.ParseExact(
                dateInput,
                "yyyy-MM-dd",
                // parse the date using the format "yyyy-MM-dd" - irrespective of the user's locale
                System.Globalization.CultureInfo.InvariantCulture);
            validDate = true;
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid date. Please use the format yyyy-MM-dd.");
        }
    }

    string status = "";
    bool validStatus = false;

    while (!validStatus) // keep prompting user for a valid status
    {
        Console.WriteLine("Select a status:");
        Console.WriteLine("1. Not Started");
        Console.WriteLine("2. In Progress");
        Console.WriteLine("3. Completed");
        Console.Write("Status: ");

        string statusChoice = Console.ReadLine() ?? "";

        switch (statusChoice) // assign status based on numerical selection from user
        {
            case "1":
                status = "Not Started";
                validStatus = true;
                break;
            case "2":
                status = "In Progress";
                validStatus = true;
                break;
            case "3":
                status = "Completed";
                validStatus = true;
                break;
            default:
                Console.WriteLine("Invalid status. Please choose 1, 2, or 3.");
                break;
        }
    }

    return new TaskItem // yay new TaskItem is ready to go! assign values now
    {
        Title = title,
        Description = description,
        DueDate = dueDate,
        Status = status
    };
}

static void PrintTask(TaskItem task) // prints a TaskItem obj to the console w/ its values
{
    Console.WriteLine($"{task.Id}: {task.Title} | {task.Description} | {task.DueDate:yyyy-MM-dd} | {task.Status}");
}
