using TodoApp.Models;
namespace TodoApp.Data;

public interface ITaskDAL // interface for the data access layer, allowing different db types or a different DAL to be used in future
{
    List<TaskItem> GetAllTasks(); // method for retrieving all tasks, returning the list of tasks
    TaskItem? GetTaskById(int id); // method for retrieving a task by its ID (possibly null)

    // CRUD operations return true if successful, false on failure/exception
    bool AddTask(TaskItem task); // method for adding a new task
    bool AddTaskDisconnected(TaskItem task); // add a task using a disconnected DataSet and DataTable
    int ImportTasksFromCsv(string filePath); // import tasks and return the number inserted
    bool ExportTasksToCsv(string filePath); // export all task records to a CSV file
    bool UpdateTask(TaskItem task); // method for updating an existing task
    bool DeleteTask(int id); // method for deleting a task by its ID
}
