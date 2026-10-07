namespace WtLab2.Models;

public abstract class Person : BaseEntity
{
    private string _fullName = string.Empty;

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("ФИО не может быть пустым");
            _fullName = value;
        }
    }

    public string Email { get; set; } = string.Empty;

    protected Person(int id, string fullName) : base(id)
    {
        FullName = fullName;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"  ФИО: {FullName}, Email: {Email}");
    }
}