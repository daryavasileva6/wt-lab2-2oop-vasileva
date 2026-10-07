using WtLab2.Interfaces;

namespace WtLab2.Models;

public abstract class BaseEntity : IStorable
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    protected BaseEntity(int id)
    {
        Id = id;
        CreatedAt = DateTime.Now;
    }

    public abstract string GetShortInfo();

    public virtual void PrintInfo()
    {
        Console.WriteLine($"[{GetType().Name}] Id={Id}, Создано={CreatedAt:g}");
    }

    protected void LogChange(string message)
    {
        Console.WriteLine($"[LOG {DateTime.Now:T}] {message}");
    }
}