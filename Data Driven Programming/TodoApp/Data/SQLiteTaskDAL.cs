using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Globalization;
using TodoApp.Models;

namespace TodoApp.Data;

public class SQLiteTaskDAL : ITaskDAL // implement the TaskDAL interface
{
    private readonly string _connectionString;

    public SQLiteTaskDAL(string databasePath)
    {
        _connectionString = $"Data Source={databasePath}"; // use path to form connection string
        CreateTable(); // create table on startup if not exiists
    }

    private SqliteConnection OpenConnection()
    {
        try // to use connection string to open a connection, retun it on success
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
        catch (SqliteException ex) // program will terminate if no connection
        {
            throw new InvalidOperationException("Could not connect to the task database.", ex);
        }
    }

    private void CreateTable()
    {
        try // create the Tasks table if it does not exist
        {
            // using var means C# automatically disposes the connection and command when out of scope
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT NOT NULL,
                    DueDate TEXT NOT NULL,
                    Status TEXT NOT NULL CHECK (Status IN ('Not Started', 'In Progress', 'Completed'))
                );
                """;
            command.ExecuteNonQuery();
        }
        catch (Exception ex) // on a failure app will terminate as it will be unusable w/o db creation
        {
            throw new InvalidOperationException(
                "Could not create the Tasks table.", ex);
        }
    }

    public List<TaskItem> GetAllTasks()
    {
        var tasks = new List<TaskItem>();

        try // to open a connection and retrieve all tasks
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tasks";
            using var reader = command.ExecuteReader();

            while (reader.Read()) // while there are more tasks, read them & add to list
            {
                tasks.Add(ReadTask(reader));
            }
        }
        catch (Exception ex) // on failure, show the error and return an empty list
        {
            Console.WriteLine($"Could not retrieve tasks: {ex.Message}");
        }

        return tasks;
    }

    public TaskItem? GetTaskById(int id)
    {
        try // open a connection and use the id to grab one task
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tasks WHERE Id = @id"; // match a task's id with the provided id
            command.Parameters.AddWithValue("@id", id); // add the id parameter to the command
            using var reader = command.ExecuteReader();

            if (reader.Read()) // on success, return the task
            {
                return ReadTask(reader);
            }

            return null; // otherwise, no task is found & return null
        }
        catch (Exception ex) // error handling - such as if an invalid id is provided
        {
            Console.WriteLine($"Could not retrieve task: {ex.Message}");
            return null;
        }
    }

    public bool AddTask(TaskItem task)
    {
        try // open a connection and add a task
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO Tasks (Title, Description, DueDate, Status)
                VALUES (@title, @description, @dueDate, @status)
                """;
            AddTaskParameters(command, task);
            return command.ExecuteNonQuery() == 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not add task: {ex.Message}");
            return false;
        }
    }

    public bool AddTaskDisconnected(TaskItem task) // disconnected mode for adding a task
    {
        try // to store the new task in memory without an open database connection
        {
            // temp. store data in memory using a DataSet and DataTable
            using DataSet taskDataSet = new DataSet();
            DataTable taskTable = new DataTable("Tasks");

            taskTable.Columns.Add("Title", typeof(string));
            taskTable.Columns.Add("Description", typeof(string));
            taskTable.Columns.Add("DueDate", typeof(string));
            taskTable.Columns.Add("Status", typeof(string));
            taskDataSet.Tables.Add(taskTable);

            DataRow newRow = taskTable.NewRow();
            newRow["Title"] = task.Title;
            newRow["Description"] = task.Description;
            newRow["DueDate"] = task.DueDate.ToString("yyyy-MM-dd");
            newRow["Status"] = task.Status;
            taskTable.Rows.Add(newRow);

            // reconnect once the disconnected data is ready to be saved
            // in practice this could be done in a batch for multiple tasks
            // the connection will be closed control leaves this block
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                INSERT INTO Tasks (Title, Description, DueDate, Status)
                VALUES (@title, @description, @dueDate, @status)
                """;
            // cannot use helper method AddTaskParameters here as it takes a TaskItem object, not a DataRow
            command.Parameters.AddWithValue("@title", newRow["Title"]);
            command.Parameters.AddWithValue("@description", newRow["Description"]);
            command.Parameters.AddWithValue("@dueDate", newRow["DueDate"]);
            command.Parameters.AddWithValue("@status", newRow["Status"]);

            return command.ExecuteNonQuery() == 1; // true if 1 row effects (insert successful) and false if 0 or >1 (insert failed)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not add task in disconnected mode: {ex.Message}");
            return false;
        }
    }

    public int ImportTasksFromCsv(string filePath) // takes a file path to a CSV file and imports the tasks into the database, returning the number of tasks inserted
    {
        int tasksInserted = 0;

        try // to parse the CSV file and insert tasks into the database
        {
            using TextFieldParser parser = new TextFieldParser(filePath);
            parser.TextFieldType = FieldType.Delimited; // will be delimited by commas
            parser.SetDelimiters(","); // comma between values
            parser.HasFieldsEnclosedInQuotes = true; // allows for fields to be enclosed in quotes

            string[]? headings = parser.ReadFields(); // read the headings

            // validate the format of the headings
            // must not be null, must be correct length, must match the expected headings (case-insensitive)
            if (headings == null || headings.Length != 4 ||
                !headings[0].Equals("Title", StringComparison.OrdinalIgnoreCase) ||
                !headings[1].Equals("Description", StringComparison.OrdinalIgnoreCase) ||
                !headings[2].Equals("DueDate", StringComparison.OrdinalIgnoreCase) ||
                !headings[3].Equals("Status", StringComparison.OrdinalIgnoreCase))
            {
                throw new FormatException(
                    "CSV headings must be: Title,Description,DueDate,Status");
            }

            while (!parser.EndOfData) // there's still data to be read
            {
                string[]? fields = parser.ReadFields();

                // check for invalid row format
                if (fields == null || fields.Length != 4)
                {
                    throw new FormatException( $"CSV row {parser.LineNumber} must contain four fields.");
                }

                // validate and parse the date
                bool validDate = DateTime.TryParseExact(
                    fields[2],
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime dueDate);

                if (!validDate)
                {
                    throw new FormatException(
                        $"CSV row {parser.LineNumber} has an invalid date. Use yyyy-MM-dd.");
                }

                // check for valid status
                if (fields[3] != "Not Started" &&
                    fields[3] != "In Progress" &&
                    fields[3] != "Completed")
                {
                    throw new FormatException(
                        $"CSV row {parser.LineNumber} has an invalid status.");
                }

                // all validation passed, assign values
                TaskItem task = new TaskItem();
                task.Title = fields[0];
                task.Description = fields[1];
                task.DueDate = dueDate; // date parsed and assigned in validation efforts
                task.Status = fields[3];

                if (!AddTask(task)) // cant add the new task for some reason
                {
                    throw new InvalidOperationException(
                        $"The task on CSV row {parser.LineNumber} could not be inserted.");
                }

                tasksInserted++; // move to the next task
            }

            return tasksInserted; // num. tasks inserted
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not import tasks from CSV: {ex.Message}");
            return -1; // obvisouly failed (can't insert -1 tasks!)
        }
    }

    public bool ExportTasksToCsv(string filePath)
    {
        try // to open a connection and save the db content to a csv
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tasks";
            using var reader = command.ExecuteReader();
            using StreamWriter writer = new StreamWriter(filePath, false);

            writer.WriteLine("Id,Title,Description,DueDate,Status"); // add the header

            while (reader.Read()) // there's more to read
            {
                // assign variables
                string id = reader["Id"].ToString() ?? "";
                string title = EscapeCsv(reader["Title"].ToString() ?? "");
                string description = EscapeCsv(reader["Description"].ToString() ?? "");
                string dueDate = EscapeCsv(reader["DueDate"].ToString() ?? "");
                string status = EscapeCsv(reader["Status"].ToString() ?? "");

                // write to file in the correct format
                writer.WriteLine($"{id},{title},{description},{dueDate},{status}");
            }

            return true; // succuss!
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not export tasks to CSV: {ex.Message}");
            return false; // boo failure
        }
    }

    public bool UpdateTask(TaskItem task)
    {
        try // open connection and update a task using the id
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = """
                UPDATE Tasks
                SET Title = @title, Description = @description,
                    DueDate = @dueDate, Status = @status
                WHERE Id = @id
                """;
            AddTaskParameters(command, task);
            command.Parameters.AddWithValue("@id", task.Id); // add id to params seperately as AddTaskParameters does not include it
            return command.ExecuteNonQuery() == 1; // true if 1 row effects (update successful) and false if 0 or >1 (update failed)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not update task: {ex.Message}");
            return false;
        }
    }

    public bool DeleteTask(int id)
    {
        try // open connection and delete a task using the id
        {
            using var connection = OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tasks WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() == 1; // true if 1 row effects (delete successful) and false if 0 or >1 (delete failed)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete task: {ex.Message}");
            return false;
        }
    }

    private static void AddTaskParameters(SqliteCommand command, TaskItem task) // helper method to assign task fields to command parameters
    {
        command.Parameters.AddWithValue("@title", task.Title);
        command.Parameters.AddWithValue("@description", task.Description);
        command.Parameters.AddWithValue("@dueDate", task.DueDate.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@status", task.Status);
    }

    private static string EscapeCsv(string value) // helper used to check individual values for special characters
    {
        if (value.Contains(',') || value.Contains('"') ||
            value.Contains('\n') || value.Contains('\r'))
        {
            // escaping spec. chars prevents commas/quotes/line breaks corrupting the csv column structure
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        return value; // return escaped string
    }

    private static TaskItem ReadTask(SqliteDataReader reader) // helper method to read values from a table and store them in a Task object

    {
        // read in values from the database and return a new TaskItem object w/ values
        // GetOrdinal used to get column index by name, ensuring correct mapping even if the order of columns changes
        TaskItem task = new TaskItem();

        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
        task.Title = reader.GetString(reader.GetOrdinal("Title"));
        task.Description = reader.GetString(reader.GetOrdinal("Description"));
        task.DueDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("DueDate")));
        task.Status = reader.GetString(reader.GetOrdinal("Status"));

        return task;
    }
}
