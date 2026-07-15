namespace TodoApp.Models;

public class TaskItem
{
    private int _id;
    private string _title;
    private string _description;
    private DateTime _dueDate;
    private string _status;

    public TaskItem() // defult constructor to initialize a new task with default values
    {
        _id = 0;
        _title = "";
        _description = "";
        _dueDate = DateTime.MinValue;
        _status = "Not Started";
    }

    public int Id
    {
        get => _id;
        set => _id = value;
    }

    public string Title
    {
        get => _title;
        set => _title = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public DateTime DueDate
    {
        get => _dueDate;
        set => _dueDate = value;
    }

    public string Status
    {
        get => _status;
        set => _status = value;
    }
}
